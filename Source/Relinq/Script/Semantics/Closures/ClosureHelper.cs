using System;
using Relinq.Script.Syntax.Expressions;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Semantics.Closures
{
    public static class ClosureHelper
    {
        public static Closure GetClosure(this RelinqScriptExpression e)
        {
            return e.Parent == null ? 
                new Closure() : 
                // dynamic dispatch for extension methods :)
                (e.Parent is LambdaExpression) ? ((LambdaExpression)e.Parent).GetClosure() : e.Parent.GetClosure();
        }

        public static Closure GetClosure(this LambdaExpression le)
        {
            var closure = ((RelinqScriptExpression)le).GetClosure();
            le.Args.ForEach(argName => closure.Add(argName, le));
            return closure;
        }
    }
}