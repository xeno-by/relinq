using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Relinq.Exceptions.JSToCSharp.Parser;
using Relinq.Helpers.Collections;
using Relinq.Script.Compilation;
using Relinq.Script.Syntax.Expressions;
using System.Linq;

namespace Relinq.Playground
{
    [TestFixture]
    public class RelinqScriptParserTests
    {
        [Test]
        public void RelinqScriptParserWtf1()
        {
            var js = "2/2";
            var ast = new RelinqScriptParser(js, TestTransformationFramework.Integration).Parse();

            var paths = GetAllRootToLeafPaths(ast);

//            using (var fs = new StreamWriter(@"d:\pew-parser1.txt", false))
//            {
//                fs.WriteLine(String.Format("Assert.AreEqual({0}, paths.Count);", paths.Count));
//                for (var i = 0; i < paths.Count; i++)
//                {
//                    var s = paths[i];
//                    fs.WriteLine(String.Format(
//                        "Assert.AreEqual(\"{0}\", paths.ElementAt({1}));",
//                        s.ToVerbatimCSharpCopy(), i));
//                }
//            }

            Assert.AreEqual(2, paths.Count);
            Assert.AreEqual("///c:(2, DecimalIntegerLiteral)", paths.ElementAt(0));
            Assert.AreEqual("///c:(2, DecimalIntegerLiteral)", paths.ElementAt(1));
        }

        [Test]
        [ExpectedException(typeof(SemanticErrorException))]
        public void RelinqScriptParserWtf2()
        {
            var js = "2;a*2;";
            var ast = new RelinqScriptParser(js, TestTransformationFramework.Integration).Parse();
        }

        [Test]
        public void RelinqScriptParserPotpourri()
        {
            var js = "({c:c}).f(function($0){return (2+2)*$0[2]}, [{$0:1}, {$0:2}])";
            var ast = new RelinqScriptParser(js, TestTransformationFramework.Integration).Parse();

            var paths = GetAllRootToLeafPaths(ast);

//            using(var fs = new StreamWriter(@"d:\pew-parser2.txt", false))
//            {
//                fs.WriteLine(String.Format("Assert.AreEqual({0}, paths.Count);", paths.Count));
//                for (var i = 0; i < paths.Count; i++)
//                {
//                    var s = paths[i];
//                    fs.WriteLine(String.Format(
//                        "Assert.AreEqual(\"{0}\", paths.ElementAt({1}));",
//                        s.ToVerbatimCSharpCopy(), i));
//                }
//            }

            Assert.AreEqual(6, paths.Count);
            Assert.AreEqual("/()/f:f/new/v:c", paths.ElementAt(0));
            Assert.AreEqual("/()/λ/*/+/c:(2, DecimalIntegerLiteral)", paths.ElementAt(1));
            Assert.AreEqual("/()/λ/*/+/c:(2, DecimalIntegerLiteral)", paths.ElementAt(2));
            Assert.AreEqual("/()/λ/*/[]/v:$0", paths.ElementAt(3));
            Assert.AreEqual("/()/λ/*/[]/c:(2, DecimalIntegerLiteral)", paths.ElementAt(4));
            Assert.AreEqual("/()/c:([{$0 : 1}, {$0 : 2}], ARRAY)", paths.ElementAt(5));
        }

        private List<String> GetAllRootToLeafPaths(RelinqScriptExpression root)
        {
            var ln = new List<String>();
            SeekRootToLeafPathsRecursive(root, ln);
            return ln;
        }

        private void SeekRootToLeafPathsRecursive(RelinqScriptExpression node, List<String> ln)
        {
            if (node.Children.Count() == 0) ln.Add(node.ShortTPath);
            node.Children.ForEach(child => SeekRootToLeafPathsRecursive(child, ln));
        }

        private String Rle(String path, int threshold)
        {
            var shortened = path.Replace("/()/f:Select", "s");
            shortened = shortened.Replace("/()/f:GroupBy", "g");
            shortened = shortened.Replace("/()/f:OrderBy", "o");
            shortened = shortened.Replace("/()/f:Where", "w");

            var rle = new StringBuilder();
            char? runChar = null;
            var runLength = 0;

            Action<char?, int> encodeRun = (c, len) => {
                if (c != null)
                {
                    if (len > threshold) rle.Append(c.Value).Append(len);
                    else rle.Append(c.Value, len);
                }};

            foreach (var schar in shortened)
            {
                if (schar == runChar) 
                {
                    ++runLength;
                }
                else
                {
                    encodeRun(runChar, runLength);
                    runChar = schar;
                    runLength = 1;
                }
            }

            encodeRun(runChar, runLength);
            return rle.ToString();
        }

