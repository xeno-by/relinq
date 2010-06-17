using System;
using System.Reflection;
using Relinq.Script.Semantics.Operators;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Semantics.Lifting;
using Relinq.Script.Syntax.Operators;
using System.Linq;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Semantics.Lookup
{
    public static class LookupOperatorsHelper
    {
        // http://code.google.com/p/relinq/wiki/LookupOperators
        public static MethodGroup LookupOperators(this OperatorType opType, params RelinqScriptType[] rt_operands)
        {
            var operands = rt_operands.Select(rt => rt is ClrType ? ((ClrType)rt).Type : null).ToArray();

            // several (but not all) operands can be non-CLR types: 
            // that means these parameters represent not yet inferred types
            // and that exact CLR types need to be inferred from the context
            if (operands.All(operand => operand == null))
            {
                return null;
            }
            else
            {
                if (opType.GetArity() != operands.Length)
                {
                    return null;
                }

                var predefined = opType.GetPredefinedOperators();
                var userDefined = operands.Select(operand => operand == null ? new MethodInfo[0] :
                    // a single undecorate is enough since: 
                    // 1) there can't be recursive nullables, so a single undecorate will do.
                    // 2) noone can inherit a nullable type, so base types do not need personal treatment.
                    operand.UndecorateNullable().LookupThisAndBaseTypes().Select(bt => bt
                        .GetMethods(BF.PublicStatic)
                        .Where(mi => mi.DeclaringType == bt)
                        .Where(mi => mi.IsUserDefinedOperator())
                        .Where(mi => mi.GetOperatorTypes().Contains(opType))).Flatten()).Flatten().Distinct();

                userDefined.ToArray();

                // a special rule of declaring user-defined conditional logical operators
                if (opType == OperatorType.AndAlso || opType == OperatorType.OrElse)
                {
                    Func<Type, bool> declaresAllBoolOps = t => t
                        .GetMethods(BF.PublicStatic)
                        .Where(mi => mi.DeclaringType == t)
                        .Where(mi => mi.IsBooleanOperator())
                        .Count() == 2; // both true() and false()

                    userDefined = userDefined
                        .Where(mi => mi.DeclaringType.SameType(mi.ReturnType))
                        .Where(mi => declaresAllBoolOps(mi.DeclaringType));
                }

                var alternatives = predefined.Concat(userDefined);
                alternatives = alternatives.Concat(alternatives.LiftSignatures());

                alternatives = alternatives.Where(alt =>
                    alt.GetParameters().All(pi => !pi.IsOut && !pi.ParameterType.IsByRef));

                return alternatives.IsNullOrEmpty() ? null :
                    new MethodGroup(alternatives, opType.GetOpCode());
            }
        }
    }
}
