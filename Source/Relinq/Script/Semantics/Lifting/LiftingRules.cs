using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Relinq.Helpers.Collections;
using Relinq.Script.Compilation.MethodRedirection;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Syntax.Operators;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.StructuralTrees;
using Relinq.Script.Semantics.Operators;

namespace Relinq.Script.Semantics.Lifting
{
    public static class LiftingRules
    {
        public static IEnumerable<MethodInfo> LiftSignatures(this IEnumerable<MethodInfo> ops)
        {
//            foreach (var holder in LiftImpl(ops).GetTypes())
//            {
//                foreach (var sig in holder.GetMethods())
//                {
//                    yield return sig;
//                }
//            }

            // todo. temporary N/A due to performance problems
            yield break;
        }

        public static AssemblyBuilder LiftImpl(IEnumerable<MethodInfo> ops)
        {
            // todo. use dynamic assembly holder and cache sigs
            var currentDomain = AppDomain.CurrentDomain;
            var name = new AssemblyName { Name = "Relinq.Signatures" };
            var asm = currentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);
            var module = asm.DefineDynamicModule("Relinq.Signatures.MainModule", "signatures.dll");

            LiftPredefined(module, ops.Where(op => op.IsPredefinedOperator()));
            LiftUserDefined(module, ops.Where(op => op.IsUserDefinedOperator()));

            // only user-defined casts are represented by methods, since
            // predefined casts don't really need these complications
            LiftUserDefined(module, ops.Where(op => op.IsUserDefinedImplicitCast()));

            return asm;
        }

