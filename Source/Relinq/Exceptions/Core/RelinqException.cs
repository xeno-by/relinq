using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Antlr.Runtime.Tree;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Strings;
using Relinq.Script.Compilation.EngineAspects;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Exceptions.Core
{
    public abstract class RelinqException : ApplicationException
    {
        [IncludeInMessage]
        public Guid Id { get; private set; }

        [IncludeInMessage]
        public abstract bool IsUnexpected { get; }

        protected RelinqException()
            : this(null)
        {
        }

        protected RelinqException(Exception innerException)
            : base(null, innerException)
        {
            Id = Guid.NewGuid();
        }

        public Dictionary<String, Object> Properties
        {
            get
            {
                var props = GetType().GetProperties(BF.All).Where(p => p.HasAttr<IncludeInMessageAttribute>());
                return props.ToDictionary(p => p.Name, p => p.GetValue(this, null));
            }
        }

        private int PPDepth
        {
            get
            {
                var frames = new StackTrace().GetFrames();
                return frames.Count(f => f.GetMethod() == typeof(RelinqException).GetProperty("PrettyProperties", BF.All).GetGetMethod(true));
            }
        }

        internal Dictionary<String, String> PrettyProperties
        {
            get
            {
                var props = GetType().GetProperties(BF.All).Where(p => p.HasAttr<IncludeInMessageAttribute>());

                var vals = props.ToDictionary(p => p.Name, p => p.GetValue(this, null));
                vals = vals.Where(kvp => kvp.Value != null).ToDictionary();

                var valStrings = new Dictionary<String, String>();
                foreach (var kvp in vals)
                {
                    var nativeList = kvp.Value is IEnumerable && !(kvp.Value is String);
                    var list = nativeList ? ((IEnumerable)kvp.Value).Cast<Object>() : Enumerable.Empty<Object>();
                    list = kvp.Value.AsArray().Concat(list);

                    var minDepth = 0;
                    if (kvp.Value is IEnumerable<TypeInferenceHistoryEntry>)
                    {
                        var history = (IEnumerable<TypeInferenceHistoryEntry>)kvp.Value;
                        minDepth = history.Min(e => e.Expression.Depth());
                    }

                    Func<Object, bool> isKvp = o => o != null &&
                        o.GetType().SameMetadataToken(typeof(KeyValuePair<,>));
                    Func<Object, Object> getKey = o =>
                        isKvp(o) ? o.GetType().GetProperty("Key").GetValue(o, null) : null;
                    Func<Object, Object> getValue = o =>
                        isKvp(o) ? o.GetType().GetProperty("Value").GetValue(o, null) : null;

                    Func<Object, String> valtos = o => null;
                    valtos = o => // hello recursion in lambdas
                    {
                        if (o is CommonTree)
                        {
                            return ((CommonTree)o).ToStringTree();
                        }
                        else if (o is TypeInferenceHistoryEntry)
                        {
                            var entry = (TypeInferenceHistoryEntry)o;
                            var depth = entry.Expression.Depth() - minDepth;
                            return new String(' ', 2 * depth) + o;
                        }
                        else if (o is RelinqException)
                        {
                            var rex = (RelinqException)o;
                            var indent = new String(' ', PPDepth * 2);
                            return String.Format("{0}{1}ExceptionType: {2}{3}",
                                Environment.NewLine, indent, rex.GetType(), rex.Message);
                        }
                        else if (o is MethodInfo)
                        {
                            return ((MethodInfo)o).ToComprehensiveString();
                        }
                        else if (o is Type)
                        {
                            return ((Type)o).ToComprehensiveString();
                        }
                        else if (o is ICustomAttributeProvider)
                        {
                            return ((ICustomAttributeProvider)o).ToComprehensiveString();
                        }
                        else if (isKvp(o))
                        {
                            return valtos(getValue(o));
                        }
                        else
                        {
                            return o == null ? "null" : o.ToString();
                        }
                    };

                    var index = 0;
                    foreach (var listEl in list)
                    {
                        if (index == 0)
                        {
                            valStrings.Add(kvp.Key, valtos(listEl));
                        }
                        else
                        {
                            var key = String.Format("{0}[{1}]", kvp.Key, index - 1);
                            if (isKvp(listEl))
                            {
                                var indent = new String(' ', PPDepth * 2);
                                var kvp_valtos = valtos(getValue(listEl));
                                kvp_valtos = kvp_valtos.Replace(Environment.NewLine + indent, Environment.NewLine + indent + "  ");

                                var valString = String.Format("{0}{1}Key: {2}{0}{1}Value: {3}",
                                    Environment.NewLine, indent, valtos(getKey(listEl)), kvp_valtos);
                                valStrings.Add(key, valString);
                            }
                            else
                            {
                                valStrings.Add(key, valtos(listEl));
                            }
                        }

                        ++index;
                    }
                }

                if (PPDepth > 1 && InnerException != null)
                {
                    if (!(InnerException is RelinqException))
                    {
                        var indent = new String(' ', PPDepth * 2);
                        var valString = String.Format("{0}{1}Type: {2}{0}{1}Message: {3}",
                            Environment.NewLine, indent, InnerException.GetType(),
                            InnerException.Message);
                        valStrings.Add("InnerException", valString);
                    }
                    else
                    {
                        var indent = new String(' ', PPDepth * 2);
                        var valString = String.Format("{0}{1}ExceptionType: {2}{3}",
                            Environment.NewLine, indent, InnerException.GetType(),
                            InnerException.Message);
                        valStrings.Add("InnerException", valString);
                    }
                }

                return valStrings;
            }
        }

        public sealed override string Message
        {
            get
            {
                var indent = new String(' ', PPDepth * 2);
                var valStrings = PrettyProperties;
                var mix = valStrings.Select(kvp => String.Format("{0}{2}: {3}{1}", 
                    indent, Environment.NewLine, kvp.Key, kvp.Value));
                var @out = Environment.NewLine + mix.StringJoin(String.Empty);

                var nl = Environment.NewLine;
                var doubleNl = nl + nl;
                while (@out.Contains(doubleNl))
                {
                    @out = @out.Replace(doubleNl, nl);
                }

                return @out;
            }
        }
    }
}