using System;
using System.Collections.Generic;
using System.Reflection;

namespace Relinq.Script.Compilation.SignatureValidation.Core
{
    // todo. add logics of checking whether signature-valid attributes 
    // todo. do not contradict other metadata (in both regular and lifted cases)
    public abstract class SignatureValidWhenAttribute : Attribute
    {
        public abstract bool ValidateSignature(SignatureInspectionOptions options);

        public virtual GenericParameterAttributes InferTargetGenericParameterAttributes()
        {
            return 0;
        }

        public virtual IEnumerable<Type> InferTargetGenericParameterBaseTypes()
        {
            return new Type[0];
        }

        public virtual GenericParameterAttributes InferGenericParameterAttributes(Type t)
        {
            return 0;
        }

        public virtual IEnumerable<Type> InferGenericParameterBaseTypes(Type t)
        {
            return new Type[0];
        }
    }
}