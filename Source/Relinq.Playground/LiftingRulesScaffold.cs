using System;
using System.IO;
using NUnit.Framework;
using System.Linq;
using Relinq.Script.Semantics.Lifting;
using Relinq.Script.Syntax.Operators;
using Relinq.Helpers.Reflection;
using Relinq.Script.Semantics.Operators;
using Relinq.Helpers.Collections;

namespace Relinq.Playground
{
    [TestFixture]
    public class LiftingRulesScaffold
    {
        [Test]
        public void GenerateLiftedSignaturesGeneralCase()
        {
//            using (var sw = new StreamWriter(@"d:\pew-sigs.txt", false))
//            {
//                foreach (var m in typeof(IReferencePredefinedSignatures).GetMethods())
//                {
//                    sw.WriteLine("[SourceSignature"+
//                        "(typeof(IReferencePredefinedSignatures), \"" + m + "\")]");
//                }
//
//                foreach (var m in typeof(S<>).GetMethods()
//                    .Where(mi => mi.IsUserDefinedOperator()))
//                {
//                    sw.WriteLine("[SourceSignature" +
//                        "(typeof(S<>), \"" + m + "\")]");
//                }
//
//                foreach (var m in typeof(S<int>).GetMethods()
//                    .Where(mi => mi.IsUserDefinedOperator()))
//                {
//                    sw.WriteLine("[SourceSignature" +
//                        "(typeof(S<int>), \"" + m + "\")]");
//                }
//            }

            var predefinedSigs = OperatorType.Equal.GetPredefinedOperators().Concat(
                OperatorType.NotEqual.GetPredefinedOperators());
            var userDefinedSigs = typeof(S<>).GetMethods().Concat(
                typeof(S<int>).GetMethods()).Where(mi => mi.IsUserDefinedOperator());

            if (userDefinedSigs.LiftSignatures().IsNullOrEmpty())
            {
                Assert.Fail("U/d coercions lifting is NYI");
            }

            var asm = LiftingRules.LiftImpl(predefinedSigs.Concat(userDefinedSigs));
            asm.Save("signatures.dll"); 
        }
    }
}
