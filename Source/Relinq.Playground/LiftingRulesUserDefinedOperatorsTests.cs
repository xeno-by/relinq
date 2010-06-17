using Relinq.Script.Compilation.MethodRedirection;
using Relinq.Script.Semantics.Lifting;

namespace Relinq.Playground
{
    public struct S<T> where T : struct
    {
        public static bool operator >(S<T> s1, S<T> s2) { return s1 > s2; }
        public static bool operator <(S<T> s1, S<T> s2) { return s1 < s2; }
        public static S<T>? operator *(S<T> s, T? t) { return s * t; }
        public static S<T> operator +(S<T> s1, T t) { return s1 + t; }
    }

    public interface IReferenceLiftedUserDefinedOperators
    {
        [Lifted, RedirectTo(typeof(S<int>), "Boolean op_LessThan(Playground.V2.Lifting.S`1[System.Int32], Playground.V2.Lifting.S`1[System.Int32])")]
        bool S1(S<int>? s1, S<int>? s2);
        [Lifted, RedirectTo(typeof(S<int>), "System.Nullable`1[Playground.V2.Lifting.S`1[System.Int32]] op_Multiply(Playground.V2.Lifting.S`1[System.Int32], System.Nullable`1[System.Int32])")]
        bool S2(S<int>? s1, S<int>? s2);
        [Lifted, RedirectTo(typeof(S<int>), "Playground.V2.Lifting.S`1[System.Int32] op_Addition(Playground.V2.Lifting.S`1[System.Int32], Int32)")]
        S<int>? S3(S<int>? s1, int? s2);
    }
    
    public interface IReferenceLiftedUserDefinedOperators<T> where T : struct
    {
        [Lifted, RedirectTo(typeof(S<>), "Boolean op_GreaterThan(Playground.V2.Lifting.S`1[T], Playground.V2.Lifting.S`1[T])")]
        bool S1(S<T>? s1, S<T>? s2);
        [Lifted, RedirectTo(typeof(S<>), "Boolean op_LessThan(Playground.V2.Lifting.S`1[T], Playground.V2.Lifting.S`1[T])")]
        bool S2(S<T>? s1, S<T>? s2);
        [Lifted, RedirectTo(typeof(S<>), "Playground.V2.Lifting.S`1[T] op_Addition(Playground.V2.Lifting.S`1[T], T)")]
        S<T>? S3(S<T>? s1, T? s2);
    }

    // todo. this stuff is not yet implemented
}
