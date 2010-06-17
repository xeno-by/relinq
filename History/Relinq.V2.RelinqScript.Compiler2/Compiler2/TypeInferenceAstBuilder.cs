using System;
using System.Collections.Generic;
using Relinq.V2.RelinqScript.Compiler2.Expressions;
using Relinq.V2.Helpers;
using System.Linq;
using RelinqExpressionType = Relinq.V2.RelinqScript.Grammar.Expressions.ExpressionType;

namespace Relinq.V2.RelinqScript.Compiler2
{
    public class TypeInferenceAstBuilder
    {
        public TypeInferenceUnit Visit(Grammar.Expressions.RelinqScriptExpression e)
        {
            if (e == null)
            {
                return null;
            }
            else
            {
                switch (e.NodeType)
                {
                    case RelinqExpressionType.Keyword:
                        var ke = (Grammar.Expressions.KeywordExpression)e;
                        return new KeywordExpression(ke.Name);

                    case RelinqExpressionType.Variable:
                        var ve = (Grammar.Expressions.VariableExpression)e;
                        return new VariableExpression(ve.Name);

                    case RelinqExpressionType.Constant:
                        var le = (Grammar.Expressions.ConstantExpression)e;
                        return new LiteralExpression(le.Data);

                    case RelinqExpressionType.New:
                        var je = (Grammar.Expressions.NewExpression)e;
                        var dic = new Dictionary<String, TypeInferenceUnit>();
                        je.Props.ForEach(kvp => dic.Add(kvp.Key, Visit(kvp.Value)));
                        return new JsonExpression(dic);

                    case RelinqExpressionType.Lambda:
                        var lae = (Grammar.Expressions.LambdaExpression)e;
                        return new LambdaExpression(lae.Args, Visit(lae.Body));

                    case RelinqExpressionType.Field:
                        var fe = (Grammar.Expressions.FieldExpression)e;
                        return new FieldExpression(fe.Name, Visit(fe.Target));

                    case RelinqExpressionType.Call:
                        var ce = (Grammar.Expressions.CallExpression)e;

                        // To express parameter conversion semantics we prepare N auxiliary conversion expressions
                        // One might think that target expression also needs conversion stuff 
                        // when the method is called in extension style. However that's unnecessary 
                        // since GetMethods dispenser will be set up to yield extension methods 
                        // in form of forged instance methods.
                        //
                        // Conversion calls have special name that matches both "op_Implicit" and "op_Explicit"
                        // To provide unified approach to conversions, inheritance-based coercions 
                        // are treated in a special way: when resolver will ask ReflectionHelper for 
                        // conversion operators they will be forged and yielded as "op_Implicit" 
                        // dynamic methods along with real ops.

                        // upd. Commenting this so far. Nicely working method alternative resolution
                        // based on type alternatives + conversion nodes for targets/args
                        // will be implemented only in v4.

//                        var argConversions = 
//                            ce.Args.Select(arg => new ConversionExpression(Visit(arg))).ToArray();
//                        return new CallExpression(ce.Name, Visit(ce.Target), argConversions);

                        // todo: jeez.. wtf
                        var args = ce.Args.Select(arg => Visit(arg)).ToArray();
                        return new CallExpression(ce.Name, Visit(ce.Target), args);

                    case RelinqExpressionType.Conditional:
                        var te = (Grammar.Expressions.ConditionalExpression)e;

                        // todo: insert conversion nodes for ifTrue and ifFalse expressions
                        return new ConditionalExpression(Visit(te.Condition), Visit(te.IfTrue), Visit(te.IfFalse));

                    default:
                        throw new NotSupportedException(e.ToString());
                }
            }
        }
    }
}
