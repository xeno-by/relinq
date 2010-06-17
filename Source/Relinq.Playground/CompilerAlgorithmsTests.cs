using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using NUnit.Framework;
using Relinq.Script.Compilation;
using Relinq.Script.Semantics.Casts;
using Relinq.Script.Semantics.Operators.Signatures;
using Relinq.Script.Semantics.TypeInference;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Semantics.Lookup;
using Relinq.Helpers.Collections;
using Relinq.Script.Syntax.Operators;

namespace Relinq.Playground
{
    [TestFixture]
    public class CompilerAlgorithmsTests
    {
        [Test]
        public void TestLookupBaseTypes()
        {
            Assert.IsEmpty(typeof(object).LookupBaseTypes());
            Assert.IsTrue(typeof(DayOfWeek).LookupBaseTypes().SequenceEqual(
                new Type[] { typeof(Enum), typeof(ValueType), typeof(object) }));
            Assert.IsTrue(typeof(int).LookupBaseTypes().SequenceEqual(
                new Type[] { typeof(ValueType), typeof(object) }));
            Assert.IsTrue(typeof(NotSupportedException).LookupBaseTypes().SequenceEqual(
                new Type[] { typeof(SystemException), typeof(Exception), typeof(object) }));
            Assert.IsTrue(typeof(IEnumerable<>).LookupBaseTypes().SequenceEqual(
                new Type[] { typeof(IEnumerable), typeof(object) }));
            Assert.IsTrue(typeof(int[,]).LookupBaseTypes().SequenceEqual(
                new Type[] { typeof(Array), typeof(object) }));
            Assert.IsTrue(typeof(EventHandler).LookupBaseTypes().SequenceEqual(
                new Type[] { typeof(Delegate), typeof(object) }));
            Assert.IsTrue(typeof(int?).LookupBaseTypes().SequenceEqual(
                new Type[] { typeof(ValueType), typeof(object) }));
        }

        // todo. more tests to come
        // todo. including but NOT LIMITED TO:

        // 1) stuff that actually tests all predefined signatures and their lifted versions
        //    (i.e. tries to compose expression tree that corresponds to test invocation)
        //
        // 2) stuff that uses Reflector plugin to dump assembly with lifted signatures and
        //    compare the dumped classes with predefined ones (lift both p/d and u/d sigs)
        //
        // 3) tests for JsonDeserializer
        // 4) tests for various TypeStructuralTree stuff

        [Test]
        public void TestPredefinedImplicitCasts()
        {
            Action<RelinqScriptType, RelinqScriptType> yes = (s, d) => 
                Assert.IsTrue(s.HasPredefinedImplicitCastTo(d));
            Action<RelinqScriptType, RelinqScriptType> no = (s, d) => 
                Assert.IsFalse(s.HasPredefinedImplicitCastTo(d));

            // Implicit numeric conversions
            yes(typeof(sbyte), typeof(decimal));
            yes(typeof(int), typeof(float));
            no(typeof(float), typeof(decimal));
            no(typeof(sbyte), typeof(ulong));
            yes(typeof(char), typeof(double));

            // Implicit reference conversions
            yes(typeof(String), typeof(object));
            yes(typeof(Expression<EventHandler>), typeof(LambdaExpression));
            no(typeof(LambdaExpression), typeof(ConstantExpression));
            yes(typeof(Uri), typeof(ISerializable));
            no(typeof(Expression), typeof(ISerializable));
            yes(typeof(IQueryable<int>), typeof(IEnumerable<int>));
            no(typeof(IEnumerable), typeof(IQueryable));
            yes(typeof(string[]), typeof(object[]));
            no(typeof(string[,]), typeof(object[]));
            yes(typeof(string[]), typeof(IList<string>));
            yes(typeof(string[]), typeof(IEnumerable<string>));
            yes(typeof(string[]), typeof(ICollection<Object>));
            yes(typeof(string[][]), typeof(Array));
            yes(typeof(string[,]), typeof(Array));
            yes(typeof(EventHandler), typeof(Delegate));
            no(typeof(Expression<EventHandler>), typeof(Delegate));
            yes(typeof(string[,]), typeof(ICloneable));
            yes(typeof(EventHandler), typeof(ICloneable));

            // Boxing conversions
            yes(typeof(DateTime), typeof(object));
            yes(typeof(DateTime), typeof(ValueType));
            yes(typeof(DateTime), typeof(ISerializable));
            yes(typeof(DayOfWeek), typeof(Enum));
            yes(typeof(DayOfWeek?), typeof(Enum));
            yes(typeof(DayOfWeek?), typeof(ValueType));

            // Nullable conversions
            yes(typeof(sbyte), typeof(decimal?));
            no(typeof(float?), typeof(decimal?));
            yes(typeof(char?), typeof(double?));

            // Identity conversions
            yes(typeof(ushort), typeof(ushort));
            yes(typeof(ushort?), typeof(ushort?));
            yes(typeof(IEnumerable), typeof(IEnumerable));
            yes(typeof(String), typeof(String));

            // Null conversions
            yes(new Null(), typeof(String));
            yes(new Null(), typeof(int?));
            no(new Null(), typeof(int));
        }

        [Test]
        public void TestUserDefinedImplicitCasts()
        {
            Action<Type, Type> yes = (s, d) => Assert.IsTrue(s.HasUserDefinedImplicitCastTo(d));
            Action<Type, Type> no = (s, d) => Assert.IsFalse(s.HasUserDefinedImplicitCastTo(d));

            // Nullable conversions
            yes(typeof(S), typeof(T?));
            yes(typeof(S?), typeof(T?));
            no(typeof(S?), typeof(T));

            // User-defined conversions
            yes(typeof(S), typeof(T));
            no(typeof(S), typeof(ICloneable));
            yes(typeof(S), typeof(CompilerAlgorithmsTests));
            no(typeof(T), typeof(S));
        }

        private struct S
        {
            public static implicit operator T(S s) { return default(T); }
            public static implicit operator C(S s) { return default(C); }
        }

        private struct T : ICloneable
        {
            public object Clone(){ throw new NotSupportedException(); }
        }

        private class C : CompilerAlgorithmsTests
        {
        }

        [Test]
        public void TestOperatorCompilation()
        {
            OperatorType.Equal.LinqExpressionFactory()(
                new object[]{
                Expression.Constant("hello "),
                Expression.Constant("world!"),
                typeof(String).GetMethod("op_Equality")});
        }
    }
}
