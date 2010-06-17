using System;
using System.Collections.Generic;
using System.Reflection;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.Operators.Signatures;
using Relinq.Script.Syntax.Operators;
using System.Linq;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.Operators
{
    public static class PredefinedOperators
    {
        // todo. carefully check for duplicate signatures for the same operator type!

        private static Dictionary<OperatorType, MethodInfo[]> _repo = 
            new Dictionary<OperatorType, MethodInfo[]>();

        static PredefinedOperators()
        {
            var sampleHost = typeof(ISpecialEqualitySignatures);
            var allHosts = sampleHost.Assembly.GetTypes()
                .Where(t => t.Namespace == sampleHost.Namespace);

            foreach (var host in allHosts)
            {
                if (!host.IsInterface || host.GetInterfaces().Length != 0 || host.IsGenericType)
                {
                    throw new NotSupportedException(host.ToString());
                }
                else
                {
                    foreach (var sig in host.GetMethods())
                    {
                        var opTypes = sig.GetOperatorTypes();
                        if (opTypes.IsNullOrEmpty())
                        {
                            throw new NotSupportedException(host + "::" + sig);
                        }

                        foreach (var opType in opTypes)
                        {
                            if (!_repo.ContainsKey(opType))
                                _repo.Add(opType, new MethodInfo[0]);

                            _repo[opType] = new List<MethodInfo>(_repo[opType]){sig}.ToArray();
                        }
                    }
                }
            }
        }

        public static MethodInfo[] GetPredefinedOperators(this OperatorType opType)
        {
            return _repo.ContainsKey(opType) ? _repo[opType] : new MethodInfo[0];
        }
    }
}