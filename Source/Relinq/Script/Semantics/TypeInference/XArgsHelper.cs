using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Compilation;

namespace Relinq.Script.Semantics.TypeInference
{
    public static class XArgsHelper
    {
        // converts invocation args to an x-form: head+body+tail:
        // head: info about this (null for regular methods, p[0] for extensions).
        // body: arguments of the signature except the first for extensions and last for varargs.
        // tail: either array or expanded form of the varargs parameter (depending on xargc).
        public static IEnumerable<Type> ToXArgs(this MethodInfo alt, int xargc)
        {
            var ptypes = alt.GetParameters().Select(pi => pi.ParameterType);
            var head = (alt.IsExtension() ? ptypes.First() : typeof(This)).AsArray();

            var body = !alt.IsExtension() ? ptypes : ptypes.Skip(1);
            if (alt.IsVarargs()) body = body.Take(body.Count() - 1);

            var tail = Enumerable.Empty<Type>();
            if (alt.IsVarargs())
            {
                var hbCount = head.Concat(body).Count();
                tail = xargc <= hbCount ? ptypes.Last().AsArray() : 
                    Enumerable.Repeat(ptypes.Last().GetElementType(), xargc - hbCount);
            }

            return head.Concat(body).Concat(tail);
        }

        public static IEnumerable<int> XArgsCount(this MethodInfo alt)
        {
            var from = alt.ToXArgs(int.MinValue).Count();
//            var to = alt.ToXArgs(int.MaxValue).Count();
            var to = alt.ToXArgs(16).Count();
            return Enumerable.Range(from, to - from + 1);
        }

        public static IEnumerable<RelinqScriptType> ToXArgs(
            this RelinqScriptExpression root, TypeInferenceCache inf)
        {
            // cba to duplicate validating logics here
            // see todo in CSharpExpressionTreeBuilder

            if (!root.IsCall())
            {
                throw new NotSupportedException(String.Format(
                    "X-args can only be acquired for call-type nodes. " +
                    "Target node '{0}' is unsuitable for this purpose.", root));
            }
            else
            {
                if (root is InvokeExpression)
                {
                    var ie = (InvokeExpression)root;

                    // head is either an irrelevant type (if its a delegate invocation)
                    // or a source of the method group (if its a normal invocation)
                    yield return inf[ie.Target] is ClrType ? 
                        new This() :
                        inf[((MemberAccessExpression)ie.Target).Target];

                    foreach(var arg in ie.Args) yield return inf[arg]; // body + tail
                }
                else if (root is IndexerExpression)
                {
                    var ie = (IndexerExpression)root;
                    yield return inf[ie.Target]; // head is not null, since indexers are instance methods
                    foreach (var operand in ie.Operands) yield return inf[operand]; // body + tail
                }
                else if (root is OperatorExpression)
                {
                    var ie = (OperatorExpression)root;
                    yield return null; // head is null, since the call is static
                    foreach (var operand in ie.Operands) yield return inf[operand]; // body + tail
                }
                else
                {
                    throw new NotSupportedException(String.Format(
                        "Something was overlooked: there's no way a call-type node " +
                        "can be the following: '{0}'.", root));
                }
            }
        }

        public static int XArgsCount(this RelinqScriptExpression root)
        {
            // cba to duplicate validating logics here
            // see todo in CSharpExpressionTreeBuilder

            if (!root.IsCall())
            {
                throw new NotSupportedException(String.Format(
                    "X-args can only be acquired for call-type nodes. " +
                    "Target node '{0}' is unsuitable for this purpose.", root));
            }
            else
            {
                if (root is InvokeExpression)
                {
                    var ie = (InvokeExpression)root;
                    return 1 + ie.Args.Count();
                }
                else if (root is IndexerExpression)
                {
                    var ie = (IndexerExpression)root;
                    return 1 + ie.Operands.Count();
                }
                else if (root is OperatorExpression)
                {
                    var oe = (OperatorExpression)root;
                    return 1 + oe.Operands.Count();
                }
                else
                {
                    throw new NotSupportedException(String.Format(
                        "Something was overlooked: there's no way a call-type node " +
                        "can be the following: '{0}'.", root));
                }
            }
        }
    }
}
