using System;
using System.Collections.Generic;
using System.Reflection;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using System.Linq;
using Relinq.Helpers.StructuralTrees;

namespace Relinq.Script.Semantics.Lookup
{
    // http://code.google.com/p/relinq/wiki/HidingMethods
    public static class HidingMethodsHelper
    {
        public static bool IsHiddenBy(this MethodInfo m1, MethodInfo m2)
        {
            if (m1.IsExtension() || m2.IsExtension()) return false;
            if (m1.IsOperator() || m2.IsOperator()) return false;

            m1 = m1.XGetGenericDefinition();
            m2 = m2.XGetGenericDefinition();
            if (!m2.DeclaringType.LookupThisAndBaseTypes().Contains(m1.DeclaringType)) return false;

            var aux = Guid.NewGuid().ToString();
            var namesMatch = (m1.IsIndexer() ? aux : m1.Name) == (m2.IsIndexer() ? aux : m2.Name);
            var typeParamsMatch = m1.XGetGenericArguments().Length == m2.XGetGenericArguments().Length;
            var paramListStyleMatch = m1.IsVarargs() == m2.IsVarargs();
            var formalParamsMatch = m1.GetParameters().AllMatch(m2.GetParameters(), (p1, p2) =>
            {
                if (p1.IsOut ^ p2.IsOut) return false;
                if (p1.ParameterType.IsByRef ^ p2.ParameterType.IsByRef) return false;

                var p1m = p1.ParameterType.GetStructuralTree();
                var p2m = p2.ParameterType.GetStructuralTree();

                return p1m.AllMatch(p2m, (p1mi, p2mi) =>
                {
                    if (p1mi.Key != p2mi.Key) return false;
                    if (p1mi.Value.IsGenericParameter ^ p2mi.Value.IsGenericParameter) return false;

                    if (!p1mi.Value.IsGenericParameter && !p2mi.Value.IsGenericParameter)
                    {
                        return p1mi.Value.SameBasisType(p2mi.Value);
                    }
                    else
                    {
                        Func<MethodInfo, KeyValuePair<String, Type>, bool> isMethodParam = 
                            (mi, pimi) => mi.XGetGenericArguments().Contains(pimi.Value);
                        var positionsMatch = p1mi.Value.GenericParameterPosition ==
                            p2mi.Value.GenericParameterPosition;

                        return isMethodParam(m1, p1mi) && isMethodParam(m2, p2mi) && positionsMatch;
                    }
                });
            });

            var sigsMatch = namesMatch && typeParamsMatch && paramListStyleMatch && formalParamsMatch;
            return m1.IsHideBySig ? sigsMatch : namesMatch;
        }
    }
}
