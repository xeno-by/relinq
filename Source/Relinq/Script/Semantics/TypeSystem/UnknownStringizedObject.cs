using System;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Syntax.SyntaxReference;

namespace Relinq.Script.Semantics.TypeSystem
{
    public class UnknownStringizedObject : UnknownConstant
    {
        public UnknownStringizedObject(ConstantExpression constant) 
            : base(constant)
        {
            if (!EcmaScriptV3Syntax.IsString(constant.Token))
            {
                throw new ArgumentException(String.Format(
                    "No way that constant expression '{0}' can represent " +
                    "an object serialized as string.", this));
            }
        }
    }
}