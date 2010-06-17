using System;
using System.Collections.Generic;
using System.Reflection;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Semantics.Lookup;

namespace Relinq.Script.Compilation.SignatureValidation
{
    [AttributeUsage(AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
    public class IsStructAttribute : SignatureValidWhenAttribute
    {
        public override bool ValidateSignature(SignatureInspectionOptions options)
        {
            return options.InspectedType.IsValueType;
        }

        public override GenericParameterAttributes InferTargetGenericParameterAttributes()
        {
            return GenericParameterAttributes.NotNullableValueTypeConstraint;
        }

        public override IEnumerable<Type> InferTargetGenericParameterBaseTypes()
        {
            return typeof(int).LookupBaseTypes();
        }
    }
}