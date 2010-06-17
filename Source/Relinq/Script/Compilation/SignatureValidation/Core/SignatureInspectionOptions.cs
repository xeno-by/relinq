using System;
using System.Reflection;
using Relinq.Helpers.Reflection;
using Relinq.Script.Semantics.TypeSystem;

namespace Relinq.Script.Compilation.SignatureValidation.Core
{
    public class SignatureInspectionOptions
    {
        public MethodInfo Signature;
        public RelinqScriptType[] ArgTypes;
        public bool InspectingMethod = false;
        public int InspectingTypeParam = -1;
        public bool InspectingRetVal = false;
        public int InspectingParamOrArg = -1;

        public SignatureInspectionOptions(MethodInfo signature)
        {
            Signature = signature;
        }

        public SignatureInspectionOptions(MethodInfo signature, RelinqScriptType[] argTypes)
            : this(signature)
        {
            ArgTypes = argTypes;
        }

        public bool IsValid()
        {
            // todo: implement redundancy analysis
            return InspectingMethod || InspectingTypeParam != -1 ||
                InspectingRetVal || InspectingParamOrArg != -1;
        }

        public Type InspectedType
        {
            get
            {
                if (InspectingTypeParam != -1)
                {
                    return Signature.XGetGenericArguments()[InspectingTypeParam];
                }
                else if (InspectingRetVal)
                {
                    return Signature.ReturnType;
                }
                else if (InspectingParamOrArg != -1)
                {
                    return Signature.GetParameters()[InspectingTypeParam].ParameterType;
                }

                throw new NotSupportedException(ToString());
            }
        }

        public ParameterInfo InspectedParameter
        {
            get
            {
                if (InspectingRetVal)
                {
                    return Signature.ReturnParameter;
                }
                else if (InspectingParamOrArg != -1)
                {
                    return Signature.GetParameters()[InspectingTypeParam];
                }

                throw new NotSupportedException(ToString());
            }
        }

        public RelinqScriptType InspectedArgument
        {
            get
            {
                if (InspectingParamOrArg != -1)
                {
                    return ArgTypes[InspectingTypeParam];
                }

                throw new NotSupportedException(ToString());
            }
        }

        public override string ToString()
        {
            return String.Format("[Sig = '{0}', ?m = {1}, ?tp = {2}, ?ret = {3}, ?fp = {4}]",
                Signature, InspectingMethod, InspectingTypeParam, InspectingRetVal, InspectingParamOrArg);
        }
    }
}