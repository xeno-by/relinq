using System;
using System.Linq;
using Antlr.Runtime;
using NUnit.Framework;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.JSToCSharp;
using Relinq.Exceptions.JSToCSharp.Parser;
using Relinq.Helpers.Reflection;

namespace Relinq.Playground
{
    [TestFixture]
    public class RelinqScriptParserExceptionsTests
    {
        [Test]
        public void TestSyntaxError_1()
        {
            try
            {
                TestTransformationFramework.JSToCSharp("2 +* 2;");
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("SyntaxError", ex0_kvps["Type"]);
                Assert.AreEqual("2 +* 2;", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(SyntaxErrorException), ex1.GetType());
                Assert.IsNotNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("SyntaxError", ex1_kvps["Type"]);
                Assert.AreEqual("1: 2 +[ERROR>>> *] 2;", ex1_kvps["SourceCode"]);
                Assert.AreEqual("1", ex1_kvps["LineNumber"]);
                Assert.AreEqual("3", ex1_kvps["CharPositionInLine"]);
                Assert.AreEqual("[3..4]", ex1_kvps["ErrorSpan"]);
                Assert.AreEqual("line 1:3 no viable alternative at input '*'", ex1_kvps["AntlrMessage"]);

                var ex2 = ex1.InnerException;
                Assert.AreEqual(typeof(NoViableAltException), ex2.GetType());
                Assert.IsNull(ex2.InnerException);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestSyntaxError_2()
        {
            try
            {
                var e = TestTransformationFramework.JSToCSharp("a.Contains{'b'}");
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("SyntaxError", ex0_kvps["Type"]);
                Assert.AreEqual("a.Contains{'b'}", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(SyntaxErrorException), ex1.GetType());
                Assert.IsNotNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("SyntaxError", ex1_kvps["Type"]);
                Assert.AreEqual("1: a.Contains[ERROR>>> {]'b'}", ex1_kvps["SourceCode"]);
                Assert.AreEqual("1", ex1_kvps["LineNumber"]);
                Assert.AreEqual("10", ex1_kvps["CharPositionInLine"]);
                Assert.AreEqual("[10..11]", ex1_kvps["ErrorSpan"]);
                Assert.AreEqual("line 1:10 no viable alternative at input '{'", ex1_kvps["AntlrMessage"]);

                var ex2 = ex1.InnerException;
                Assert.AreEqual(typeof(NoViableAltException), ex2.GetType());
                Assert.IsNull(ex2.InnerException);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestUnsupportedJsConstruct()
        {
            try
            {
                var e = TestTransformationFramework.JSToCSharp(@"try{}finally{}");
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("UnsupportedJsConstruct", ex0_kvps["Type"]);
                Assert.AreEqual("try{}finally{}", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(SemanticErrorException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("UnsupportedJsConstruct", ex1_kvps["Type"]);
                Assert.AreEqual("(try BLOCK (finally BLOCK))", ex1_kvps["Node"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestJsShouldBeSingleExpression()
        {
            try
            {
                var e = TestTransformationFramework.JSToCSharp("2;a*2;");
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("JsShouldBeSingleExpression", ex0_kvps["Type"]);
                Assert.AreEqual("2;a*2;", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(SemanticErrorException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("JsShouldBeSingleExpression", ex1_kvps["Type"]);
                Assert.AreEqual("2 (* a 2)", ex1_kvps["Node"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestArrayLiteralsShouldBeCompileTimeConstants()
        {
            try
            {
                var e = TestTransformationFramework.JSToCSharp("[a, 5, 'xyz']");
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("ArrayLiteralsShouldBeCompileTimeConstants", ex0_kvps["Type"]);
                Assert.AreEqual("[a, 5, 'xyz']", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(SemanticErrorException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("ArrayLiteralsShouldBeCompileTimeConstants", ex1_kvps["Type"]);
                Assert.AreEqual("(ARRAY (ITEM a) (ITEM 5) (ITEM 'xyz'))", ex1_kvps["Node"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }
    }
}