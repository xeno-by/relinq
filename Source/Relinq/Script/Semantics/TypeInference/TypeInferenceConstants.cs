using System;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Syntax.Expressions;
using Relinq.Script.Syntax.Operators;
using Relinq.Script.Syntax.SyntaxReference;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.TypeInference
{
    public static class TypeInferenceConstants
    {
        public static RelinqScriptType InferType(ConstantExpression ce)
        {
            if (ce.Token.Text == "null")
            {
                return new Null();
            }
            else if (ce.Token.Text == "false" || ce.Token.Text == "true")
            {
                return typeof(bool);
            }
            else
            {
                if (EcmaScriptV3Syntax.IsInteger(ce.Token))
                {
                    try
                    {
                        try
                        {
                            var value = (long)typeof(long).FromInvariantString(ce.Token.Text);
                            if (long.MinValue <= value && value < int.MinValue)
                            {
                                return typeof(long);
                            }
                            else if (int.MinValue <= value && value <= int.MaxValue)
                            {
                                return typeof(int);
                            }
                            else if (value == (long)int.MaxValue + 1)
                            {
                                var parentIsUnaryMinus = ce.Parent is OperatorExpression &&
                                    ((OperatorExpression)ce.Parent).Type == OperatorType.UnaryMinus;
                                return parentIsUnaryMinus ? typeof(int) : typeof(long);
                            }
                            else if ((long)int.MaxValue + 1 < value && value <= uint.MaxValue)
                            {
                                return typeof(uint);
                            }
                            else if (uint.MaxValue < value && value <= long.MaxValue)
                            {
                                return typeof(long);
                            }
                            else
                            {
                                throw new ArgumentException("Something was overlooked: " +
                                    "no way a long number can have value '" + value + "'.");
                            }
                        }
                        catch (OverflowException)
                        {
                            ulong value;
                            if (ulong.TryParse(ce.Token.Text, out value))
                            {
                                if (value == (ulong)long.MaxValue + 1)
                                {
                                    var parentIsUnaryMinus = ce.Parent is OperatorExpression &&
                                        ((OperatorExpression)ce.Parent).Type == OperatorType.UnaryMinus;
                                    return parentIsUnaryMinus ? typeof(long) : typeof(ulong);
                                }
                                else if ((ulong)long.MaxValue + 1 < value && value <= ulong.MaxValue)
                                {
                                    return typeof(ulong);
                                }
                                else
                                {
                                    throw new ArgumentException("Something was overlooked: " +
                                        "no way an ulong (but not long) number can have value '" + value + "'.");
                                }
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else if (EcmaScriptV3Syntax.IsFloat(ce.Token))
                {
                    try
                    {
                        return typeof(double).FromInvariantString(ce.Token.Text).GetType();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else if (EcmaScriptV3Syntax.IsString(ce.Token))
                {
                    return new UnknownStringizedObject(ce);
                }
                else if (EcmaScriptV3Syntax.IsObject(ce.Token))
                {
                    return new UnknownPropertyBag(ce);
                }
                else if (EcmaScriptV3Syntax.IsArray(ce.Token))
                {
                    return new UnknownList(ce);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
