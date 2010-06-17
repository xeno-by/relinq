using System;
using NUnit.Framework;

namespace Playground.BasicStuff
{
    [TestFixture]
    public class FunkyTests
    {
        [Test]
        public void TestInitialExample()
        {
            var a = 0;
            foreach (var c in FancyExpressions.InitialExample) ++a;
            Assert.AreEqual(2, a);
        }

//        [Test]
//        public void TestWithAnonymousTypes()
//        {
//            var a = 0;
//            foreach (var c in FancyExpressions.WithAnonymousTypes) ++a;
//            Assert.AreEqual(2, a);
//        }

        [Test, ExpectedException(typeof(NotImplementedException))]
        public void TestHowIQueryableWorks()
        {
            var a = 0;
            foreach (var c in FancyExpressions.ViaQueryable) ++a;
            Assert.AreEqual(2, a);
        }
    }
}