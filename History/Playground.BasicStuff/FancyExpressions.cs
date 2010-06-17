using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Playground.BasicStuff.Queryable;

namespace Playground.BasicStuff
{
    public static class FancyExpressions
    {
        public static IQueryable InitialExample
        {
            get
            {
                ParameterExpression ph, ph2;
                return ("adasds".AsQueryable<char>())
                    .Where<char>(
                    Expression.Lambda<Func<char, bool>>(
                        Expression.Equal(
                            Expression.Convert(
                                ph = Expression.Parameter(typeof(char), "c"),
                                typeof(int)),
                            Expression.Constant(0x61, typeof(int))),
                        new ParameterExpression[] { ph }))
                    .Select<char, char>(
                    Expression.Lambda<Func<char, char>>(
                        ph2 = Expression.Parameter(typeof(char), "c"),
                        new ParameterExpression[] { ph2 }));
            }
        }

        public class MyQueryProvider : AbstractQueryProvider
        {
            public override string GetQueryText(Expression expression)
            {
                return "(MyQP)";
            }

            public override object Execute(Expression expression)
            {
//                Debugger.Break();
                throw new NotImplementedException();
            }
        }

        public static IQueryable ViaQueryable
        {
            get
            {
                var hello = new Queryable.Query<string>(new MyQueryProvider());
                return hello.Where(s => s.Length > 2).Select(s => new { String = s, s.Length });
            }
        }

//        public static IQueryable WithAnonymousTypes
//        {
//            get
//            {
////                var hello =
//////                    new Query<string>(new MyQueryProvider());
////                      (new[]{"aa", "aaa", "aaaa"}).AsQueryable();
////                return hello.Where(s => s.Length > 2).Select(s => new { String = s, s.Length });
//
//                ParameterExpression pe1, pe2;
////                return new Query<string>(new MyQueryProvider())
//                return (new[]{"aa", "aaa", "aaaa"}).AsQueryable()
//                    .Where<string>(
//                    Expression.Lambda<Func<string, bool>>(
//                        Expression.GreaterThan(
//                            Expression.Property(
//                                pe1 = Expression.Parameter(typeof(string), "s"), 
//                                typeof(string).GetMethod("get_Length")
//                                ), 
//                            Expression.Constant(2, typeof(int))
//                            ),
//                        new ParameterExpression[] { pe1 }
//                        )
//                    )
//                    .Select<String, Tuple2<String, int>>(
//                    Expression.Lambda<Func<String, Tuple2<String, int>>>(
//                        Expression.New(
//                            typeof(Tuple2<String, int>).GetConstructors()[0], 
//                            new Expression[] { 
//                                 pe2 = Expression.Parameter(typeof(string), "s"),
//                                 Expression.Property(pe2, typeof(string).GetMethod("get_Length"))
//                                 },
//                            new MethodInfo[]{
//                                typeof(Tuple2<String, int>).GetMethod("get_Field0"), 
//                                typeof(Tuple2<String, int>).GetMethod("get_Field1")
//                                }
//                            ),
//                        new ParameterExpression[] { pe2 }
//                        )
//                    );
//            }
//        }
    }
}