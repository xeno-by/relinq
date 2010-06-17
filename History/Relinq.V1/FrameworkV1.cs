using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Relinq.V1.Xml;

namespace Relinq.V1
{
    public static class FrameworkV1
    {
        public static Stream SerializeQuery(Expression e)
        {
            var xmlex = XmlExpression.FromExpression(e);

            var sb = new StringBuilder();
            new XmlSerializer(typeof(XmlExpression)).Serialize(new StringWriter(sb), xmlex);
            
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(sb.ToString());
            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static Expression DeserializeQuery(Stream s)
        {
            var str = new StreamReader(s).ReadToEnd();
            var xmlex = (XmlExpression)new XmlSerializer(typeof(XmlExpression)).Deserialize(
                                           XmlReader.Create(new StringReader(str)));
            return XmlExpression.ToExpression(xmlex);
        }

        public static object ExecuteQueryImpl(Expression e, IQueryable host)
        {
            return Evaluate(e, host);
        }

        private static object Evaluate(Expression e, IQueryable host)
        {
            if (e == null)
            {
                return null;
            }
            
            if (e is ConstantExpression)
            {
                var ce = (ConstantExpression)e;
                return ce.Type.Name.EndsWith("QueryImpl") ? host : ce.Value;
            }

            if (e is MethodCallExpression)
            {
                var mce = (MethodCallExpression)e;
                var target = Evaluate(mce.Object, host);

                var args = new List<object>();
                for (var i = 0; i < mce.Method.GetParameters().Length; i++)
                {
                    var pi = mce.Method.GetParameters()[i];
                    var arg = mce.Arguments[i];

                    if (typeof(LambdaExpression).IsAssignableFrom(pi.ParameterType) &&
                        arg is LambdaExpression)
                    {
                        args.Add(arg);
                    }
                    else
                    {
                        args.Add(Evaluate(arg, host));
                    }
                }

                return mce.Method.Invoke(target, args.ToArray());
            }

            if (e is UnaryExpression)
            {
                var ue = (UnaryExpression)e;
                if (ue.NodeType == ExpressionType.Quote)
                {
                    return ue.Operand;
                }
            }

            if (e is MemberExpression)
            {
                var me = (MemberExpression)e;
                var target = Evaluate(me.Expression, host);
                
                if (me.Member is FieldInfo)
                {
                    return ((FieldInfo)me.Member).GetValue(target);
                }

                if (me.Member is PropertyInfo)
                {
                    return ((PropertyInfo)me.Member).GetValue(target, null);
                }
            }

            if (e is BinaryExpression)
            {
                var be = (BinaryExpression)e;
                var left = Evaluate(be.Left, host);
                var right = Evaluate(be.Right, host);

                switch (e.NodeType)
                {
                    case ExpressionType.Equal:
                        return Object.Equals(left, right);

                    case ExpressionType.GreaterThan:
                        Debugger.Break();
                        break;

                    case ExpressionType.NotEqual:
                        return !Object.Equals(left, right);
                }
            }

            if (e is NewExpression)
            {
                var ne = (NewExpression)e;
                Debugger.Break();
            }

            if (e is LambdaExpression)
            {
                var le = (LambdaExpression)e;
                return le.Compile();
            }

            throw new NotSupportedException(String.Format("Expression '{0}' is not supported", e));
        }

        public static Stream SerializeResult(object q)
        {
            throw new NotImplementedException();
        }

        public static object DeserializeResult(Stream s)
        {
            throw new NotImplementedException();
        }
    }
}