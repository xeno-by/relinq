using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using NUnit.Framework;
using Relinq.Script.Compilation.SignatureValidation;
using Relinq.Script.Syntax.Operators;
using Relinq.Script.Semantics.Operators;

namespace Relinq.Playground
{
    [TestFixture]
    public class MessingWithCodeGen
    {
        [Test]
        public void GenerateSingleLiftedSignature()
        {
            var currentDomain = AppDomain.CurrentDomain;
            var name = new AssemblyName{Name = "Relinq.SingleSignature"};
            var asm  = currentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);
            var module = asm.DefineDynamicModule("Relinq.SingleSignature.MainModule", "signature.dll");

            var iface = module.DefineType("ISingleSignature", 
                TypeAttributes.Abstract | TypeAttributes.Interface);
            DefineAttribute<PredefinedOperatorAttribute>(iface, OperatorType.Equal);
            DefineAttribute<PredefinedOperatorAttribute>(iface, OperatorType.NotEqual);

            var method = iface.DefineMethod("S",
                MethodAttributes.HideBySig | MethodAttributes.Abstract | 
                    MethodAttributes.Virtual | MethodAttributes.Public, 
                CallingConventions.Standard);
            var garg = method.DefineGenericParameters("S")[0];
            garg.SetGenericParameterAttributes(
                GenericParameterAttributes.NotNullableValueTypeConstraint);
            DefineAttribute<IsEnumAttribute>(garg);

            method.SetReturnType(typeof(bool));

            var gargNullable = typeof(Nullable<>).MakeGenericType(garg);
            method.SetParameters(gargNullable, gargNullable);
            method.DefineParameter(1, ParameterAttributes.None, "x");
            method.DefineParameter(2, ParameterAttributes.None, "y");

            var ifaceType = iface.CreateType();
            asm.Save("signature.dll");
        }

        private void DefineAttribute<A>(object builder, params object[] @params)
        {
            Action<ConstructorInfo, object[]> setCustomAttribute = (ci, args) =>
                {
                    var method = builder.GetType().GetMethod("SetCustomAttribute",
                        new Type[] {typeof(CustomAttributeBuilder)});
                    method.Invoke(builder, new object[] { new CustomAttributeBuilder(ci, args) });
                };

            var ctor = typeof(A).GetConstructor(
                @params.Select(param => param.GetType()).ToArray());
            setCustomAttribute(ctor, @params);
        }
    }
}