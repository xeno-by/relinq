using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Relinq.Playground.DataContexts;
using Relinq.Playground.Domain;
using Relinq.Script.Compilation;

namespace Relinq.Playground
{
    [TestFixture]
    public class RelinqSuiteTests
    {
        [SetUp]
        public void SetUp()
        {
            TypeInferenceEngine._sap.ClearHistory();
        }

        [TearDown]
        public void TearDown()
        {
            TypeInferenceEngine._sap.DumpHistory();
        }

        [Test]
        public void TestSimple()
        {
            var some = from c in new TestClientDataContext().Companies
                       where c.Id == String.Empty
                       select c;

            Assert.AreEqual(0, some.ToArray().Count());
        }

        [Test]
        public void TestFullCycle()
        {
            var some = from c in new TestClientDataContext().Companies
                       where c.Employees.Count == 2
                       select new { c.Name, Name2 = new { c.Name } };

            Assert.AreEqual(1, some.ToArray().Count());
        }

        private int thisX = 2;

        [Test]
        public void TestWithClosure()
        {
            MoreComplexTestWithClosure(2);
        }

        private void MoreComplexTestWithClosure(int parX)
        {
            var localX = 2;

            var some = from c in new TestClientDataContext().Companies
                       where thisX * parX * localX == 8
                       select c;

            Assert.AreEqual(3, some.ToArray().Count());
        }

        private delegate int Calculator(int x);
        private int CalculatorMethod(int x){return x*x;}

        [Test]
        public void TestWithDelegateFuncletizer()
        {
            Calculator del = CalculatorMethod;

            var some = from c in new TestClientDataContext().Companies
                       where del(2) == 4
                       select c;

            Assert.AreEqual(3, some.ToArray().Count());
        }

//        [Test]
//        public void DateTimeTest()
//        {
//            var ctx = new TestClientDataContext();
//            var some = from c in ctx.Companies
//                       where c.FoundationDate < DateTime.Today
//                       select c;
//
//            Assert.AreEqual(3, some.ToArray().Count());
//        }

        [Test]
        public void OrderByTest()
        {
            var some = from c in new TestClientDataContext().Companies
                       orderby c.Name ascending
                       select c;

            Assert.AreEqual(3, some.ToArray().Count());
            Assert.AreEqual(some.ElementAt(0).Name, "C1");
        }

        [Test]
        public void TestEnumCompare()
        {
            var some = from c in new TestClientDataContext().Companies
                       where c.DayOfWeek == DayOfWeek.Wednesday
                       select c;

            Assert.AreEqual(1, some.ToArray().Count());
            Assert.AreEqual(some.ElementAt(0).Name, "C2");
        }

        [Test]
        public void TestArrayRoundtrip()
        {
            var some = from c in new TestClientDataContext().Companies
                       where c.LolMethod2(new []{"lol"})
                       select c;

            Assert.AreEqual(3, some.ToArray().Count());
        }

        [Test]
        public void GroupByTest()
        {
            var some = from c in new TestClientDataContext().Companies
                       group c by c.Name into g
                       select g;

            Assert.AreEqual(2, some.ToArray().Count());
        }

        [Test]
        public void TestUnaryPlus()
        {
            var some = from c in new TestClientDataContext().Companies
                       where c.Name.Length == +2
                       select c;

            Assert.AreEqual(3, some.ToArray().Count());
        }

        [Test]
        public void OperatorPrioTest()
        {
            var some = from c in new TestClientDataContext().Companies
                       where (c.Name.Length + 2) * 3 == 12
                       select c;

            Assert.AreEqual(3, some.ToArray().Count());
        }

        [Test]
        public void OperatorsPotpourriAndIndexerTest()
        {
            var some = from c in new TestClientDataContext().Companies
                       where 
                         c.Employees.Count != 0 && 
                         c.Employees[c.Employees.Count - c.Employees.Count].FirstName == "Vassily" &&
                         c[4, 2] == 8
                       select c;

            Assert.AreEqual(1, some.ToArray().Count());
        }


        // todo. why top > Failboats[0] > Inner > Inner > Failboats[0] doesn't contain ActualArgs?

        [Test]
        public void BracketsTest()
        {
            var some =
                from c in new TestClientDataContext().Companies
                let e = c.Employees
                where c.LolMethod(((int?)0) + (c.D == null ? 0 : 1))
                where (e || e) == true
                where e != null && -(-(e.Count)) < 10
                where e != null && ~(~(e.Count)) < 10
                where e != null && -(1 + e.Count) < 10 
                select c;

            Assert.AreEqual(3, some.ToArray().Count());
        }

        public static IQueryable Monster
        {
            get
            {
                // todo. add lifted u/d operators and casts

                var i_nullable = (int?)0;
                IEnumerable<int> ie = new List<int> { 1, 2, 3 };
                var d = new Dictionary<int, string>();
                d.Add(2, "string");
                d.Add(3, "string");

                var some =
                    from c in new TestClientDataContext().Companies
                    let g = new Guid("ee1e42c3-1d16-465a-b2b1-bcdbc645db3b")
                    let n = 2m
                    let i = c.D()
                    let ai = c.Array1D[0]
                    let ai_jagged = c.Array2DJagged[0][0]
                    let ai_rect = c.Array2DRect[0, 0]
                    let mg = (Company.DGetInt)(2 + (c.Employees != null ? 2 : 0)).GetHashCode
                    let e = c.Employees
                    let id = c.Id ?? "\0\x0001\b\r\n\f\t\v\xffff\'\"\\abcdef"
                    let ts = (c.Name + " rocks").ToUpper()
                    let imba = mg() == 4 ? mg : () => 4
                    where imba() == 4
                    where c[4, 2] == 8
                    where c.Description == "Dummy"
                    where (c.Description[0] == 'c') == ('c' > 'a')
                    where c.LolMethod('c')
                    where c.LolMethod(i_nullable + (String.IsNullOrEmpty("") ? 0 : 1))
                    where c.FoundationDate > new DateTime(2000, 1, 1)
                    where (e || e) == true
                    where (c.Employees ?? c.Employees ?? c.Employees) == true
                    where e.Concat(new List<Employee> { new Employee { FirstName = "Dummy" } }).Any(ae => ae.FirstName == "Dummy")
                    where c.LolMethod(0, i, (int)2.0)
                    orderby c.FoundationDate
                    let monster = new
                    {
                        c,
                        i,
                        g,
                        n,
                        ai,
                        ai_jagged,
                        ai_rect,
                        d,
                        ie,
                        id,
                        ts
                    }
                    group monster by monster.c.Name into monsterg
                    select monsterg;

                return some;
            }
        }

        [Test]
        public void RelinqScriptBuilderMonstrousTest()
        {
            var listObj = new List<Object>();
            foreach(var mitem in Monster) listObj.Add(mitem);
            Assert.AreEqual(3, listObj.Count);
        }
    }
}