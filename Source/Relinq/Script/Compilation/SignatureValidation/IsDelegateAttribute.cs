using System;
using System.Collections.Generic;
using System.Reflection;
using Relinq.Helpers.Reflection;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Semantics.Lookup;

namespace Relinq.Script.Compilation.SignatureValidation
{
    [AttributeUsage(AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
    public class IsDelegateAttribute : SignatureValidWhenAttribute
    {
        public override bool ValidateSignature(SignatureInspectionOptions options)
        {
            return options.InspectedType.IsDelegate();
        }

        public override GenericParameterAttributes InferTargetGenericParameterAttributes()
        {
            return GenericParameterAttributes.ReferenceTypeConstraint;
        }

        public override IEnumerable<Type> InferTargetGenericParameterBaseTypes()
        {
            return typeof(EventHandler).LookupBaseTypes();
        }
    }
}