using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Strings;
using Relinq.Playground.DataContexts;

namespace Relinq.Playground
{
    [TestFixture]
    public class RelinqScriptBuilderTests
    {
        [Test]
        public void BracketsTest()
        {
            var some =
                from c in new TestClientDataContext().Companies
                let e = c.Employees
                where c.LolMethod(((int?)0) + (c.D == null ? 0 : 1))
                where (e || e) == true
                where e != null && -(-(e.Count)) < 10
                where e != null && ~(~(e.Count)) < 10
                where e != null && -(1 + e.Count) < 10
                select c;

            var js = TestTransformationFramework.CSharpToJS(some.Expression);
            Assert.AreEqual(
                "ctx.Companies.Select(function(c){return {c : c, e : c.Employees}})" +
                ".Where(function($0){return $0.c.LolMethod(0 + ($0.c.D == null ? 0 : 1))})" +
                ".Where(function($0){return ($0.e || $0.e) == true})" +
                ".Where(function($0){return $0.e != null && -(-$0.e.Count) < 10})" +
                ".Where(function($0){return $0.e != null && !!$0.e.Count < 10})" +
                ".Where(function($0){return $0.e != null && -(1 + $0.e.Count) < 10})" +
                ".Select(function($0){return $0.c})", js);
        }

        [Test]
        public void BracketsTest2()
        {
            var some =
                from s in new TestClientDataContext().Companies.Select(c => c.Name)
                where (s + s)[0] == (s + s[0])[0]
                select s;

            var js = TestTransformationFramework.CSharpToJS(some.Expression);
            Assert.AreEqual(
                "ctx.Companies.Select(function(c){return c.Name})" +
                ".Where(function(s){return (s + s)[0] == (s + s[0])[0]})", js);
        }

        private IEnumerable<String> PrettyPrintImpl(String js)
        {
            var newLineDelims = new []{".Select(", ".Where(", ".OrderBy(", ".GroupBy"};
            Func<int> delimPos = () => js.IsNullOrEmpty() ? -1 : newLineDelims
                .Select(delim => js.IndexOf(delim,1 ))
                .Aggregate(-1, (running, curr) => curr <= 0 ? 
                    running : (running == -1 ? curr : Math.Min(running, curr)));

            while(delimPos() != -1)
            {
                yield return js.Substring(0, delimPos()).ToVerbatimCSharpCopy();
                js = js.Substring(delimPos());
            }

            if (!js.IsNullOrEmpty()) 
                yield return js;
        }

        [Test]
        public void RelinqScriptBuilderMonstrousTest()
        {
            var js = TestTransformationFramework.CSharpToJS(RelinqSuiteTests.Monster.Expression);

//            using(var fs = new StreamWriter(@"d:\pew-builder.txt", false))
//            {
//                fs.Write("Assert.AreEqual(");
//                fs.Write(PrettyPrintImpl(js).Select(lineContent => "\"" + lineContent + "\"")
//                    .StringJoin(String.Format(" +{0}    ", Environment.NewLine)));
//                fs.Write(", js);");
//            }

            Assert.AreEqual("ctx.Companies" +
                ".Select(function(c){return {c : c, g : 'ee1e42c3-1d16-465a-b2b1-bcdbc645db3b'}})" +
                ".Select(function($0){return {$1 : $0, n : '2'}})" +
                ".Select(function($2){return {$3 : $2, i : $2.$0.c.D()}})" +
                ".Select(function($4){return {$5 : $4, ai : $4.$2.$0.c.Array1D[0]}})" +
                ".Select(function($6){return {$7 : $6, ai_jagged : $6.$4.$2.$0.c.Array2DJagged[0][0]}})" +
                ".Select(function($8){return {$9 : $8, ai_rect : $8.$6.$4.$2.$0.c.Array2DRect[0, 0]}})" +
                ".Select(function($10){return {$11 : $10, mg : ((2 + ($10.$8.$6.$4.$2.$0.c.Employees != null ? 2 : 0))).GetHashCode}})" +
                ".Select(function($12){return {$13 : $12, e : $12.$10.$8.$6.$4.$2.$0.c.Employees}})" +
                ".Select(function($14){return {$15 : $14, id : $14.$12.$10.$8.$6.$4.$2.$0.c.Id != null ? $14.$12.$10.$8.$6.$4.$2.$0.c.Id : '\\0\\u0001\\b\\r\\n\\f\\t\\v\\uffff\\'\"\\\\abcdef'}})" +
                ".Select(function($16){return {$17 : $16, ts : ($16.$14.$12.$10.$8.$6.$4.$2.$0.c.Name + ' rocks').ToUpper()}})" +
                ".Select(function($18){return {$19 : $18, imba : $18.$16.$14.$12.mg() == 4 ? $18.$16.$14.$12.mg : function(){return 4}}})" +
                ".Where(function($20){return $20.imba() == 4})" +
                ".Where(function($20){return $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c[4, 2] == 8})" +
                ".Where(function($20){return $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.Description == 'Dummy'})" +
                ".Where(function($20){return ($20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.Description[0] == 'c') == true})" +
                ".Where(function($20){return $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.LolMethod('c')})" +
                ".Where(function($20){return $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.LolMethod(0)})" +
                ".Where(function($20){return $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.FoundationDate > '2000-01-01'})" +
                ".Where(function($20){return ($20.$18.$16.$14.e || $20.$18.$16.$14.e) == true})" +
                ".Where(function($20){return ($20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.Employees != null ? $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.Employees : ($20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.Employees != null ? $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.Employees : $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.Employees)) == true})" +
                ".Where(function($20){return $20.$18.$16.$14.e.Concat([{Id : null, FirstName : 'Dummy', LastName : null, CompanyId : null}]).Any(function(ae){return ae.FirstName == 'Dummy'})})" +
                ".Where(function($20){return $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.LolMethod(0, $20.$18.$16.$14.$12.$10.$8.$6.$4.i, 2)})" +
                ".OrderBy(function($20){return $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c.FoundationDate})" +
                ".Select(function($20){return {$21 : $20, monster : {c : $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.c, i : $20.$18.$16.$14.$12.$10.$8.$6.$4.i, g : $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.$0.g, n : $20.$18.$16.$14.$12.$10.$8.$6.$4.$2.n, ai : $20.$18.$16.$14.$12.$10.$8.$6.ai, ai_jagged : $20.$18.$16.$14.$12.$10.$8.ai_jagged, ai_rect : $20.$18.$16.$14.$12.$10.ai_rect, d : [[2, 'string'], [3, 'string']], ie : [1, 2, 3], id : $20.$18.$16.id, ts : $20.$18.ts}}})" +
                ".GroupBy(function($22){return $22.monster.c.Name}, function($22){return $22.monster})" +
                ".Select(function(monsterg){return monsterg})", js);
        }
    }
}