using System;
using System.Collections.Generic;
using Relinq.Helpers.Reflection;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Semantics.Lookup;

namespace Relinq.Script.Compilation.SignatureValidation
{
    [AttributeUsage(AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
    public class IsNullableAttribute : SignatureValidWhenAttribute
    {
        public override bool ValidateSignature(SignatureInspectionOptions options)
        {
            return options.InspectedType.IsNullable();
        }

        public override IEnumerable<Type> InferTargetGenericParameterBaseTypes()
        {
            return typeof(int?).LookupBaseTypes();
        }
    }
}