using System;
using System.Collections.Generic;
using Relinq.V2.Helpers;
using System.Linq;
using Relinq.V2.RelinqScript.Reflection;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference
{
    public class FuzzyLambda : FuzzyType
    {
        public IDictionary<String, FuzzyType> Args { get; private set; }
        public FuzzyType ReturnValue { get { return GenericArgs[0].GenericArgs.Last(); } }

        protected FuzzyLambda(IEnumerable<String> args) 
            // bug: jeez... that's wtf
//            : base(ReflectionHelper.ForgeFuncType(args.Count()))
            : base(ReflectionHelper.ForgeFuncType(args.Count()).ForgeTypedLambda())
        {
            var fuzzyArgs = new Dictionary<String, FuzzyType>();

            var position = 0;
            foreach (var arg in args)
            {
                var realLambdaType = GenericArgs[0];
                fuzzyArgs[arg] = realLambdaType.GenericArgs[position++];
            }

            Args = fuzzyArgs.AsReadOnly();

            // Register typepoints for debug -> 0 downtime in release, but immensely useful in debug mode
            // bug: jeez... that's wtf
//            ReturnValue.RegDebuggableParent(this).SetDesc("ret");
//            Args.ForEach((arg, i) => arg.Value.RegDebuggableParent(this).SetDesc("arg"+i));
        }

        protected override String ContentToString() { return "l:" + (RuntimeType == null ? "null" : RuntimeType.Name); }

        public static FuzzyLambda Unknown(IEnumerable<String> args)
        {
            return new FuzzyLambda(args);
        }
    }
}