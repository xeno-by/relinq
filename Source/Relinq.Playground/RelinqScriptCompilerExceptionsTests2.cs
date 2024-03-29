using System;
using System.Diagnostics;
using NUnit.Framework;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.JSToCSharp;
using Relinq.Exceptions.JSToCSharp.TypeInference;

namespace Relinq.Playground
{
    [TestFixture]
    public class RelinqScriptCompilerExceptionsTests2
    {
        [Test]
        public void TestMethodMatching_ArgcMismatch()
        {
            try
            {
                var js = "ctx.Companies.Where('argc will fail', function(c){return c.Name == '';})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where('argc will fail', function(c){return c.Name == '';})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ArgcMismatch
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where('argc will fail', c => c.Name == '')]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: <unknown> ([/()/arg0:c:('argc will fail', StringLiteral) -> 'argc will fail'])
    ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 13af08fc-5515-48e6-b691-dd0f2d8723b0
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ArgcMismatch
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where('argc will fail', c => c.Name == '')]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: <unknown> ([/()/arg0:c:('argc will fail', StringLiteral) -> 'argc will fail'])
    ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 2ac64f0c-9ff7-48bd-ae9b-e25e10fbee0c
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ArgcMismatch
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where('argc will fail', c => c.Name == '')]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: <unknown> ([/()/arg0:c:('argc will fail', StringLiteral) -> 'argc will fail'])
    ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: b82c1289-2d0e-48a9-9ce7-fdde2da3234e
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ArgcMismatch
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where('argc will fail', c => c.Name == '')]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: <unknown> ([/()/arg0:c:('argc will fail', StringLiteral) -> 'argc will fail'])
    ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: f88a017d-86cf-4071-84eb-a3bd2d745838
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where('argc will fail', c => c.Name == '')]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where('argc will fail', c => c.Name == '')]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestMethodMatching_SignatureIsEntrapped()
        {
            try
            {
                var js = "ctx.Companies.ElementAt(0).LolMethod(1 && 2)";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.ElementAt(0).LolMethod(1 && 2)", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG &&: System.Void [Trap] S(System.Int32 x, System.Int32 y), System.Void [Trap] S(System.UInt32 x, System.UInt32 y), System.Void [Trap] S(System.Int64 x, System.Int64 y), System.Void [Trap] S(System.UInt64 x, System.UInt64 y), System.Void [Trap] S(System.Int64 x, System.UInt64 y), System.Void [Trap] S(System.UInt64 x, System.Int64 y), E [Trap] S`1[E](E x, E y), System.Boolean S(System.Boolean x, System.Boolean y)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: E [Trap] S`1[E](E x, E y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: SignatureFailsMetaConstrains
    IsUnexpected: False
    OriginalSignature: E [Trap] S`1[E](E x, E y)
    InferredSignature: System.Int32 [Trap] S`1[System.Int32](System.Int32 x, System.Int32 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Int32
    FormalArguments[2]: System.Int32
    RootExpression: [/()/arg0:&& -> 1 && 2]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.Int32>
    ActualArguments[2]: <System.Int32>
    Id: 6407b71c-fcef-43cd-8e7e-a1552b32be16
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S(System.Boolean x, System.Boolean y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Boolean S(System.Boolean x, System.Boolean y)
    InferredSignature: System.Boolean S(System.Boolean x, System.Boolean y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Boolean
    FormalArguments[2]: System.Boolean
    RootExpression: [/()/arg0:&& -> 1 && 2]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.Int32>
    ActualArguments[2]: <System.Int32>
    MismatchIndex: 1
    Id: 6a9a73d1-2162-47ed-aabb-305369902102
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Void [Trap] S(System.UInt32 x, System.UInt32 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Void [Trap] S(System.UInt32 x, System.UInt32 y)
    InferredSignature: System.Void [Trap] S(System.UInt32 x, System.UInt32 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.UInt32
    FormalArguments[2]: System.UInt32
    RootExpression: [/()/arg0:&& -> 1 && 2]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.Int32>
    ActualArguments[2]: <System.Int32>
    MismatchIndex: 1
    Id: d12725e2-1f63-4ac6-8adf-512af40a962f
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Void [Trap] S(System.UInt64 x, System.UInt64 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Void [Trap] S(System.UInt64 x, System.UInt64 y)
    InferredSignature: System.Void [Trap] S(System.UInt64 x, System.UInt64 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.UInt64
    FormalArguments[2]: System.UInt64
    RootExpression: [/()/arg0:&& -> 1 && 2]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.Int32>
    ActualArguments[2]: <System.Int32>
    MismatchIndex: 1
    Id: e0099a94-9d38-4b27-bf11-1500ec376b2c
", ex1_kvps["Failboats[3]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Void [Trap] S(System.Int64 x, System.UInt64 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Void [Trap] S(System.Int64 x, System.UInt64 y)
    InferredSignature: System.Void [Trap] S(System.Int64 x, System.UInt64 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Int64
    FormalArguments[2]: System.UInt64
    RootExpression: [/()/arg0:&& -> 1 && 2]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.Int32>
    ActualArguments[2]: <System.Int32>
    MismatchIndex: 2
    Id: febf2ab7-de6c-4b82-84b2-2793ead6f84b
", ex1_kvps["Failboats[4]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Void [Trap] S(System.UInt64 x, System.Int64 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Void [Trap] S(System.UInt64 x, System.Int64 y)
    InferredSignature: System.Void [Trap] S(System.UInt64 x, System.Int64 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.UInt64
    FormalArguments[2]: System.Int64
    RootExpression: [/()/arg0:&& -> 1 && 2]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.Int32>
    ActualArguments[2]: <System.Int32>
    MismatchIndex: 1
    Id: eb97dcce-dd53-4b16-b98b-c07aa4f52e85
", ex1_kvps["Failboats[5]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Void [Trap] S(System.Int64 x, System.Int64 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: SignatureIsSubPar
    IsUnexpected: False
    OriginalSignature: System.Void [Trap] S(System.Int64 x, System.Int64 y)
    InferredSignature: System.Void [Trap] S(System.Int64 x, System.Int64 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Int64
    FormalArguments[2]: System.Int64
    RootExpression: [/()/arg0:&& -> 1 && 2]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.Int32>
    ActualArguments[2]: <System.Int32>
    Id: 304a4a0e-c0fd-4bc6-98ac-e303940073b6
", ex1_kvps["Failboats[6]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Void [Trap] S(System.Int32 x, System.Int32 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: SignatureIsEntrapped
    IsUnexpected: False
    OriginalSignature: System.Void [Trap] S(System.Int32 x, System.Int32 y)
    InferredSignature: System.Void [Trap] S(System.Int32 x, System.Int32 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Int32
    FormalArguments[2]: System.Int32
    RootExpression: [/()/arg0:&& -> 1 && 2]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.Int32>
    ActualArguments[2]: <System.Int32>
    Id: 96880b50-46ab-4e8d-ba9f-babe2546881f
", ex1_kvps["Failboats[7]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.ElementAt(0).LolMethod(1 && 2)]", ex1_kvps["Root"]);
                Assert.AreEqual("[/()/arg0:&& -> 1 && 2]", ex1_kvps["Expression"]);
                Assert.AreEqual("Operator", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestMethodMatching_ActualArgCannotBeCastToFormalArg_SignatureFailsMetaConstraints()
        {
            try
            {
                var js = "ctx.Companies.ElementAt(0).DayOfWeek == 'failure'";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.ElementAt(0).DayOfWeek == 'failure'", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG ==: System.Boolean S(System.Boolean x, System.Boolean y), System.Boolean S3`1[C](C x, C y), System.Boolean S4`1[S](S x, [IsNull] S y), System.Boolean S5`1[S]([IsNull] S x, S y), System.Boolean S(System.Int32 x, System.Int32 y), System.Boolean S(System.UInt32 x, System.UInt32 y), System.Boolean S(System.Int64 x, System.Int64 y), System.Boolean S(System.UInt64 x, System.UInt64 y), System.Void [Trap] S(System.Int64 x, System.UInt64 y), System.Void [Trap] S(System.UInt64 x, System.Int64 y), System.Boolean S(System.Single x, System.Single y), System.Boolean S(System.Double x, System.Double y)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S3`1[C](C x, C y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: SignatureFailsMetaConstrains
    IsUnexpected: False
    OriginalSignature: System.Boolean S3`1[C](C x, C y)
    InferredSignature: System.Boolean S3`1[System.DayOfWeek](System.DayOfWeek x, System.DayOfWeek y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.DayOfWeek
    FormalArguments[2]: System.DayOfWeek
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    Id: 570a580f-ea47-47f9-ace5-331bf48596b6
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S4`1[S](S x, [IsNull] S y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: SignatureFailsMetaConstrains
    IsUnexpected: False
    OriginalSignature: System.Boolean S4`1[S](S x, [IsNull] S y)
    InferredSignature: System.Boolean S4`1[System.DayOfWeek](System.DayOfWeek x, [IsNull] System.DayOfWeek y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.DayOfWeek
    FormalArguments[2]: System.DayOfWeek
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    Id: 6bef5738-4b1d-46e1-8f73-e026e4e3749b
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S5`1[S]([IsNull] S x, S y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: SignatureFailsMetaConstrains
    IsUnexpected: False
    OriginalSignature: System.Boolean S5`1[S]([IsNull] S x, S y)
    InferredSignature: System.Boolean S5`1[System.DayOfWeek]([IsNull] System.DayOfWeek x, System.DayOfWeek y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.DayOfWeek
    FormalArguments[2]: System.DayOfWeek
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    Id: db90f75c-6a4d-448c-a95e-1f1b1026f020
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S(System.Boolean x, System.Boolean y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Boolean S(System.Boolean x, System.Boolean y)
    InferredSignature: System.Boolean S(System.Boolean x, System.Boolean y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Boolean
    FormalArguments[2]: System.Boolean
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 1
    Id: b44e9ae1-adce-40c5-92df-47b95d55172e
", ex1_kvps["Failboats[3]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S(System.Int32 x, System.Int32 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Boolean S(System.Int32 x, System.Int32 y)
    InferredSignature: System.Boolean S(System.Int32 x, System.Int32 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Int32
    FormalArguments[2]: System.Int32
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 2
    Id: 45772460-221f-429d-910e-fc54824c4c23
", ex1_kvps["Failboats[4]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S(System.UInt32 x, System.UInt32 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Boolean S(System.UInt32 x, System.UInt32 y)
    InferredSignature: System.Boolean S(System.UInt32 x, System.UInt32 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.UInt32
    FormalArguments[2]: System.UInt32
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 1
    Id: 30c5d984-eebe-44ed-a49f-c30d9992b4aa
", ex1_kvps["Failboats[5]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S(System.Int64 x, System.Int64 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Boolean S(System.Int64 x, System.Int64 y)
    InferredSignature: System.Boolean S(System.Int64 x, System.Int64 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Int64
    FormalArguments[2]: System.Int64
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 1
    Id: 6c051af4-7226-4c99-96a3-9fc686772bcc
", ex1_kvps["Failboats[6]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S(System.UInt64 x, System.UInt64 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Boolean S(System.UInt64 x, System.UInt64 y)
    InferredSignature: System.Boolean S(System.UInt64 x, System.UInt64 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.UInt64
    FormalArguments[2]: System.UInt64
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 1
    Id: 7d195ad9-924b-4d92-b2cf-859824bb864f
", ex1_kvps["Failboats[7]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Void [Trap] S(System.Int64 x, System.UInt64 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Void [Trap] S(System.Int64 x, System.UInt64 y)
    InferredSignature: System.Void [Trap] S(System.Int64 x, System.UInt64 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Int64
    FormalArguments[2]: System.UInt64
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 1
    Id: 1c4af3b1-5714-4ddd-8898-fcb4fddc528c
", ex1_kvps["Failboats[8]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Void [Trap] S(System.UInt64 x, System.Int64 y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Void [Trap] S(System.UInt64 x, System.Int64 y)
    InferredSignature: System.Void [Trap] S(System.UInt64 x, System.Int64 y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.UInt64
    FormalArguments[2]: System.Int64
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 1
    Id: a9a5628d-74f9-4b24-afe4-a47e299cd3f4
", ex1_kvps["Failboats[9]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S(System.Single x, System.Single y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Boolean S(System.Single x, System.Single y)
    InferredSignature: System.Boolean S(System.Single x, System.Single y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Single
    FormalArguments[2]: System.Single
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 1
    Id: 4db90ddb-3ebf-4519-8853-5fe1fb9a8606
", ex1_kvps["Failboats[10]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean S(System.Double x, System.Double y)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: ActualArgCannotBeCastToFormalArg
    IsUnexpected: False
    OriginalSignature: System.Boolean S(System.Double x, System.Double y)
    InferredSignature: System.Boolean S(System.Double x, System.Double y)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: System.Double
    FormalArguments[2]: System.Double
    RootExpression: [/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: null
    ActualArguments[1]: <System.DayOfWeek>
    ActualArguments[2]: <unknown> ([/==/r:c:('failure', StringLiteral) -> 'failure'])
    MismatchIndex: 1
    Id: f52e2971-7e37-4851-a4e2-98e7359e2566
", ex1_kvps["Failboats[11]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']", ex1_kvps["Root"]);
                Assert.AreEqual("[/== -> ctx.Companies.ElementAt(0).DayOfWeek == 'failure']", ex1_kvps["Expression"]);
                Assert.AreEqual("Operator", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestMethodMatching_SignatureIsSubPar_SignatureIsOkButAmbiguous()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return c.LolMethod3(c.B, c.B);})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod3(c.B, c.B);})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: cb3fe7bf-038c-428f-91c8-9cb0fc1ed1cb
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 64357758-c84f-4223-8861-9a98dbf7ae27
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod3: System.Boolean LolMethod3([ParamArray] Playground.Domain.TypeA[] a), System.Boolean LolMethod3(Playground.Domain.TypeB b, Playground.Domain.TypeA a), System.Boolean LolMethod3(Playground.Domain.TypeA a, Playground.Domain.TypeB b)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod3([ParamArray] Playground.Domain.TypeA[] a)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: SignatureIsSubPar
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod3([ParamArray] Playground.Domain.TypeA[] a)
            InferredSignature: System.Boolean LolMethod3([ParamArray] Playground.Domain.TypeA[] a)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: Playground.Domain.TypeA
            FormalArguments[2]: Playground.Domain.TypeA
            RootExpression: [/()/arg0:λ/() -> c.LolMethod3(c.B, c.B)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.TypeB>
            ActualArguments[2]: <Playground.Domain.TypeB>
            Id: bab49d05-f4c6-424c-8a8f-bfd223dad3ce
        Failboats[1]: 
          Key: System.Boolean LolMethod3(Playground.Domain.TypeB b, Playground.Domain.TypeA a)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: SignatureIsOkButAmbiguous
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod3(Playground.Domain.TypeB b, Playground.Domain.TypeA a)
            InferredSignature: System.Boolean LolMethod3(Playground.Domain.TypeB b, Playground.Domain.TypeA a)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: Playground.Domain.TypeB
            FormalArguments[2]: Playground.Domain.TypeA
            RootExpression: [/()/arg0:λ/() -> c.LolMethod3(c.B, c.B)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.TypeB>
            ActualArguments[2]: <Playground.Domain.TypeB>
            Id: 23229f73-1c80-49bd-bac9-46770da30641
        Failboats[2]: 
          Key: System.Boolean LolMethod3(Playground.Domain.TypeA a, Playground.Domain.TypeB b)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: SignatureIsOkButAmbiguous
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod3(Playground.Domain.TypeA a, Playground.Domain.TypeB b)
            InferredSignature: System.Boolean LolMethod3(Playground.Domain.TypeA a, Playground.Domain.TypeB b)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: Playground.Domain.TypeA
            FormalArguments[2]: Playground.Domain.TypeB
            RootExpression: [/()/arg0:λ/() -> c.LolMethod3(c.B, c.B)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.TypeB>
            ActualArguments[2]: <Playground.Domain.TypeB>
            Id: f7aa548b-57f6-4b45-a9a5-9c970dc77be8
        Type: AmbiguousMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
        Expression: [/()/arg0:λ/() -> c.LolMethod3(c.B, c.B)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: aba1c1fa-3b69-4979-97cc-d3b573f0e063
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 838d6d7b-d3fe-44e9-8182-2ac959ec730f
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: ff53da8e-efe0-42c1-8d3a-6e6e648cc572
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 671b9eb9-8de4-4ebd-8093-db78f237a5d0
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 76131f75-acc7-4d8b-9c3a-0f1ed47bb792
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod3: System.Boolean LolMethod3([ParamArray] Playground.Domain.TypeA[] a), System.Boolean LolMethod3(Playground.Domain.TypeB b, Playground.Domain.TypeA a), System.Boolean LolMethod3(Playground.Domain.TypeA a, Playground.Domain.TypeB b)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod3([ParamArray] Playground.Domain.TypeA[] a)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: SignatureIsSubPar
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod3([ParamArray] Playground.Domain.TypeA[] a)
            InferredSignature: System.Boolean LolMethod3([ParamArray] Playground.Domain.TypeA[] a)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: Playground.Domain.TypeA
            FormalArguments[2]: Playground.Domain.TypeA
            RootExpression: [/()/arg0:λ/() -> c.LolMethod3(c.B, c.B)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.TypeB>
            ActualArguments[2]: <Playground.Domain.TypeB>
            Id: 5d516b66-7bd4-4cff-80dd-7eb2498b6a55
        Failboats[1]: 
          Key: System.Boolean LolMethod3(Playground.Domain.TypeB b, Playground.Domain.TypeA a)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: SignatureIsOkButAmbiguous
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod3(Playground.Domain.TypeB b, Playground.Domain.TypeA a)
            InferredSignature: System.Boolean LolMethod3(Playground.Domain.TypeB b, Playground.Domain.TypeA a)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: Playground.Domain.TypeB
            FormalArguments[2]: Playground.Domain.TypeA
            RootExpression: [/()/arg0:λ/() -> c.LolMethod3(c.B, c.B)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.TypeB>
            ActualArguments[2]: <Playground.Domain.TypeB>
            Id: eaede7aa-6237-47d6-871f-aaacf9250c7f
        Failboats[2]: 
          Key: System.Boolean LolMethod3(Playground.Domain.TypeA a, Playground.Domain.TypeB b)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: SignatureIsOkButAmbiguous
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod3(Playground.Domain.TypeA a, Playground.Domain.TypeB b)
            InferredSignature: System.Boolean LolMethod3(Playground.Domain.TypeA a, Playground.Domain.TypeB b)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: Playground.Domain.TypeA
            FormalArguments[2]: Playground.Domain.TypeB
            RootExpression: [/()/arg0:λ/() -> c.LolMethod3(c.B, c.B)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.TypeB>
            ActualArguments[2]: <Playground.Domain.TypeB>
            Id: 739843b8-b0a4-43d8-bc9c-ec29b2e570d7
        Type: AmbiguousMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
        Expression: [/()/arg0:λ/() -> c.LolMethod3(c.B, c.B)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 6eeddcfa-68bb-4c09-bdef-5b0cab968e13
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 8b267e63-2df4-4de5-b902-ea9659f2f970
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 890f9a25-7192-4ddd-a802-7674235e3eae
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod3(c.B, c.B))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_LambdaArgcMismatch_LambdaIncompatibleRetval()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return 2;})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch(Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return 2;})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => 2)]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 51e05713-afc8-4ccc-85c6-ca476a9c1c73
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaIncompatibleRetval
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => 2)]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: b1150ca5-e9d2-4747-b030-f44d59a95020
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => 2)]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 948a0af7-5d8b-4060-8a32-8d00408c2cde
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => 2)]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 0cb2a4fa-807a-40a9-8e36-a365fa536489
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => 2)]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: aa38f88e-d8ea-484a-be11-d23019373be9
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaIncompatibleRetval
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => 2)]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: f437f447-8f33-4479-86eb-a9eb5822ea3b
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => 2)]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 59d9390e-dc8c-4a9a-9a7d-a78711220248
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => 2)]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 2d6aa001-947f-4ab4-8929-f6c919a31f86
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => 2)]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => 2)]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_IncompleteInferenceSet()
        {
            try
            {
                // todo. failboat exception
                var js = "ctx.Companies.ElementAt(0).LolMethod4()";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.ElementAt(0).LolMethod4()", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG LolMethod4: System.Int32 LolMethod4`1[T]()]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Int32 LolMethod4`1[T]()
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Int32 LolMethod4`1[T]()
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    RootExpression: [/() -> ctx.Companies.ElementAt(0).LolMethod4()]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <Playground.Domain.Company>
    Id: a36704de-1a03-497b-950b-53659c35631f
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: IncompleteInferenceSet
      IsUnexpected: False
      OriginalSignature: System.Int32 LolMethod4`1[T]()
      InferredSignature: System.Int32 LolMethod4`1[T]()
      FormalArguments: System.Type[]
      FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
      RootExpression: [/() -> ctx.Companies.ElementAt(0).LolMethod4()]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <Playground.Domain.Company>
      Id: 75f38f11-b591-45bf-81f4-cd55df113a48
", ex1_kvps["Failboats[0]"]);
                Assert.AreEqual("Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]", ex1_kvps["ActualArguments"]);
                Assert.AreEqual("<Relinq.Playground.Domain.Company>", ex1_kvps["ActualArguments[0]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.ElementAt(0).LolMethod4()]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.ElementAt(0).LolMethod4()]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_InferencesContradictGenericConstraints()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return c.LolMethod6(c);})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod6(c);})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 7c6e257c-e415-46b6-9101-614d558395f1
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 2ead12ef-f4e3-4e6c-81da-fd3fbc343e6f
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod6: System.Boolean LolMethod6`1[T](T t)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod6`1[T](T t)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod6`1[T](T t)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: T
            RootExpression: [/()/arg0:λ/() -> c.LolMethod6(c)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            Id: 7d188ab2-d62d-4618-9ad1-4ea044d2f256
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: InferencesContradictGenericConstraints
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod6`1[T](T t)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: T
              RootExpression: [/()/arg0:λ/() -> c.LolMethod6(c)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              MismatchIndex: 1
              Id: 607e3e25-b859-431a-b84d-51259f258c21
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
        Expression: [/()/arg0:λ/() -> c.LolMethod6(c)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 0b22db6a-5626-4e05-a751-c4f2da9d047a
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: e5f2d58b-163f-4a2f-8d6f-a096faaf405a
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: b89ba2c8-8112-4a25-b7d2-408e03428569
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: e8d13978-3a62-44a6-8d2d-5a75971ec3b4
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: a937c8df-998c-4f01-aa8c-99758958fa55
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod6: System.Boolean LolMethod6`1[T](T t)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod6`1[T](T t)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod6`1[T](T t)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: T
            RootExpression: [/()/arg0:λ/() -> c.LolMethod6(c)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            Id: ef0b465b-7cf3-4542-a0e5-e75274274fa1
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: InferencesContradictGenericConstraints
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod6`1[T](T t)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: T
              RootExpression: [/()/arg0:λ/() -> c.LolMethod6(c)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              MismatchIndex: 1
              Id: 785085fd-0f5e-41a9-b7e9-9d830221531b
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
        Expression: [/()/arg0:λ/() -> c.LolMethod6(c)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: f7fa7c85-7ff9-4d02-8860-6201e73e17a1
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 40c71979-85e2-4a11-a508-3f573b3b55cd
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod6(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: eabd5b72-a240-4531-88cf-b2eb52fb3c70
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod6(c))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod6(c))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_LambdaInvalidRetval()
        {
            try
            {
                var js = "ctx.Companies.Select(function(c){return {Name : 'hello', Description : 'world'};})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Select(function(c){return {Name : 'hello', Description : 'world'};})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Select: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[TSource, TResult](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, TResult] selector), System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[TSource, TResult](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, TResult] selector), System.Linq.IQueryable`1[TResult] [Extension] Select`2[TSource, TResult](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, TResult]] selector), System.Linq.IQueryable`1[TResult] [Extension] Select`2[TSource, TResult](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, TResult]] selector)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[TSource, TResult](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, TResult] selector)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[TSource, TResult](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, TResult] selector)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, TResult]
    RootExpression: [/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 2e7f51e3-6882-48d9-8a3a-3f3368ccd3f6
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInvalidRetval
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[TSource, TResult](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, TResult] selector)
      InferredSignature: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[Playground.Domain.Company, TResult](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, TResult] selector)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, TResult]
      RootExpression: [/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 45147b1a-b422-4cec-b3e2-d8ff0cc130cb
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[TSource, TResult](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, TResult] selector)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[TSource, TResult](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, TResult] selector)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, TResult]
    RootExpression: [/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 48ba7482-272e-44e1-a647-4000614dd755
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[TSource, TResult](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, TResult] selector)
      InferredSignature: System.Collections.Generic.IEnumerable`1[TResult] [Extension] Select`2[Playground.Domain.Company, TResult](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, TResult] selector)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, TResult]
      RootExpression: [/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 2c5c082a-e905-431a-82ba-4f091554ba0f
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TResult] [Extension] Select`2[TSource, TResult](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, TResult]] selector)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TResult] [Extension] Select`2[TSource, TResult](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, TResult]] selector)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, TResult]]
    RootExpression: [/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 44312f7e-7865-4c86-a5e0-c0975842e095
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInvalidRetval
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TResult] [Extension] Select`2[TSource, TResult](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, TResult]] selector)
      InferredSignature: System.Linq.IQueryable`1[TResult] [Extension] Select`2[Playground.Domain.Company, TResult](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, TResult]] selector)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, TResult]]
      RootExpression: [/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 3d6fe8b9-56e3-4b5e-81fd-5375932eaf26
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TResult] [Extension] Select`2[TSource, TResult](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, TResult]] selector)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TResult] [Extension] Select`2[TSource, TResult](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, TResult]] selector)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, TResult]]
    RootExpression: [/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 657000fe-c998-47c8-ae67-f6493441a7d6
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TResult] [Extension] Select`2[TSource, TResult](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, TResult]] selector)
      InferredSignature: System.Linq.IQueryable`1[TResult] [Extension] Select`2[Playground.Domain.Company, TResult](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, TResult]] selector)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, TResult]]
      RootExpression: [/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: b8e02844-1a82-4eea-83b5-d89d841619c1
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Select(c => {Name : 'hello', Description : 'world'})]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_FormalArgCannotBeInferredFromActualArg()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return c.LolMethod7(c);})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod7(c);})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: c22b7e22-0a79-4774-a40c-9229b72e39f0
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: daf41565-4d33-40ee-bb5a-9dd27905081a
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod7: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: Playground.Domain.U[]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod7(c)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            Id: 57b57d7f-f75b-4972-94d5-6abe788dcb4d
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: FormalArgCannotBeInferredFromActualArg
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)
              InferredSignature: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.U[]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod7(c)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              MismatchIndex: 1
              Id: 5fa7ad11-4974-4f01-befd-9af57889d6e1
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
        Expression: [/()/arg0:λ/() -> c.LolMethod7(c)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 0ee3a0c4-e0d5-482f-9f7b-f38522534f25
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 058a29b2-10e5-4b6f-b500-286fd0741abd
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: e484cd48-5784-4ed7-892e-8dc3cdd62a1e
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 3142b795-0d2e-47c2-8adc-22546e06eefe
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 6d998813-16fc-41d9-bd59-860d899456a0
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod7: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: Playground.Domain.U[]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod7(c)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            Id: edc953e6-5bbf-40c7-b7d0-a525bfd51e55
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: FormalArgCannotBeInferredFromActualArg
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)
              InferredSignature: System.Boolean LolMethod7`1[U](Playground.Domain.U[] t)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.U[]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod7(c)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              MismatchIndex: 1
              Id: a2ea6a4e-f732-49ac-8e1c-b655ba77988a
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
        Expression: [/()/arg0:λ/() -> c.LolMethod7(c)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: eb6dd6e1-cb3c-4795-8463-cf074327b112
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 1fc1d61a-989b-4f74-af09-f9c0af4f6961
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod7(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 173dd3b9-c102-4047-8858-8e47a00e46dc
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod7(c))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod7(c))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_FormalArgCannotBeInferredFromActualArg2()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return c.LolMethod8(c);})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod8(c);})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: fe18fb83-e0b8-4feb-8f21-02a3da14e2d5
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 19fd16a3-8385-4d1b-a082-c11f72e80cd4
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod8: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: System.Collections.Generic.List`1[U]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod8(c)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            Id: faaa2948-51b9-4d69-ac35-c8485c12588c
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: FormalArgCannotBeInferredFromActualArg
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)
              InferredSignature: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: System.Collections.Generic.List`1[U]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod8(c)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              MismatchIndex: 1
              Id: 99eec7fb-ff33-47f0-90b9-e772babab5f2
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
        Expression: [/()/arg0:λ/() -> c.LolMethod8(c)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 76b754b2-4d21-4783-9dcc-22f328c5d230
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: b2f755ce-f613-465d-b5ae-163d6df37c03
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 57c7efa6-0495-4f44-8d15-7617d0dd4c83
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 5c81062e-acfd-4ba1-8303-7410b1415867
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 48eedc85-e055-4f59-93f6-65767c062537
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod8: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: System.Collections.Generic.List`1[U]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod8(c)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            Id: fe62fd1e-c5e4-4fcf-b5e7-57d3f789475e
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: FormalArgCannotBeInferredFromActualArg
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)
              InferredSignature: System.Boolean LolMethod8`1[U](System.Collections.Generic.List`1[U] t)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: System.Collections.Generic.List`1[U]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod8(c)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              MismatchIndex: 1
              Id: 078ed43b-bc7c-48a5-a062-0a64d2db06c6
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
        Expression: [/()/arg0:λ/() -> c.LolMethod8(c)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 72b5fd7e-21ae-4fd8-9cda-884ce2bd5c32
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: a8b4f048-7147-46f4-b0e3-7d8d4f8420a4
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod8(c))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 92cce870-dc58-4de5-9c02-297a4ce286fe
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod8(c))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod8(c))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_LambdaIncompatibleRetval2_LambdaInconsistentBody()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return c.LolMethod9(c, function(c1){return 2;});})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod9(c, function(c1){return 2;});})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: a1ad5bb1-6418-4645-bd3d-cc6629afe426
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 71c14014-7675-46c2-b6ac-48cacc219370
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod9: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, System.Boolean]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod9(c, c1 => 2)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: ccfec215-fa11-4eef-b69d-3733aff9772a
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: LambdaIncompatibleRetval
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
              InferredSignature: System.Boolean LolMethod9`1[Playground.Domain.Company](Playground.Domain.Company u, System.Func`2[Playground.Domain.Company, System.Boolean] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.Company
              FormalArguments[2]: System.Func`2[Playground.Domain.Company, System.Boolean]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod9(c, c1 => 2)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
              MismatchIndex: 2
              Id: aa77bb48-b558-496d-879f-bbb99c2c13a5
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
        Expression: [/()/arg0:λ/() -> c.LolMethod9(c, c1 => 2)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: e76ff1d9-7c24-43c2-a74d-73fbd5e2c7ab
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 39497c2d-b0a5-4745-b22e-1980a004a420
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: d409c437-8f1d-42a5-9982-118abfcd25d1
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: bc6f4f5d-5c16-4fe3-ab74-d91aab56c99f
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: d8261369-501b-44d1-8f93-a97ceb59911f
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod9: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, System.Boolean]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod9(c, c1 => 2)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: 52b417f5-376a-486e-8f30-9479eeb294f5
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: LambdaIncompatibleRetval
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
              InferredSignature: System.Boolean LolMethod9`1[Playground.Domain.Company](Playground.Domain.Company u, System.Func`2[Playground.Domain.Company, System.Boolean] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.Company
              FormalArguments[2]: System.Func`2[Playground.Domain.Company, System.Boolean]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod9(c, c1 => 2)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
              MismatchIndex: 2
              Id: 19796912-6998-4a8e-86f1-c6a0dc2b211c
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
        Expression: [/()/arg0:λ/() -> c.LolMethod9(c, c1 => 2)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 1e5ed929-92a0-4278-a4e8-7eca743354b2
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 25fd92cc-1bce-4d08-b3e8-8df8a8917352
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: a8c944ce-ee50-48ad-8e46-5630899bd113
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod9(c, c1 => 2))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_LambdaInconsistentBody2()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return c.LolMethod9(c, function(e){return e.LastName;});})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod9(c, function(e){return e.LastName;});})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 2bb0a176-c3d1-46fa-b1f3-ac407528accf
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: e8851da3-0e8b-452a-9fe0-fddf8eb06d09
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod9: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, System.Boolean]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod9(c, e => e.LastName)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: 36c7ba21-25bc-44f1-912e-7fae15b92698
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: LambdaInconsistentBody
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
              InferredSignature: System.Boolean LolMethod9`1[Playground.Domain.Company](Playground.Domain.Company u, System.Func`2[Playground.Domain.Company, System.Boolean] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.Company
              FormalArguments[2]: System.Func`2[Playground.Domain.Company, System.Boolean]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod9(c, e => e.LastName)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
              MismatchIndex: 2
              Id: 5a788817-388f-4a45-bc4d-7e85c43691ff
              InnerException: 
                ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.NoSuchMemberException
                InferredTypeOfTarget: <Playground.Domain.Company>
                MemberName: LastName
                Type: NoSuchFieldOrProp
                Root: [/()/arg0:λ/() -> c.LolMethod9(c, e => e.LastName)]
                Expression: [/()/arg0:λ/()/arg1:λ/f:LastName -> e.LastName]
                ExpressionType: MemberAccess
                IsUnexpected: False
                Id: 79ec1750-152b-4353-a2cf-b23f93b2cf27
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
        Expression: [/()/arg0:λ/() -> c.LolMethod9(c, e => e.LastName)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 659ec798-c3e4-46c4-bf73-10619fa1eb46
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 77a12f97-4a9b-45d6-833a-c36462f95f84
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 3b329aaf-88a0-4eab-9113-b7665b30f084
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: a9fc3918-6633-48a7-9ccb-013eda7d04cb
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 2bc20e73-e4e3-43d1-a853-8251ac5f4d1e
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod9: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, System.Boolean]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod9(c, e => e.LastName)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: ca495504-9144-4995-ac11-3462dfa99bfd
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: LambdaInconsistentBody
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod9`1[U](U u, System.Func`2[U, System.Boolean] f)
              InferredSignature: System.Boolean LolMethod9`1[Playground.Domain.Company](Playground.Domain.Company u, System.Func`2[Playground.Domain.Company, System.Boolean] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.Company
              FormalArguments[2]: System.Func`2[Playground.Domain.Company, System.Boolean]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod9(c, e => e.LastName)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
              MismatchIndex: 2
              Id: f02b16c9-bf13-4e97-8b67-696092259835
              InnerException: 
                ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.NoSuchMemberException
                InferredTypeOfTarget: <Playground.Domain.Company>
                MemberName: LastName
                Type: NoSuchFieldOrProp
                Root: [/()/arg0:λ/() -> c.LolMethod9(c, e => e.LastName)]
                Expression: [/()/arg0:λ/()/arg1:λ/f:LastName -> e.LastName]
                ExpressionType: MemberAccess
                IsUnexpected: False
                Id: 83865e34-235d-4723-8fd7-696471676056
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
        Expression: [/()/arg0:λ/() -> c.LolMethod9(c, e => e.LastName)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: f1ebf8bf-c135-4482-b936-1e290f250d97
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 08601c99-e092-4b59-a1fa-2d09c1a3ccc2
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 4c9c6ac2-af6d-418e-a31f-ab13eb8b857c
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod9(c, e => e.LastName))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_InferencesContradictGenericConstraints2()
        {
            try
            {
                var js = "ctx.Companies.ElementAt(0).LolMethod10(ctx.Companies.ElementAt(0), function(c1){return c1.Id;})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.ElementAt(0).LolMethod10(ctx.Companies.ElementAt(0), function(c1){return c1.Id;})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG LolMethod10: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
    FormalArguments: System.Type[]
    FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
    FormalArguments[1]: U
    FormalArguments[2]: System.Func`2[U, S]
    RootExpression: [/() -> ctx.Companies.ElementAt(0).LolMethod10(ctx.Companies.ElementAt(0), c1 => c1.Id)]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <Playground.Domain.Company>
    ActualArguments[1]: <Playground.Domain.Company>
    ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 0e12247d-17a0-4105-8c14-ca4070161059
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: InferencesContradictGenericConstraints
      IsUnexpected: False
      OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
      FormalArguments: System.Type[]
      FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
      FormalArguments[1]: U
      FormalArguments[2]: System.Func`2[U, S]
      RootExpression: [/() -> ctx.Companies.ElementAt(0).LolMethod10(ctx.Companies.ElementAt(0), c1 => c1.Id)]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <Playground.Domain.Company>
      ActualArguments[1]: <Playground.Domain.Company>
      ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 2
      Id: 4b2ea876-ec9f-4a23-a3b8-f3a69f444d1e
", ex1_kvps["Failboats[0]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.ElementAt(0).LolMethod10(ctx.Companies.ElementAt(0), c1 => c1.Id)]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.ElementAt(0).LolMethod10(ctx.Companies.ElementAt(0), c1 => c1.Id)]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_InferencesContradictGenericConstraints3()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return c.LolMethod10(c, function(c1){return c1.Id;});})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod10(c, function(c1){return c1.Id;});})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: c06f2c70-5a28-4f2e-90a6-732f69f2b60b
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 951472dc-3eee-4120-8acb-18201a46c891
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod10: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, S]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod10(c, c1 => c1.Id)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: d1dde3fb-0b21-4b67-9129-3cf4444bd8b0
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: InferencesContradictGenericConstraints
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: U
              FormalArguments[2]: System.Func`2[U, S]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod10(c, c1 => c1.Id)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
              MismatchIndex: 2
              Id: 73e5f14f-288c-4a85-9fd2-c1fdb0af475c
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
        Expression: [/()/arg0:λ/() -> c.LolMethod10(c, c1 => c1.Id)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 08888809-1900-47fb-9ad0-b901be7b9adc
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 9430831b-19b0-4bc5-925e-ab75c4f5e987
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 78a5662d-aabc-4a77-a493-0baf7a7aa142
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 5c4ec47d-51bd-4a5b-bb02-223c179e22c2
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 0be6c613-d7c0-4ab0-9cc0-47fc62076753
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod10: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, S]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod10(c, c1 => c1.Id)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: fc0dc602-cb00-4cb1-b503-3ed1075213f8
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: InferencesContradictGenericConstraints
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, S] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: U
              FormalArguments[2]: System.Func`2[U, S]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod10(c, c1 => c1.Id)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
              MismatchIndex: 2
              Id: 8c348b65-6469-42b3-ab7e-a9719f720a30
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
        Expression: [/()/arg0:λ/() -> c.LolMethod10(c, c1 => c1.Id)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: a1c0af82-bdb7-4643-98fc-b7c37aaf3a0d
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: debf412e-0cbb-4b0b-bdd2-c7c37cd05814
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 41f89475-8a74-4c79-8773-64a6616dcb27
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod10(c, c1 => c1.Id))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_LambdaArgcMismatch2()
        {
            try
            {
                var js = "ctx.Companies.Where(function(c){return c.LolMethod10(c, function(c1, c2){return c1 == c2;});})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod10(c, function(c1, c2){return c1 == c2;});})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 0a767c0b-8da0-4e49-8334-7d3fe5030066
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: eb0ad90c-e937-4863-a1ff-37207e96dae8
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod10: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, System.Nullable`1[S]] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, System.Nullable`1[S]] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, System.Nullable`1[S]] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, System.Nullable`1[S]]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod10(c, (c1, c2) => c1 == c2)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`3[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: b683de6e-4bc4-42cb-b7ed-e7d09f632b61
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: LambdaArgcMismatch
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, System.Nullable`1[S]] f)
              InferredSignature: System.Boolean LolMethod10`2[Playground.Domain.Company, S](Playground.Domain.Company u, System.Func`2[Playground.Domain.Company, System.Nullable`1[S]] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.Company
              FormalArguments[2]: System.Func`2[Playground.Domain.Company, System.Nullable`1[S]]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod10(c, (c1, c2) => c1 == c2)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`3[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
              MismatchIndex: 2
              Id: 3104d3d0-fc78-4825-835a-3b83416d4a20
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
        Expression: [/()/arg0:λ/() -> c.LolMethod10(c, (c1, c2) => c1 == c2)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: e8292965-5c1d-45a8-bde2-affdad01e439
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 846b42c0-5cf2-4c6e-bf05-e4180f894d43
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 7d3ead35-618c-4d66-9461-0ec1fc1e55a4
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 30bb1bbb-c3da-49a6-99e9-b8fe002d29f7
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 1094f84f-1f05-4ca7-8d45-693b1c6d3fd4
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod10: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, System.Nullable`1[S]] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, System.Nullable`1[S]] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, System.Nullable`1[S]] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, System.Nullable`1[S]]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod10(c, (c1, c2) => c1 == c2)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`3[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: 308bc7cb-8367-44ec-aed1-8cb27212f3e2
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: LambdaArgcMismatch
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod10`2[U, S](U u, System.Func`2[U, System.Nullable`1[S]] f)
              InferredSignature: System.Boolean LolMethod10`2[Playground.Domain.Company, S](Playground.Domain.Company u, System.Func`2[Playground.Domain.Company, System.Nullable`1[S]] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.Company
              FormalArguments[2]: System.Func`2[Playground.Domain.Company, System.Nullable`1[S]]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod10(c, (c1, c2) => c1 == c2)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`3[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
              MismatchIndex: 2
              Id: 6f7d9404-f79c-4cc7-a15c-6169a0dbabde
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
        Expression: [/()/arg0:λ/() -> c.LolMethod10(c, (c1, c2) => c1 == c2)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: dcc8d7c0-3310-4d92-8957-5a17067d88be
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 2c343469-aecc-419b-9ef0-dcb5af71fb57
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: e323fa8e-16de-4225-809e-5b0d4138a859
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod10(c, (c1, c2) => c1 == c2))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }

        [Test]
        public void TestGargInference_IncompleteInferenceSet2()
        {
            try
            {
                // todo. failboat exception
                var js = "ctx.Companies.Where(function(c){return c.LolMethod11(c, function(c1){return true;});})";
                TestTransformationFramework.JSToCSharp(js);
            }
            catch (Exception ex)
            {
                var ex0 = ex;
                Assert.AreEqual(typeof(JSToCSharpException), ex0.GetType());
                Assert.IsNotNull(ex0.InnerException);

                var ex0_kvps = ((RelinqException)ex0).PrettyProperties;
                Assert.AreEqual("FruitlessMethodGroupResolution", ex0_kvps["Type"]);
                Assert.AreEqual("ctx.Companies.Where(function(c){return c.LolMethod11(c, function(c1){return true;});})", ex0_kvps["Input"]);
                Assert.AreEqual("False", ex0_kvps["IsUnexpected"]);

                var ex1 = ex0.InnerException;
                Assert.AreEqual(typeof(MethodGroupResolutionFailedException), ex1.GetType());
                Assert.IsNull(ex1.InnerException);

                var ex1_kvps = ((RelinqException)ex1).PrettyProperties;
                Assert.AreEqual("[MG Where: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate), System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate), System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)]", ex1_kvps["MethodGroup"]);
                Assert.AreEqual("System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]", ex1_kvps["Failboats"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`2[TSource, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 3deb88e8-7afd-455e-bd38-e10dfa207e3d
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`2[TSource, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`2[Playground.Domain.Company, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`2[Playground.Domain.Company, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: d91545cf-0ec8-4285-b218-594e80640a8d
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod11: System.Boolean LolMethod11`2[U, S](U u, System.Func`2[U, System.Boolean] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod11`2[U, S](U u, System.Func`2[U, System.Boolean] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod11`2[U, S](U u, System.Func`2[U, System.Boolean] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, System.Boolean]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod11(c, c1 => true)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: 19d89a5a-32da-4b8e-9be3-9f570158f147
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: IncompleteInferenceSet
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod11`2[U, S](U u, System.Func`2[U, System.Boolean] f)
              InferredSignature: System.Boolean LolMethod11`2[Playground.Domain.Company, S](Playground.Domain.Company u, System.Func`2[Playground.Domain.Company, System.Boolean] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.Company
              FormalArguments[2]: System.Func`2[Playground.Domain.Company, System.Boolean]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod11(c, c1 => true)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
              Id: a725f49b-2429-4eea-9791-5d428129feee
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
        Expression: [/()/arg0:λ/() -> c.LolMethod11(c, c1 => true)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 118b4079-3f49-4730-85c2-0d6eaa704c28
", ex1_kvps["Failboats[0]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Collections.Generic.IEnumerable`1[TSource]
    FormalArguments[1]: System.Func`3[TSource, System.Int32, System.Boolean]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 1b538446-0709-45b2-b6ae-45b491d9f7fa
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Collections.Generic.IEnumerable`1[TSource] [Extension] Where`1[TSource](System.Collections.Generic.IEnumerable`1[TSource] source, System.Func`3[TSource, System.Int32, System.Boolean] predicate)
      InferredSignature: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Collections.Generic.IEnumerable`1[Playground.Domain.Company] source, System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Collections.Generic.IEnumerable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 29c14a7a-1464-40cd-b7e3-08f08480f37d
", ex1_kvps["Failboats[1]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 7aa89cc5-ebbf-45f2-a9d2-675ff4ce5568
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaInconsistentBody
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`2[TSource, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`2[Playground.Domain.Company, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 6a542822-dfe6-4831-8eb5-6b987de708cd
      InnerException: 
        ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodGroupResolutionFailedException
        MethodGroup: [MG LolMethod11: System.Boolean LolMethod11`2[U, S](U u, System.Func`2[U, System.Boolean] f)]
        Failboats: System.Collections.Generic.Dictionary`2[System.Reflection.MethodInfo,Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodResolutionException]
        Failboats[0]: 
          Key: System.Boolean LolMethod11`2[U, S](U u, System.Func`2[U, System.Boolean] f)
          Value: 
            ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
            Type: GenericArgInferenceFailed
            IsUnexpected: False
            OriginalSignature: System.Boolean LolMethod11`2[U, S](U u, System.Func`2[U, System.Boolean] f)
            FormalArguments: System.Type[]
            FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
            FormalArguments[1]: U
            FormalArguments[2]: System.Func`2[U, System.Boolean]
            RootExpression: [/()/arg0:λ/() -> c.LolMethod11(c, c1 => true)]
            ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
            ActualArguments[0]: <Playground.Domain.Company>
            ActualArguments[1]: <Playground.Domain.Company>
            ActualArguments[2]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
            Id: 82b19907-9b04-419b-be28-6bb1b46043b7
            InnerException: 
              ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
              Type: IncompleteInferenceSet
              IsUnexpected: False
              OriginalSignature: System.Boolean LolMethod11`2[U, S](U u, System.Func`2[U, System.Boolean] f)
              InferredSignature: System.Boolean LolMethod11`2[Playground.Domain.Company, S](Playground.Domain.Company u, System.Func`2[Playground.Domain.Company, System.Boolean] f)
              FormalArguments: System.Type[]
              FormalArguments[0]: Relinq.Script.Semantics.TypeSystem.This
              FormalArguments[1]: Playground.Domain.Company
              FormalArguments[2]: System.Func`2[Playground.Domain.Company, System.Boolean]
              RootExpression: [/()/arg0:λ/() -> c.LolMethod11(c, c1 => true)]
              ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
              ActualArguments[0]: <Playground.Domain.Company>
              ActualArguments[1]: <Playground.Domain.Company>
              ActualArguments[2]: [λ: <System.Func`2[Playground.Domain.Company, Relinq.Script.Semantics.TypeSystem.Variant]>]
              Id: 2fdc2ae9-d269-4e60-a906-052ce1f1ddb3
        Type: FruitlessMethodGroupResolution
        Root: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
        Expression: [/()/arg0:λ/() -> c.LolMethod11(c, c1 => true)]
        ExpressionType: Invoke
        IsUnexpected: False
        Id: 1acf6ef4-e995-41e8-88fc-ace56a1b9edf
", ex1_kvps["Failboats[2]"]);
                RelinqExceptionSupport.AssertAreEqualExceptGuids(@"
  Key: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
  Value: 
    ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.MethodMatchingException
    Type: GenericArgInferenceFailed
    IsUnexpected: False
    OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
    FormalArguments: System.Type[]
    FormalArguments[0]: System.Linq.IQueryable`1[TSource]
    FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]]
    RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
    ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
    ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
    ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
    Id: 4d30ec8a-274e-4efa-baa3-5a9189fc9e86
    InnerException: 
      ExceptionType: Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution.GenericArgumentsInferenceException
      Type: LambdaArgcMismatch
      IsUnexpected: False
      OriginalSignature: System.Linq.IQueryable`1[TSource] [Extension] Where`1[TSource](System.Linq.IQueryable`1[TSource] source, System.Linq.Expressions.Expression`1[System.Func`3[TSource, System.Int32, System.Boolean]] predicate)
      InferredSignature: System.Linq.IQueryable`1[Playground.Domain.Company] [Extension] Where`1[Playground.Domain.Company](System.Linq.IQueryable`1[Playground.Domain.Company] source, System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]] predicate)
      FormalArguments: System.Type[]
      FormalArguments[0]: System.Linq.IQueryable`1[Playground.Domain.Company]
      FormalArguments[1]: System.Linq.Expressions.Expression`1[System.Func`3[Playground.Domain.Company, System.Int32, System.Boolean]]
      RootExpression: [/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]
      ActualArguments: Relinq.Script.Semantics.TypeSystem.RelinqScriptType[]
      ActualArguments[0]: <System.Linq.IQueryable`1[Playground.Domain.Company]>
      ActualArguments[1]: [λ: <System.Func`2[Relinq.Script.Semantics.TypeSystem.Variant, Relinq.Script.Semantics.TypeSystem.Variant]>]
      MismatchIndex: 1
      Id: 2f7a1b33-3e01-4273-b1ad-ef466db95bf0
", ex1_kvps["Failboats[3]"]);
                Assert.AreEqual("FruitlessMethodGroupResolution", ex1_kvps["Type"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]", ex1_kvps["Root"]);
                Assert.AreEqual("[/() -> ctx.Companies.Where(c => c.LolMethod11(c, c1 => true))]", ex1_kvps["Expression"]);
                Assert.AreEqual("Invoke", ex1_kvps["ExpressionType"]);
                Assert.AreEqual("False", ex1_kvps["IsUnexpected"]);

                return;
            }

            Assert.Fail("Expected exception wasn't thrown");
        }
    }
}