        private static bool IsUpForLifting(MethodInfo op)
        {
            foreach (var pi in op.GetParameters().Concat(op.ReturnParameter.AsArray()))
            {
                var ptype = pi.ParameterType;
                if (!ptype.IsGenericParameter)
                {
                    if (!ptype.IsNonNullableValueType()) return false;
                }
                else
                {
                    // here we need to check only generic parameter attributes
                    // since one can't inherit from value types

                    Func<GenericParameterAttributes, bool> isNnvt = gpa =>
                        (gpa & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0;
                    if (isNnvt(ptype.GenericParameterAttributes))
                    {
                        continue;
                    }

                    Func<ICustomAttributeProvider, IEnumerable<SignatureValidWhenAttribute>> 
                        swats = cap => cap.Attrs<SignatureValidWhenAttribute>();

                    var unrelatedSwats = swats(op).Concat(
                        swats(op.ReturnTypeCustomAttributes).Concat(
                        op.GetParameters().Select(fpi => swats(fpi.ParameterType)).Flatten().Concat(
                        op.XGetGenericArguments().Where(gpi => gpi != ptype).Select(gpi => swats(gpi)).Flatten())));
                    if (unrelatedSwats.Any(swat => 
                        isNnvt(swat.InferGenericParameterAttributes(ptype))))
                    {
                        continue;
                    }

                    if (swats(ptype).Any(swat =>
                        isNnvt(swat.InferTargetGenericParameterAttributes())))
                    {
                        continue;
                    }

                    return false;
                }
            }

            if (op.GetOperatorTypes().Any(opType => opType.IsRelational()))
            {
                if (op.ReturnType != typeof(bool)) return false;
            }

            return true;
        }

        private static void LiftPredefined(ModuleBuilder module, IEnumerable<MethodInfo> ops)
        {
            var iface = module.DefineType("ILiftedPredefinedSignatures",
                TypeAttributes.Abstract | TypeAttributes.Interface);

            var next = 0;
            foreach (var source in ops.Where(IsUpForLifting))
            {
                var opTypes = source.GetOperatorTypes();
                if (opTypes.IsNullOrEmpty())
                {
                    throw new NotSupportedException(source.DeclaringType + "::" + source);
                }

                var sourceMap = source.GetGenericArgsMapping();

                // 1) method: any name...
                var lifted = iface.DefineMethod("S" + next++,
                    MethodAttributes.HideBySig | MethodAttributes.Abstract |
                    MethodAttributes.Virtual | MethodAttributes.Public,
                    CallingConventions.Standard);

                // 2) gparams: redefine types, but retain attributes and constraints
                if (source.IsGenericMethod)
                {
                    // can't happen for predefined, but might happen for user-defined
                    // that's why they're not implemented so far
                    if (source.GetGenericArguments().Any(garg => !garg.IsGenericParameter))
                    {
                        throw new NotImplementedException(String.Format(
                            "Don't know how to lift half-open generic signature: '{0}'", source));
                    }

                    var gparams = lifted.DefineGenericParameters(
                        source.GetGenericArguments().Select(garg => garg.Name).ToArray());
                    for (var i = 0; i < gparams.Length; i++)
                    {
                        var gparam = gparams[i];
                        var sourceGparam = source.GetGenericArguments()[i];
                        sourceGparam.CopyAttributesTo(gparam);

                        var sigConstraints = sourceGparam.GetGenericParameterConstraints();
                        gparam.SetBaseTypeConstraint(sigConstraints.Where(c => c.IsClass).SingleOrDefault());
                        gparam.SetInterfaceConstraints(sigConstraints.Where(c => c.IsInterface).ToArray());

                        var sigConstraintAttributes = sourceGparam.GenericParameterAttributes;
                        if (sourceMap.Any(kvp => kvp.Value == "m[" + i + "]") &&
                            (sourceGparam.GenericParameterAttributes 
                            & GenericParameterAttributes.NotNullableValueTypeConstraint) == 0)
                        {
                            sigConstraintAttributes |= GenericParameterAttributes.NotNullableValueTypeConstraint;
                        }

                        gparam.SetGenericParameterAttributes(sigConstraintAttributes);
                    }
                }

                // 3) retval: lift type, retain attributes and flags
                var tRetval = source.ReturnType;
                foreach (var kvp in sourceMap.Where(kvp2 => kvp2.Key.StartsWith("ret")))
                {
                    var myType = lifted.SelectStructuralUnit(kvp.Value);
                    tRetval = tRetval.InferStructuralUnit(kvp.Key.Substring(3), myType);
                }

                if(source.GetOperatorTypes().All(opType => !opType.IsRelational()))
                    tRetval = typeof(Nullable<>).MakeGenericType(tRetval);

                lifted.SetReturnType(tRetval);

                // todo. sadly atm I can't make this work
//                var retval = lifted.DefineParameter(0, source.ReturnParameter.Attributes, source.ReturnParameter.Name);
//                source.ReturnTypeCustomAttributes.CopyAttributesTo(retval);

                // 4) fparams: retain attributes, names and flags (including ParamsArray)
                var tFparams = source.GetParameters().Select(pi => pi.ParameterType).ToArray();
                for (var i = 0; i < tFparams.Length; i++)
                {
                    foreach (var kvp in sourceMap.Where(kvp2 => kvp2.Key.StartsWith("a" + i)))
                    {
                        var myType = lifted.SelectStructuralUnit(kvp.Value);
                        tFparams[i] = tFparams[i].InferStructuralUnit(kvp.Key.Substring(2), myType);
                    }
                }

                tFparams = tFparams.Select(fparam => typeof(Nullable<>).MakeGenericType(fparam)).ToArray();
                lifted.SetParameters(tFparams);

                for (var i = 0; i < source.GetParameters().Length; i++)
                {
                    var sourceParam = source.GetParameters()[i];
                    var param = lifted.DefineParameter(i + 1, sourceParam.Attributes, sourceParam.Name);
                    sourceParam.CopyAttributesTo(param);
                }

                // 1) method... retain attributes except [PredefinedOperator] 
                //    that gets replaced with [Lifted, RedirectTo(...)]
                source.CopyAttributesTo(lifted, cad =>
                    cad.Constructor.DeclaringType != typeof(PredefinedOperatorAttribute));
                EmitHelper.DefineAttribute<LiftedAttribute>(lifted);
                EmitHelper.DefineAttribute<RedirectToAttribute>(
                    lifted, source.DeclaringType, source.ToString());
            }

            iface.CreateType();
        }

        private static void LiftUserDefined(ModuleBuilder module, IEnumerable<MethodInfo> ops)
        {
            // todo. to be implemented
            // todo. keep in mind an important case of method using type generic argument(s)
            // todo. another tricky stuff is half-constructed generic method
            // todo. user-defined casts

//            throw new NotImplementedException("Lifting user-defined operators/casts is not implemented yet.");
        }
    }
}
