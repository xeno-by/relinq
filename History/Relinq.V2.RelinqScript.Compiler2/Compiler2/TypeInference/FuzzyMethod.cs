using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Relinq.V2.RelinqScript.Compiler2.Expressions;
using Relinq.V2.RelinqScript.Reflection.ForgedMethods;
using System.Linq;
using Relinq.V2.Helpers;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;
using Relinq.V2.RelinqScript.Reflection;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference
{
    public class FuzzyMethod : FuzzyMetadata
    {
        private IMethodInfo _method;
        public IList<FuzzyType> GenericArguments { get; private set; }

        public override bool IsFullyInferred 
        { 
            get 
            {
                return
                    // we already know signature of the method, so the idea is
                    // to just check whether all possible generic arguments are inferred:
                    // both type-related and method-related (prior is a PITA btw - see below)

                    Target.GenericArgs.All(arg => arg.IsFullyInferred) &&
                    GenericArguments.All(arg => arg.IsFullyInferred);
            } 
        }

        // todo: here we'll need to replace generic arguments with inferred types
        public MethodInfo RuntimeMethod 
        {
            get
            {
                var imethod = _method.ImplementGenericPattern(
                    Target.GenericArgs.Select(arg => arg.RuntimeType).ToArray(),
                    GenericArguments.Select(arg => arg.RuntimeType).ToArray());
                return imethod.ToRuntimeMethod();
            }
        }

        public FuzzyType Target { get; private set; }
        public FuzzyType ReturnValue { get; private set; }
        public FuzzyType[] Args { get; private set; }

        public override TypeInferenceUnit Host
        {
            set
            {
                base.Host = value;
                if (Target != null) Target.Host = value;
                if (ReturnValue != null) ReturnValue.Host = value;
                Args.ForEach(arg => arg.Host = value);
            }
        }

        public static FuzzyMethod FromRuntimeMethod(FuzzyType target, IMethodInfo runtimeMethod)
        {
            return new FuzzyMethod(target, runtimeMethod);
        }

        protected override String FuzzyTypeCode { get { return "Mt"; } }
        protected override String ContentToString()
        {
            var parentBinding = DebuggableParents
                .OfType<FuzzyMethodBinding>().SingleOrDefault();
            if (parentBinding == null)
            {
                return "alt:-1";
            }
            else
            {
                return Array.IndexOf((parentBinding.Alternatives ?? Enumerable.Empty<FuzzyMethod>()).ToArray(), this).ToString();
            }
        }

        private FuzzyMethod(FuzzyType target, IMethodInfo runtimeMethod)
        {
            _method = runtimeMethod;

            var genericArgs = new List<FuzzyType>();
            foreach (var genericArg in _method.GetMethodGenericArguments())
                genericArgs.Add(FuzzyType.FromRuntimeType(genericArg));
            GenericArguments = genericArgs.AsReadOnly();

            Target = target;
            ReturnValue = FuzzyType.FromRuntimeType(_method.ReturnType);
            Args = _method.GetParameters()
                .Select(param => FuzzyType.FromRuntimeType(param.ParameterType))
                .ToArray();

            // establishing links between typedef generic args and method's generic args
            // read more comments at FuzzyType.GetFieldOrProperty
            // see also comments to ReflectionHelper.GetGenericArgsMapping

            foreach (var pair in _method.GetGenericArgsMapping())
            {
                var typeDefArg = SelectGenericArg(pair.Value);
                var publicTypePointArgs = SelectGenericArg(pair.Key);
                publicTypePointArgs.IsIdenticalTo(typeDefArg);
            }

            // SomeMoreStuffInferred handlers for children just escalate the event
            // no need to process it since everything necessary is already done by links
            //
            // upd. currently noone cares about this event from this very class, 
            // but imo it's still important to comply to FuzzyMetadata contract

            Target.SomeMoreStuffInferred += (o, e) => FireSomeMoreStuffInferred();
            ReturnValue.SomeMoreStuffInferred += (o, e) => FireSomeMoreStuffInferred();
            Args.ForEach(arg => arg.SomeMoreStuffInferred += (o, e) => FireSomeMoreStuffInferred());

            // Register typepoints for debug -> 0 downtime in release, but immensely useful in debug mode
            var methodBinding = target.DebuggableParents.OfType<FuzzyMethodBinding>().SingleOrDefault();
            if (methodBinding != null)
            {
                this.RegDebuggableParent(methodBinding).SetDesc("alt");
                ReturnValue.RegDebuggableParent(this).SetDesc("ret");
                Args.ForEach((arg, i) => arg.RegDebuggableParent(this).SetDesc("arg" + i));
            }
        }

        public FuzzyType SelectGenericArg(String path)
        {
            var matchPath = Regex.Match(path, @"^ret(?<subpath>(\[\d*\])*)$");
            if (matchPath.Success)
            {
                var subPath = matchPath.Result("${subpath}");
                return ReturnValue.SelectGenericArg(subPath);
            }

            var matchTarget = Regex.Match(path, @"^t(?<subpath>(\[\d*\])*)$");
            if (matchTarget.Success)
            {
                var subPath = matchTarget.Result("${subpath}");
                return Target.SelectGenericArg(subPath);
            }

            var matchMethod = Regex.Match(path, @"^m\[(?<index>\d*)\](?<subpath>(\[\d*\])*)$");
            if (matchMethod.Success)
            {
                var index = int.Parse(matchMethod.Result("${index}"));
                var subPath = matchMethod.Result("${subpath}");
                return GenericArguments[index].SelectGenericArg(subPath);
            }

            var matchArg = Regex.Match(path, @"^a(?<index>\d*)(?<subpath>(\[\d*\])*)$");
            if (matchArg.Success)
            {
                var index = int.Parse(matchArg.Result("${index}"));
                var subPath = matchArg.Result("${subpath}");
                return Args[index].SelectGenericArg(subPath);
            }

            throw new ArgumentException(String.Format("Invalid path '{0}'", path));
        }
    }
}
