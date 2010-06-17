using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Helpers.Reflection;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Semantics.Lookup;

namespace Relinq.Script.Compilation.SignatureValidation
{
    [AttributeUsage(AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
    public class IsEnumAttribute : SignatureValidWhenAttribute
    {
        private Type UnderlyingType { get; set; }
        private String UnderlyingTypeRef { get; set; }

        public IsEnumAttribute()
        {
        }

        public IsEnumAttribute(Type underlyingType)
        {
            UnderlyingType = underlyingType;
        }

        public IsEnumAttribute(String typeArgName)
        {
            UnderlyingTypeRef = typeArgName;
        }

        public override bool ValidateSignature(SignatureInspectionOptions options)
        {
            var t = options.InspectedType;

            var uparam = UnderlyingTypeRef == null ? UnderlyingType :
                options.Signature.XGetGenericDefinition().XGetGenericArguments()
                .Where(arg => arg.Name == UnderlyingTypeRef).Single();
            var uindex = uparam == null ? -1 : uparam.GenericParameterPosition;
            var u = uindex == -1 ? null : options.Signature.XGetGenericArguments()[uindex];

            return t.IsEnum && (u == null || Enum.GetUnderlyingType(t) == u);
        }

        public override GenericParameterAttributes InferTargetGenericParameterAttributes()
        {
            return GenericParameterAttributes.NotNullableValueTypeConstraint;
        }

        public override IEnumerable<Type> InferTargetGenericParameterBaseTypes()
        {
            return typeof(DayOfWeek).LookupBaseTypes();
        }

        public override GenericParameterAttributes InferGenericParameterAttributes(Type t)
        {
            return t.Name == UnderlyingTypeRef ? GenericParameterAttributes.NotNullableValueTypeConstraint 
                : base.InferGenericParameterAttributes(t);
        }

        public override IEnumerable<Type> InferGenericParameterBaseTypes(Type t)
        {
            return t.Name == UnderlyingTypeRef ? typeof(int).LookupBaseTypes() 
                : base.InferGenericParameterBaseTypes(t);
        }
    }
}
