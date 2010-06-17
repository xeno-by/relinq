using System;
using NUnit.Framework;
using Relinq.Script.Semantics.Literals;
using Relinq.Script.Semantics.TypeSystem.Anonymous;
using Relinq.Helpers.Reflection;

namespace Relinq.Playground
{
    [TestFixture]
    public class AnonymousTypeGenerationTests
    {
        [Test]
        public void TestAnonymousTypeGeneration()
        {
            Func<Type> genType2 = () => AnonymousTypesHelper.ForgeTupleType(
                new string[] {"f21", "f22"},
                new Type[] {typeof(string), typeof(string)});
            Func<Type> genType1 = () => AnonymousTypesHelper.ForgeTupleType(
                new string[] {"f1", "f2"},
                new Type[] {typeof(int), genType2()});
            Func<AnonymousType1> genObj = () => new AnonymousType1(
                Activator.CreateInstance(genType1(), 1,
                    Activator.CreateInstance(genType2(), "hello", "world")));

            var o1 = genObj();

            try { DynamicAssemblyHolder.Current.Assembly.Save("anonymous.dll"); }
            catch (Exception) { /* let's not worry much about this */ }

            Assert.IsTrue(o1.Impl.GetType().IsAnonymous());
            Assert.AreEqual(1, o1.F1);
            Assert.AreEqual("hello", o1.F2.F21);
            Assert.AreEqual("world", o1.F2.F22);
            Assert.AreEqual("{f1 : 1, f2 : {f21 : 'hello', f22 : 'world'}}",
                JsonSerializer.Serialize(o1.Impl, o1.Impl.GetType()));

            var o2 = genObj();
            Assert.IsTrue(o2.Impl.GetType().IsAnonymous());
            Assert.AreEqual(1, o1.F1);
            Assert.AreEqual("hello", o1.F2.F21);
            Assert.AreEqual("world", o1.F2.F22);
            Assert.AreEqual("{f1 : 1, f2 : {f21 : 'hello', f22 : 'world'}}",
                JsonSerializer.Serialize(o2.Impl, o1.Impl.GetType()));

            Assert.AreEqual(o1.Impl.GetType(), o2.Impl.GetType());
            Assert.AreEqual(o1.Impl.GetHashCode(), o2.Impl.GetHashCode());
            Assert.IsFalse(ReferenceEquals(o1.Impl, o2.Impl));
            Assert.IsTrue(Equals(o1.Impl, o2.Impl));
        }
    }

    public class AnonymousType1
    {
        public AnonymousType1(object impl)
        {
            Impl = impl;
        }

        public int F1
        {
            get { return (int)Impl.GetType().GetProperty("f1").GetValue(Impl, null); }
        }

        public AnonymousType2 F2
        {
            get { return new AnonymousType2(
                Impl.GetType().GetProperty("f2").GetValue(Impl, null)); }
        }

        public object Impl { get; private set; }
    }

    public class AnonymousType2
    {
        public AnonymousType2(object impl)
        {
            Impl = impl;
        }

        public string F21
        {
            get { return (string)Impl.GetType().GetProperty("f21").GetValue(Impl, null); }
        }

        public string F22
        {
            get { return (string)Impl.GetType().GetProperty("f22").GetValue(Impl, null); }
        }

        public object Impl { get; private set; }
    }
}
