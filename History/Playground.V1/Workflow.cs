using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Playground.BasicStuff.Queryable;
using Playground.Domain;
using Playground.V1.Server;
using Relinq.V1;

namespace Playground.V1
{
    namespace Client
    {
        public class QueryProxy : Query<Company>
        {
            public QueryProxy()
                : base(new QueryProviderProxy())
            {
            }

            private class QueryProviderProxy : AbstractQueryProvider
            {
                public override string GetQueryText(Expression expression)
                {
                    return "(Proxy)";
                }

                public override object Execute(Expression expression)
                {
                    var serialized = FrameworkV1.SerializeQuery(expression);
                    var result = RelinqRemoteTransport.Relinq(serialized);
                    return FrameworkV1.DeserializeResult(result);
                }

                private static class RelinqRemoteTransport
                {
                    public static Stream Relinq(Stream serialized)
                    {
                        return RelinqImpl.Relinq(serialized);
                    }
                }
            }
        }
    }

    namespace Server
    {
        public static class RelinqImpl
        {
            public static Stream Relinq(Stream serialized)
            {
                var expression = FrameworkV1.DeserializeQuery(serialized);
                var q = new QueryImpl(expression);
                return FrameworkV1.SerializeResult(q);
            }

            private class QueryImpl : Query<Company>
            {
                // Used via reflection by QueryProxyReference
                public QueryImpl()
                    : base(new QueryProviderImpl())
                {
                }

                public QueryImpl(Expression expression)
                    : base(new QueryProviderImpl(), expression)
                {
                }

                private class QueryProviderImpl : AbstractQueryProvider
                {
                    public override string GetQueryText(Expression expression)
                    {
                        return "(Impl)";
                    }

                    public override object Execute(Expression expression)
                    {
                        return FrameworkV1.ExecuteQueryImpl(expression, DomainDataDispenser.AllCompanies);
                    }

                    private static class DomainDataDispenser
                    {
                        public static IQueryable<Company> AllCompanies
                        {
                            get
                            {
                                var companies = new List<Company>();
                                companies.Add(new Company { Name = "C1" });
                                companies.Add(new Company { Name = "C2" });

                                companies[0].Employees.Add(new Employee { FirstName = "Vassily", LastName = "Pupkeen" });
                                companies[0].Employees.Add(new Employee { FirstName = "Piotr", LastName = "Tapkin" });
                                companies[1].Employees.Add(companies[0].Employees[0]);

                                return companies.AsQueryable();
                            }
                        }
                    }
                }
            }
        }
    }
}
