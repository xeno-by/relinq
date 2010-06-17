using System;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Semantics.TypeSystem;

namespace Relinq.Script.Compilation.SignatureValidation
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class IsNullAttribute : SignatureValidWhenAttribute
    {
        public override bool ValidateSignature(SignatureInspectionOptions options)
        {
            return options.InspectedArgument is Null;
        }
    }
}
