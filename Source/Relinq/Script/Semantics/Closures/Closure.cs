using System;
using System.Linq;
using Relinq.Helpers.Collections;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Script.Semantics.Closures
{
    public class Closure : InitOnlyDictionary<String, LambdaExpression>
    {
        public override string ToString()
        {
            return "[" + this.Select(kvp => kvp.Key).StringJoin() + "]";
        }
    }
}
