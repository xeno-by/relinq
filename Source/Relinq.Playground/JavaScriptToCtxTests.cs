namespace Relinq.Playground
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using DataContexts;
    using Domain;
    using NUnit.Framework;
    using Relinq.Linq.Evaluation;

    [TestFixture]
    public class JavaScriptToCtxTests
    {
        protected IEnumerable<T> Read<T>(string js)
        {
            return (IEnumerable<T>) TestTransformationFramework.JSToCSharp(js).Evaluate();
        }

        protected IEnumerable Read(string js)
        {
            return (IEnumerable) TestTransformationFramework.JSToCSharp(js).Evaluate();
        }

        [Test]
        public void JoinTest()
        {
            var db = new TestServerDataContext();
            var expected = db.Users.Join(
                db.Companies,
                user => user.CompanyId,
                company => company.Guid,
                (user, company) => new {user.FirstName, user.LastName, company.Name}
                );

//            expected = from user in db.Users
//                                 join company in db.Companies on user.CompanyId equals company.Guid
//                                 select new { user.FirstName, user.LastName, company.Name };

            var actual = Read(@"
ctx.Users.Join(
    ctx.Companies,
    function(user) { return user.CompanyId },
    function(company) { return company.Guid },
    function(user, company) {return {fName: user.FirstName, lName: user.LastName, cName: company.Name} }
)");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.OfType<object>().Count());

            var ea = expected.ToArray();
            var aa = actual.OfType<object>().ToArray();

            for (var i = 0; i < ea.Length; i++)
            {
                Assert.AreEqual(string.Format("{{ fName = {0}, lName = {1}, cName = {2} }}", ea[i].FirstName, ea[i].LastName, ea[i].Name), aa[i].ToString());
            }
        }

        [Test]
        public void GroupTest()
        {
            var db = new TestServerDataContext();
            var expected = db.Users.GroupBy(user => user.CompanyId).Select(employer => employer);
//            expected = from user in db.Users
//                                 group user by user.CompanyId into employer
//                                 select employer;

            var actual = Read<IGrouping<Guid, User>>(@"
ctx.Users
    .GroupBy(function(u) { return u.CompanyId })
    .Select(function(e) { return e})
)");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count());

            var ea = expected.ToArray();
            var aa = actual.ToArray();

            for (var i = 0; i < ea.Length; i++)
            {
                Assert.AreEqual(ea[i].Key, aa[i].Key);
                Assert.AreEqual(ea[i].Count(), aa[i].Count());
                var ersa = ea[i].ToArray();
                var arsa = aa[i].ToArray();
                for (var j = 0; j < ersa.Length; j++)
                {
                    Assert.AreEqual(ersa[j].Id, arsa[j].Id);
                }
            }

        }
    }
}