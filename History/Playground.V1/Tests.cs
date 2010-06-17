using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Playground.Domain;
using Playground.Extensions;
using Playground.V1.Client;
using Relinq.V1;

namespace Playground.V1
{
    [TestFixture]
    public class Tests
    {
        public static IQueryable<Company> ServerData
        {
            get
            {
                var qpimpl = Type.GetType("Playground.V1.Server.RelinqImpl+QueryImpl+QueryProviderImpl+DomainDataDispenser");
                return (IQueryable<Company>)qpimpl.GetProperty("AllCompanies").GetValue(null, null);
            }
        }

        [Test]
        public void TestLolinqWorkflow()
        {
            var result = ServerData.Where(c => c.Employees.Find(e => e.FirstName == "Vassily") != null).Select(c => new { c.Name });
            var count = result.Count();

            Assert.AreEqual(2, count);
        }

        [Test, Ignore]
        public void TestRelinqWorkflow()
        {
            var query = new QueryProxy();
            var result = query.Where(c => c.Employees.Find(e => e.FirstName == "Vassily") != null).Select(c => new { c.Name });
            var count = result.Count();

            Assert.AreEqual(2, count);
        }

        [Test]
        public void TestSerialization()
        {
            var query = new QueryProxy();
            var result = query.Where(c => c.Employees.Find(e => e.FirstName == "Vassily") != null).Select(c => new {c.Name});

            var stream = FrameworkV1.SerializeQuery(result.Expression);
            stream.Dump().Seek(0, SeekOrigin.Begin);
        }

        [Test]
        public void TestSerializeDeserializeSimple()
        {
            var query = new QueryProxy();
            var result = query.Where(c => c.Employees.Count == 2).Select(c => new { c.Name });

            var stream = FrameworkV1.SerializeQuery(result.Expression);
            stream.Dump().Seek(0, SeekOrigin.Begin);

            var ex = FrameworkV1.DeserializeQuery(stream);
        }

        [Test, Ignore]
        public void TestSerializeDeserialize()
        {
            var query = new QueryProxy();
            var result = query.Where(c => c.Employees.Find(e => e.FirstName == "Vassily") != null).Select(c => new { c.Name });

            var stream = FrameworkV1.SerializeQuery(result.Expression);
            stream.Dump().Seek(0, SeekOrigin.Begin);

            var ex = FrameworkV1.DeserializeQuery(stream);
        }

        [Test]
        public void TestSerializeDeserializeExecuteSimple()
        {
            var query = new QueryProxy();
            var clientResult = query.Where(c => c.Employees.Count == 2).Select(c => new { c.Name });

            var stream = FrameworkV1.SerializeQuery(clientResult.Expression);
            stream.Dump().Seek(0, SeekOrigin.Begin);

            var ex = FrameworkV1.DeserializeQuery(stream);

            var ass = Assembly.Load("Playground");
            var qimpl_t = ass.GetType("Playground.V1.Server.RelinqImpl+QueryImpl");
            var qimpl = Activator.CreateInstance(qimpl_t);
            var serverResult = FrameworkV1.ExecuteQueryImpl(ex, (IQueryable)qimpl);

            var count = 0;
            foreach(var serverResultEntry in (IEnumerable)serverResult) ++count;
            Assert.AreEqual(1, count);
        }
    }
}
