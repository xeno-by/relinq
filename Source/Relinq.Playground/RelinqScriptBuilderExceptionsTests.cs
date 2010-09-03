using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.CSharpToJS;
using Relinq.Exceptions.Integration;
using Relinq.Exceptions.JsonSerializer;
using Relinq.Exceptions.LinqVisitor;
using Relinq.Infrastructure.Client.Beans;
using Relinq.Playground.DataContexts;
using Relinq.Playground.Domain;

namespace Relinq.Playground
{
    [TestFixture]
    public class RelinqScriptBuilderExceptionsTests
    {
        [Test]
        public void TestIntegration()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    where c.Employees.Count == 2
                    select c;

                var tf = new TransformationFramework();
                tf.Integration.AllowDuplicateRules = false;
                tf.Integration.RegisterCSharp(o => o is IBean, o => "bean");
                tf.Integration.RegisterCSharp(o => o is IBean, o => "bean2");
                tf.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("Integration", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (c.Employees.Count = 2))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(LinqVisitorException), ex1.GetType());
                Assert.IsNotNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (c.Employees.Count = 2))", ex1_kvps["Root"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company])", ex1_kvps["Expression"]);
                Assert.AreEqual("Constant", ex1_kvps["ExpressionType"]);

                var ex2 = ex1.InnerException;
                Assert.AreEqual(typeof(CSharpToJSIntegrationException), ex2.GetType());
                Assert.IsNull(ex2.InnerException);

                var ex2_kvps = ((RelinqException)ex2).PrettyProperties;
                Assert.AreEqual("Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]", ex2_kvps["CSharpObject"]);
                Assert.AreEqual("Relinq.Playground.Domain.Company", ex2_kvps["CSharpObject[0]"]);
                Assert.AreEqual("Relinq.Playground.Domain.Company", ex2_kvps["CSharpObject[1]"]);
                Assert.AreEqual("Relinq.Playground.Domain.Company", ex2_kvps["CSharpObject[2]"]);
                Assert.AreEqual("MultipleSuitableRules", ex2_kvps["Type"]);
                Assert.AreEqual("Relinq.Script.Integration.IntegrationContext", ex2_kvps["Context"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestJsonSerialization()
        {
            try
            {
                IComparable<Company> hs = new Company();
                var some =
                    from c in new TestClientDataContext().Companies
                    where hs.CompareTo(c) != 0
                    select c;

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("JsonSerialization", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (value(Relinq.Playground.RelinqScriptBuilderExceptionsTests+<>c__DisplayClass8).hs.CompareTo(c) != 0))", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(LinqVisitorException), ex1.GetType());
                Assert.IsNotNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (value(Relinq.Playground.Domain.Company).CompareTo(c) != 0))", ex1_kvps["Root"]);
                Assert.AreEqual("value(Relinq.Playground.Domain.Company)", ex1_kvps["Expression"]);
                Assert.AreEqual("Constant", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                var ex2 = ex1.InnerException;
                Assert.AreEqual(typeof(JsonSerializationException), ex2.GetType());
                Assert.IsNull(ex2.InnerException);

                var ex2_kvps = ((RelinqException)ex2).PrettyProperties;
                Assert.AreEqual("ActualVsExpectedTypeMismatch", ex2_kvps["Type"]);
                Assert.AreEqual("Relinq.Playground.Domain.Company", ex2_kvps["Root"]);
                Assert.AreEqual("Relinq.Playground.Domain.Company", ex2_kvps["RootActualType"]);
                Assert.AreEqual("System.IComparable`1[Relinq.Playground.Domain.Company]", ex2_kvps["RootExpectedType"]);
                Assert.AreEqual("Relinq.Playground.Domain.Company", ex2_kvps["Target"]);
                Assert.AreEqual("Relinq.Playground.Domain.Company", ex2_kvps["ActualType"]);
                Assert.AreEqual("System.IComparable`1[Relinq.Playground.Domain.Company]", ex2_kvps["ExpectedType"]);
                Assert.AreEqual("Serialization metadata for type 'System.IComparable`1[[Relinq.Playground.Domain.Company, Relinq.Playground, Version=0.0.0.0, Culture=neutral, PublicKeyToken=ab3a3fb55cfc23d5]]': IsList = False, IsDictionary = False, IsPropertyBag = False, Properties = ", ex2_kvps["Metadata"]);
                Assert.AreEqual("False", ex2_kvps["IsUnexpected"]);


                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestUnsupportedNodeType_Checked()
        {
            try
            {
                checked
                {
                    var some =
                        from c in new TestClientDataContext().Companies
                        where c.Employees.Count + 2 == 4
                        select c;

                    TestTransformationFramework.CSharpToJS(some.Expression);
                }
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("CheckedArithmetic", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => ((c.Employees.Count + 2) = 4))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("CheckedArithmetic", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => ((c.Employees.Count + 2) = 4))", ex1_kvps["Root"]);
                Assert.AreEqual("(c.Employees.Count + 2)", ex1_kvps["Expression"]);
                Assert.AreEqual("AddChecked", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestStaticMember_Method()
        {
            try
            {
                IConvertible cnv = 2;
                var some =
                    from c in new TestClientDataContext().Companies
                    where cnv.GetTypeCode() == Type.GetTypeCode(c.GetType())
                    select c;

                var tf = new TransformationFramework();
                tf.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("StaticMember", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (Convert(value(Relinq.Playground.RelinqScriptBuilderExceptionsTests+<>c__DisplayClassa).cnv.GetTypeCode()) = Convert(GetTypeCode(c.GetType()))))", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("StaticMember", ex1_kvps["Type"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (9 = Convert(GetTypeCode(c.GetType()))))", ex1_kvps["Root"]);
                Assert.AreEqual("GetTypeCode(c.GetType())", ex1_kvps["Expression"]);
                Assert.AreEqual("Call", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestNotAnonymousCtorAndUnsupportedNodeType_New_ListInit_ElementInit()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    where new List<Company>{c}.Count == 1
                    select c;

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (new List`1() {Void Add(Relinq.Playground.Domain.Company)(c)}.Count = 1))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (new List`1() {Void Add(Relinq.Playground.Domain.Company)(c)}.Count = 1))", ex1_kvps["Root"]);
                Assert.AreEqual("new List`1() {Void Add(Relinq.Playground.Domain.Company)(c)}", ex1_kvps["Expression"]);
                Assert.AreEqual("ListInit", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestNotAnonymousCtorAndUnsupportedNodeType_MemberInit_New_ThreeTypesOfBindings()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    where new Company
                    {
                        Name = c.Name, 
                        Employees = { new Employee{Id = "hallo"} },
                        BestEmployee = { FirstName = "Vassily" }
                    }.LolMethod2(new []{"lol"})
                    select c;

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => new Company() {Name = c.Name, Employees = {Void Add(Relinq.Playground.Domain.Employee)(new Employee() {Id = \"hallo\"})}, BestEmployee = {FirstName = \"Vassily\"}}.LolMethod2(new [] {\"lol\"}))", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex1_kvps["Type"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => new Company() {Name = c.Name, Employees = {Void Add(Relinq.Playground.Domain.Employee)(value(Relinq.Playground.Domain.Employee))}, BestEmployee = {FirstName = \"Vassily\"}}.LolMethod2(value(System.String[])))", ex1_kvps["Root"]);
                Assert.AreEqual("new Company()", ex1_kvps["Expression"]);
                Assert.AreEqual("New", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestNotAnonymousCtor()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    select new Guid(c.Id);

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => new Guid(c.Id))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => new Guid(c.Id))", ex1_kvps["Root"]);
                Assert.AreEqual("new Guid(c.Id)", ex1_kvps["Expression"]);
                Assert.AreEqual("New", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestNotAnonymousCtor_NewArrayInit()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    select new Company[]{c};

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => new [] {c})", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => new [] {c})", ex1_kvps["Root"]);
                Assert.AreEqual("new [] {c}", ex1_kvps["Expression"]);
                Assert.AreEqual("NewArrayInit", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestNotAnonymousCtor_NewArrayBounds()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    select new Company[c.Employees.Count];

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => new Relinq.Playground.Domain.Company[*](c.Employees.Count))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("NotAnonymousCtor", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => new Relinq.Playground.Domain.Company[*](c.Employees.Count))", ex1_kvps["Root"]);
                Assert.AreEqual("new Relinq.Playground.Domain.Company[*](c.Employees.Count)", ex1_kvps["Expression"]);
                Assert.AreEqual("NewArrayBounds", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestExplicitTypeRelatedOperation_TypeIs()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    where c is IConvertible
                    select c;

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("ExplicitTypeRelatedOperation", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (c Is IConvertible))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("ExplicitTypeRelatedOperation", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (c Is IConvertible))", ex1_kvps["Root"]);
                Assert.AreEqual("(c Is IConvertible)", ex1_kvps["Expression"]);
                Assert.AreEqual("TypeIs", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestExplicitTypeRelatedOperation_TypeAs()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    select c as IConvertible;

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("ExplicitTypeRelatedOperation", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => (c As IConvertible))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("ExplicitTypeRelatedOperation", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => (c As IConvertible))", ex1_kvps["Root"]);
                Assert.AreEqual("(c As IConvertible)", ex1_kvps["Expression"]);
                Assert.AreEqual("TypeAs", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestExplicitTypeRelatedOperation_TwoCastConditions()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    select c.Name.Length > 1 ? (TypeA)c.B : c.C;

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("ExplicitTypeRelatedOperation", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => IIF((c.Name.Length > 1), Convert(c.B), Convert(c.C)))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("ExplicitTypeRelatedOperation", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Select(c => IIF((c.Name.Length > 1), Convert(c.B), Convert(c.C)))", ex1_kvps["Root"]);
                Assert.AreEqual("Convert(c.B)", ex1_kvps["Expression"]);
                Assert.AreEqual("Convert", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestExplicitTypeRelatedOperation_ExplicitCast()
        {
            try
            {
                var some =
                    from c in new TestClientDataContext().Companies
                    where ((double)c.Decimal()).CompareTo(2.0d) != 0
                    select c;

                TestTransformationFramework.CSharpToJS(some.Expression);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(CSharpToJSException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("ExplicitTypeRelatedOperation", ex0_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (Convert(c.Decimal()).CompareTo(2) != 0))", ex0_kvps["Input"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(ScriptBuilderException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("ExplicitTypeRelatedOperation", ex1_kvps["Type"]);
                Assert.AreEqual("value(Relinq.Infrastructure.Client.Beans.Bean`1[Relinq.Playground.Domain.Company]).Where(c => (Convert(c.Decimal()).CompareTo(2) != 0))", ex1_kvps["Root"]);
                Assert.AreEqual("Convert(c.Decimal())", ex1_kvps["Expression"]);
                Assert.AreEqual("Convert", ex1_kvps["ExpressionType"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }
    }
}