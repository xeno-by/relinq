using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using Relinq.Helpers.Collections;
using Relinq.Linq.Evaluation;
using Relinq.Playground.Domain;
using Relinq.Script.Compilation;

namespace Relinq.Playground
{
    // todo. most of tests that fail here are TODOs

    [TestFixture]
    public class RelinqScriptCompilerTests
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
        public void OffTest()
        {
            var js = "(ctx.Dummy + 2) * 3 == 12";
            TestTransformationFramework.JSToCSharp(js);
        }

        [Test]
        public void TestFirstFailureOfAlpha1()
        {
            var js = "ctx.Companies"+
                ".Where(function(c){return (c.Name.Length + 2) * 3 == 12})"+
                ".Select(function(c){return c;})";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(3, ((IEnumerable<Company>)et.Evaluate()).Count());
        }

        [Test]
        public void SimpleTest1()
        {
            var js = "(3 + 2 * 2).ToString() + 'abc'";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual("7abc", et.Evaluate());
        }

        [Test]
        public void SimpleTest2()
        {
            var js = "1.ToString()";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual("1", et.Evaluate());
        }

        [Test]
        public void SimpleTest3()
        {
            var js = "1 + 'a'";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual("1a", et.Evaluate());
        }

        [Test]
        public void SimpleTest4()
        {
            var js = "'a' + 1";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual("a1", et.Evaluate());
        }

        [Test]
        public void SimpleTest5()
        {
            var js = "(ctx.Companies.ElementAt(0).Employees & ctx.Companies.ElementAt(0).Employees).Count()";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(2, et.Evaluate());
        }

        [Test]
        public void SimpleTest6()
        {
            var js = "1.6 * 2.5";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(4, et.Evaluate());
        }

        [Test]
        public void SimpleTest7()
        {
            var js = "(ctx.Companies.ElementAt(0).Employees || ctx.Companies.ElementAt(0).Employees).Count()";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(2, et.Evaluate());
        }

        [Test]
        public void SimpleTest8()
        {
            var tf = new TransformationFramework();
            tf.Integration.RegisterJS("iec", new List<Company>());

            var js = "iec.Where(function(c){return c.Name=='a'}).Select(function(c){return c.Name})";
            var et = tf.JSToCSharp(js);

            Expression<Func<IEnumerable<Company>, IEnumerable<String>>> linq = iec =>
                from c in iec where c.Name == "a" select c.Name;

            var relinq = et.Evaluate();
            var native = linq.Compile()(new List<Company>());
        }

        [Test]
        public void SimpleTest9()
        {
            var js = "ctx.Companies.ElementAt(0).Employees.Select(function(e){return e.FirstName})";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual("Vassily, Piotr", ((IEnumerable<String>)et.Evaluate()).StringJoin());
        }

        public class ItemFactory
        {
            private static int counter = 0;
            public int Item() { return ++counter; }
        }

        [Test]
        public void SimpleTest10()
        {
            var tf = new TransformationFramework();
            tf.Integration.RegisterJS("items", new List<ItemFactory>{new ItemFactory(), new ItemFactory()});

            var js = "items.Select(function(c){return {I:c.Item()}})";
            var result = tf.JSToCSharp(js).Evaluate();

            var ints = new List<int>();
            foreach(var anon in (IEnumerable)result)
                ints.Add((int)anon.GetType().GetProperty("I").GetValue(anon, null));

            Assert.IsTrue(ints.SequenceEqual(Enumerable.Range(1, 2)));
        }

        [Test]
        public void SimpleTest11()
        {
            // todo. consider adding IEnumerable[int] instead of ElementAt
            // todo. if so, update the spec + roundtrip issues
            var js = "ctx.Companies[0].Employees.Select(function(e){return e.FirstName})";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual("Vassily, Piotr", ((IEnumerable<String>)et.Evaluate()).StringJoin());
        }

        [Test]
        public void SimpleTest12()
        {
            var js = "ctx.Companies.ElementAt(0).Employees.Select(true ? function(e){return e.FirstName} : function(e){return e.LastName})";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual("Vassily, Piotr", ((IEnumerable<String>)et.Evaluate()).StringJoin());
        }

        [Test]
        public void SimpleTest13()
        {
            // todo. this shouldn't get compiled according to the C# compiler but why not
            // reason: U -> S lambda doesn't fit U -> S? delegate parameter
            var js = "ctx.Companies.Where(function(c){return c.LolMethod10a(c, function(c1){return c1.DayOfWeek;});}).Count()";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(3, et.Evaluate());
        }

        [Test]
        public void SimpleTest14()
        {
            var js = "ctx.Companies.Where(function(c){return c.LolMethod9(c, c.LolMethod9a);}).Count()";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(3, et.Evaluate());
        }

        [Test]
        public void SimpleTest15()
        {
            var js = "2 + null";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(null, et.Evaluate());
        }

        [Test]
        public void SimpleTest16()
        {
            var js = "2 * null";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(null, et.Evaluate());
        }

        [Test]
        public void SimpleTest17()
        {
            var js = "ctx.Companies.ElementAt(0).LolMethod12(function(c){return c.Name.Length;})";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(true, et.Evaluate());
        }

        [Test]
        public void SimpleTest18()
        {
            var js = "ctx.Companies.Where(function(c){return c.Guid == '4ba1a9c5-9c72-4420-bde5-f3f498cff494';}).Count()";
            var et = TestTransformationFramework.JSToCSharp(js);
            Assert.AreEqual(1, et.Evaluate());
        }
    }
}