        [Test]
        public void RelinqScriptBuilderMonstrousTestNightmaresGoneLive()
        {
            var js = TestTransformationFramework.CSharpToJS(RelinqSuiteTests.Monster.Expression);
            var ast = new RelinqScriptParser(js, TestTransformationFramework.Integration).Parse();

            var paths = GetAllRootToLeafPaths(ast).Select(p => Rle(p, 2)).ToList();

//            using(var fs = new StreamWriter(@"d:\pew-parser3.txt", false))
//            {
//                fs.WriteLine(String.Format("Assert.AreEqual({0}, paths.Count);", paths.Count));
//                for (var i = 0; i < paths.Count; i++)
//                {
//                    var s = paths[i];
//                    fs.WriteLine(String.Format(
//                        "Assert.AreEqual(\"{0}\", paths.ElementAt({1}));", 
//                        s.ToVerbatimCSharpCopy(), i));
//                }
//            }

            Assert.AreEqual(92, paths.Count);
            Assert.AreEqual("sgsow11s11/f:Companies/k:ctx", paths.ElementAt(0));
            Assert.AreEqual("sgsow11s10/()/λ/new/v:c", paths.ElementAt(1));
            Assert.AreEqual("sgsow11s10/()/λ/new/c:('ee1e42c3-1d16-465a-b2b1-bcdbc645db3b', StringLiteral)", paths.ElementAt(2));
            Assert.AreEqual("sgsow11s9/()/λ/new/v:$0", paths.ElementAt(3));
            Assert.AreEqual("sgsow11s9/()/λ/new/c:('2', StringLiteral)", paths.ElementAt(4));
            Assert.AreEqual("sgsow11s8/()/λ/new/v:$2", paths.ElementAt(5));
            Assert.AreEqual("sgsow11s8/()/λ/new/()/f:D/f:c/f:$0/v:$2", paths.ElementAt(6));
            Assert.AreEqual("sgsow11s7/()/λ/new/v:$4", paths.ElementAt(7));
            Assert.AreEqual("sgsow11s7/()/λ/new/[]/f:Array1D/f:c/f:$0/f:$2/v:$4", paths.ElementAt(8));
            Assert.AreEqual("sgsow11s7/()/λ/new/[]/c:(0, DecimalIntegerLiteral)", paths.ElementAt(9));
            Assert.AreEqual("sgsow11s6/()/λ/new/v:$6", paths.ElementAt(10));
            Assert.AreEqual("sgsow11s6/()/λ/new/[]/[]/f:Array2DJagged/f:c/f:$0/f:$2/f:$4/v:$6", paths.ElementAt(11));
            Assert.AreEqual("sgsow11s6/()/λ/new/[]/[]/c:(0, DecimalIntegerLiteral)", paths.ElementAt(12));
            Assert.AreEqual("sgsow11s6/()/λ/new/[]/c:(0, DecimalIntegerLiteral)", paths.ElementAt(13));
            Assert.AreEqual("sgsow11s5/()/λ/new/v:$8", paths.ElementAt(14));
            Assert.AreEqual("sgsow11s5/()/λ/new/[]/f:Array2DRect/f:c/f:$0/f:$2/f:$4/f:$6/v:$8", paths.ElementAt(15));
            Assert.AreEqual("sgsow11s5/()/λ/new/[]/c:(0, DecimalIntegerLiteral)", paths.ElementAt(16));
            Assert.AreEqual("sgsow11s5/()/λ/new/[]/c:(0, DecimalIntegerLiteral)", paths.ElementAt(17));
            Assert.AreEqual("sgsow11s4/()/λ/new/v:$10", paths.ElementAt(18));
            Assert.AreEqual("sgsow11s4/()/λ/new/f:GetHashCode/+/c:(2, DecimalIntegerLiteral)", paths.ElementAt(19));
            Assert.AreEqual("sgsow11s4/()/λ/new/f:GetHashCode/+/test/!=/f:Employees/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/v:$10", paths.ElementAt(20));
            Assert.AreEqual("sgsow11s4/()/λ/new/f:GetHashCode/+/test/!=/c:(null, NULL)", paths.ElementAt(21));
            Assert.AreEqual("sgsow11s4/()/λ/new/f:GetHashCode/+/test/c:(2, DecimalIntegerLiteral)", paths.ElementAt(22));
            Assert.AreEqual("sgsow11s4/()/λ/new/f:GetHashCode/+/test/c:(0, DecimalIntegerLiteral)", paths.ElementAt(23));
            Assert.AreEqual("sgsow11s3/()/λ/new/v:$12", paths.ElementAt(24));
            Assert.AreEqual("sgsow11s3/()/λ/new/f:Employees/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/v:$12", paths.ElementAt(25));
            Assert.AreEqual("sgsow11ss/()/λ/new/v:$14", paths.ElementAt(26));
            Assert.AreEqual("sgsow11ss/()/λ/new/test/!=/f:Id/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/v:$14", paths.ElementAt(27));
            Assert.AreEqual("sgsow11ss/()/λ/new/test/!=/c:(null, NULL)", paths.ElementAt(28));
            Assert.AreEqual("sgsow11ss/()/λ/new/test/f:Id/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/v:$14", paths.ElementAt(29));
            Assert.AreEqual("sgsow11ss/()/λ/new/test/c:('\\0\\u031\\b\\r\\n\\f\\t\\v\\uf4\\'\"\\\\abcdef', StringLiteral)", paths.ElementAt(30));
            Assert.AreEqual("sgsow11s/()/λ/new/v:$16", paths.ElementAt(31));
            Assert.AreEqual("sgsow11s/()/λ/new/()/f:ToUpper/+/f:Name/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/v:$16", paths.ElementAt(32));
            Assert.AreEqual("sgsow11s/()/λ/new/()/f:ToUpper/+/c:(' rocks', StringLiteral)", paths.ElementAt(33));
            Assert.AreEqual("sgsow11/()/λ/new/v:$18", paths.ElementAt(34));
            Assert.AreEqual("sgsow11/()/λ/new/test/==/()/f:mg/f:$12/f:$14/f:$16/v:$18", paths.ElementAt(35));
            Assert.AreEqual("sgsow11/()/λ/new/test/==/c:(4, DecimalIntegerLiteral)", paths.ElementAt(36));
            Assert.AreEqual("sgsow11/()/λ/new/test/f:mg/f:$12/f:$14/f:$16/v:$18", paths.ElementAt(37));
            Assert.AreEqual("sgsow11/()/λ/new/test/λ/c:(4, DecimalIntegerLiteral)", paths.ElementAt(38));
            Assert.AreEqual("sgsow10/()/λ/==/()/f:imba/v:$20", paths.ElementAt(39));
            Assert.AreEqual("sgsow10/()/λ/==/c:(4, DecimalIntegerLiteral)", paths.ElementAt(40));
            Assert.AreEqual("sgsow9/()/λ/==/[]/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(41));
            Assert.AreEqual("sgsow9/()/λ/==/[]/c:(4, DecimalIntegerLiteral)", paths.ElementAt(42));
            Assert.AreEqual("sgsow9/()/λ/==/[]/c:(2, DecimalIntegerLiteral)", paths.ElementAt(43));
            Assert.AreEqual("sgsow9/()/λ/==/c:(8, DecimalIntegerLiteral)", paths.ElementAt(44));
            Assert.AreEqual("sgsow8/()/λ/==/f:Description/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(45));
            Assert.AreEqual("sgsow8/()/λ/==/c:('Dummy', StringLiteral)", paths.ElementAt(46));
            Assert.AreEqual("sgsow7/()/λ/==/==/[]/f:Description/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(47));
            Assert.AreEqual("sgsow7/()/λ/==/==/[]/c:(0, DecimalIntegerLiteral)", paths.ElementAt(48));
            Assert.AreEqual("sgsow7/()/λ/==/==/c:('c', StringLiteral)", paths.ElementAt(49));
            Assert.AreEqual("sgsow7/()/λ/==/c:(true, TRUE)", paths.ElementAt(50));
            Assert.AreEqual("sgsow6/()/λ/()/f:LolMethod/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(51));
            Assert.AreEqual("sgsow6/()/λ/()/c:('c', StringLiteral)", paths.ElementAt(52));
            Assert.AreEqual("sgsow5/()/λ/()/f:LolMethod/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(53));
            Assert.AreEqual("sgsow5/()/λ/()/c:(0, DecimalIntegerLiteral)", paths.ElementAt(54));
            Assert.AreEqual("sgsow4/()/λ/>/f:FoundationDate/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(55));
            Assert.AreEqual("sgsow4/()/λ/>/c:('203-01-01', StringLiteral)", paths.ElementAt(56));
            Assert.AreEqual("sgsow3/()/λ/==/||/f:e/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(57));
            Assert.AreEqual("sgsow3/()/λ/==/||/f:e/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(58));
            Assert.AreEqual("sgsow3/()/λ/==/c:(true, TRUE)", paths.ElementAt(59));
            Assert.AreEqual("sgsoww/()/λ/==/test/!=/f:Employees/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(60));
            Assert.AreEqual("sgsoww/()/λ/==/test/!=/c:(null, NULL)", paths.ElementAt(61));
            Assert.AreEqual("sgsoww/()/λ/==/test/f:Employees/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(62));
            Assert.AreEqual("sgsoww/()/λ/==/test/test/!=/f:Employees/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(63));
            Assert.AreEqual("sgsoww/()/λ/==/test/test/!=/c:(null, NULL)", paths.ElementAt(64));
            Assert.AreEqual("sgsoww/()/λ/==/test/test/f:Employees/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(65));
            Assert.AreEqual("sgsoww/()/λ/==/test/test/f:Employees/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(66));
            Assert.AreEqual("sgsoww/()/λ/==/c:(true, TRUE)", paths.ElementAt(67));
            Assert.AreEqual("sgsow/()/λ/()/f:Any/()/f:Concat/f:e/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(68));
            Assert.AreEqual("sgsow/()/λ/()/f:Any/()/c:([{Id : null, FirstName : 'Dummy', LastName : null, CompanyId : null}], ARRAY)", paths.ElementAt(69));
            Assert.AreEqual("sgsow/()/λ/()/λ/==/f:FirstName/v:ae", paths.ElementAt(70));
            Assert.AreEqual("sgsow/()/λ/()/λ/==/c:('Dummy', StringLiteral)", paths.ElementAt(71));
            Assert.AreEqual("sgso/()/λ/()/f:LolMethod/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(72));
            Assert.AreEqual("sgso/()/λ/()/c:(0, DecimalIntegerLiteral)", paths.ElementAt(73));
            Assert.AreEqual("sgso/()/λ/()/f:i/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(74));
            Assert.AreEqual("sgso/()/λ/()/c:(2, DecimalIntegerLiteral)", paths.ElementAt(75));
            Assert.AreEqual("sgs/()/λ/f:FoundationDate/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(76));
            Assert.AreEqual("sg/()/λ/new/v:$20", paths.ElementAt(77));
            Assert.AreEqual("sg/()/λ/new/new/f:c/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(78));
            Assert.AreEqual("sg/()/λ/new/new/f:i/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(79));
            Assert.AreEqual("sg/()/λ/new/new/f:g/f:$0/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(80));
            Assert.AreEqual("sg/()/λ/new/new/f:n/f:$2/f:$4/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(81));
            Assert.AreEqual("sg/()/λ/new/new/f:ai/f:$6/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(82));
            Assert.AreEqual("sg/()/λ/new/new/f:ai_jagged/f:$8/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(83));
            Assert.AreEqual("sg/()/λ/new/new/f:ai_rect/f:$10/f:$12/f:$14/f:$16/f:$18/v:$20", paths.ElementAt(84));
            Assert.AreEqual("sg/()/λ/new/new/c:([[2, 'string'], [3, 'string']], ARRAY)", paths.ElementAt(85));
            Assert.AreEqual("sg/()/λ/new/new/c:([1, 2, 3], ARRAY)", paths.ElementAt(86));
            Assert.AreEqual("sg/()/λ/new/new/f:id/f:$16/f:$18/v:$20", paths.ElementAt(87));
            Assert.AreEqual("sg/()/λ/new/new/f:ts/f:$18/v:$20", paths.ElementAt(88));
            Assert.AreEqual("s/()/λ/f:Name/f:c/f:monster/v:$22", paths.ElementAt(89));
            Assert.AreEqual("s/()/λ/f:monster/v:$22", paths.ElementAt(90));
            Assert.AreEqual("/()/λ/v:monsterg", paths.ElementAt(91));
        }
    }
}
