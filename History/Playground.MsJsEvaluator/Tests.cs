using System;
using System.Security;
using Microsoft.JScript;
using Microsoft.JScript.Vsa;
using NUnit.Framework;

namespace Playground.JSEvaluator
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestJsEvaluatorPregeneratedCode()
        {
            // http://www.odetocode.com/Code/80.aspx
            var eval = new Playground.JSEvaluator.Evaluator();

            var result = int.Parse(eval.Eval("2 * 2", true));
            Assert.AreEqual(4, result);

            var result2 = eval.Eval(
                "var f = function(s){return s + ' (from JS)';}; f('Hello Dolly');", true);
            Assert.AreEqual("Hello Dolly (from JS)", result2);
        }

        [Test]
        [ExpectedException(typeof(SecurityException))]
        public void TestJsEvaluatorPregeneratedCodeDisallowUnsafe()
        {
            var eval = new Playground.JSEvaluator.Evaluator();

            var result = int.Parse(eval.Eval("2 * 2", false));
            Assert.AreEqual(4, result);

            var result2 = eval.Eval(
                "var f = function(s){return s + ' (from JS)';}; f('Hello Dolly');", false);
            Assert.AreEqual("Hello Dolly (from JS)", result2);
        }

        [Test]
        public void TestJsEvaluatorFullTrust()
        {
            // http://west-wind.com/WebLog/posts/10688.aspx

            var engine = VsaEngine.CreateEngine();
            var result2 = (Closure)Eval.JScriptEvaluate("(function(s){return s + ' (from JS)';})", engine);
            var invocationResult = result2.Invoke(result2, new object[] { "Hello Dolly" });
            var resultString = (String)Microsoft.JScript.Convert.Coerce(invocationResult, typeof(String));
            Assert.AreEqual("Hello Dolly (from JS)", resultString);
        }
    }
}
