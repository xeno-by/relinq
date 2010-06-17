using NUnit.Framework;

namespace Relinq.Playground
{
    public static class Helpers
    {
        public static void Extension(this A a){}
        public static void Extension(this C a){}
    }

    public class A{public static implicit operator C(A a){return null;}}
    public class B{public static implicit operator A(B b){return null;}}
    public class C{}

    public interface IFace
    {
        void TestFace();
    }

    public class SignatureHidingTestsBase<TT1, TT2>
    {
        protected void TestSignatures<T, U>(TT1 t, T t1, U t2) { }
        protected void TestSignatures<T, U>(TT2 t, T t1, U t2) { }
        protected void TestSignatures<T, U>(T t, T t1, U t2) { }
    }

    [TestFixture]
    public class SignatureHidingTests<TT, TT3> : SignatureHidingTestsBase<TT, TT>, IFace
    {
        [Test]
        public void TestSignatures()
        {
//            TestSignatures("Hello world", "Hello again");
            // ambiguous invocation
        }

        // base type declares the following stuff
//        protected void TestSignatures<T, U>(TT1 t, T t1, U t2) { }
//        protected void TestSignatures<T, U>(TT2 t, T t1, U t2) { }
//        protected void TestSignatures<T, U>(T t, T t1, U t2) { }
        protected void TestSignatures<T, U>(TT3 t, T t1, U t2) { }
        protected void TestSignatures<U, T>(T t, T t1, U t2) { }
        protected void TestSignatures<P>(P t, P t1) { }

        public int TestFace;
        void IFace.TestFace() { }
    }
}
