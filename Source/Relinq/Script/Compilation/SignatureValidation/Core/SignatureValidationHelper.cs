using System.Collections.Generic;
using System.Reflection;
using Relinq.Script.Semantics.TypeSystem;
using System.Linq;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Compilation.SignatureValidation.Core
{
    public static class SignatureValidationHelper
    {
        public static bool IsValid(this MethodInfo mi, IEnumerable<RelinqScriptType> argTypes)
        {
            return mi.IsValid(argTypes.ToArray());
        }

        public static bool IsValid(this MethodInfo mi, params RelinqScriptType[] argTypes)
        {
            var methodValid = mi.Attrs<SignatureValidWhenAttribute>()
                .Select(retAttr => retAttr.ValidateSignature(
                    new SignatureInspectionOptions(mi, argTypes){InspectingMethod = true}));

            var retvalValid = mi.ReturnTypeCustomAttributes.Attrs<SignatureValidWhenAttribute>()
                .Select(retAttr => retAttr.ValidateSignature(
                    new SignatureInspectionOptions(mi, argTypes){InspectingRetVal = true}));

            var tparamsValid = mi.XGetGenericDefinition().GetGenericArguments().Select((garg, i) => 
                garg.Attrs<SignatureValidWhenAttribute>().Select(retAttr => retAttr.ValidateSignature(
                    new SignatureInspectionOptions(mi, argTypes){InspectingTypeParam = i}))).Flatten();

            var fparamsValid = mi.GetParameters().Select((fparam, i) =>
                fparam.Attrs<SignatureValidWhenAttribute>().Select(retAttr => retAttr.ValidateSignature(
                    new SignatureInspectionOptions(mi, argTypes){InspectingParamOrArg = i}))).Flatten();

            return methodValid.Concat(retvalValid).Concat(tparamsValid).Concat(fparamsValid).All(b => b);
        }
    }
}
