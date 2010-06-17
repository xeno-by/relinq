using System;
using System.Collections.Generic;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.TypeSystem;
using System.Linq;
using Relinq.Script.Syntax.Expressions;
using Relinq.Helpers.Assurance;

namespace Relinq.Script.Compilation.EngineAspects
{
    public class TypeInferenceOperationKey
    {
        private RelinqScriptExpression Self { get; set; }
        private RelinqScriptType MyType { get; set; }
        private IEnumerable<Type> Closure { get; set; }

        public TypeInferenceOperationKey(
            RelinqScriptExpression self, RelinqScriptType myType, IEnumerable<Type> closure)
        {
            Self = self.SurelyNotNull();
            MyType = myType;
            Closure = closure;
        }

        public override string ToString()
        {
            return String.Format("[{0}, [{1} + [{2}]]]", 
                Self, MyType == null ? "null" : MyType.ToString(), Closure.StringJoin());
        }

        public override bool Equals(object obj)
        {
            var other = obj as TypeInferenceOperationKey;
            return other != null && Self == other.Self && 
                MyType == other.MyType && Closure.SequenceEqual(other.Closure);
        }

        public override int GetHashCode()
        {
            var num = 0x10cee8ad;
            num = (-1521134295 * num) + (Self == null ? 0 : Self.GetHashCode());
            num = (-1521134295 * num) + (MyType == null ? 0 : MyType.GetHashCode());
            Closure.ForEach(entry => num = (-1521134295 * num) + entry.GetHashCode());
            return num;
        }
    }
}