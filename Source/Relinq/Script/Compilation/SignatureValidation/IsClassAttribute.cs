using System;
using System.Reflection;
using Relinq.Script.Compilation.SignatureValidation.Core;

namespace Relinq.Script.Compilation.SignatureValidation
{
    [AttributeUsage(AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
    public class IsClassAttribute : SignatureValidWhenAttribute
    {
        public override bool ValidateSignature(SignatureInspectionOptions options)
        {
            return options.InspectedType.IsClass;
        }

        public override GenericParameterAttributes InferTargetGenericParameterAttributes()
        {
            return GenericParameterAttributes.ReferenceTypeConstraint;
        }
    }
}