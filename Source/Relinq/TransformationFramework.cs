using System;
using System.Linq.Expressions;
using Relinq.Exceptions.CSharpToJS;
using Relinq.Exceptions.JSToCSharp;
using Relinq.Linq.Preprocessing;
using Relinq.Script;
using Relinq.Script.Integration;
using Relinq.Linq;

namespace Relinq
{
    // todo. after exception refactoring is complete, search for all throws in the solution

    public class TransformationFramework
    {
        public IntegrationContext Integration { get; private set; }

        public TransformationFramework()
        {
            Integration = new IntegrationContext();
        }

        public String CSharpToJS(Expression e)
        {
            try
            {
                var pp = new Inliner(e).Visit();
                pp = new CoalesceHandler(pp).Visit();
                pp = new Funcletizer(pp).Visit();

                var builder = new RelinqScriptBuilder(pp, Integration);
                return builder.VisitThenAcq(b => b.Script.ToString());
            }
            catch (Exception ex)
            {
                throw new CSharpToJSException(e, ex);
            }
        }

        public Expression JSToCSharp(String s)
        {
            try
            {
                return new RelinqScriptCompiler(Integration).Compile(s);
            }
            catch (Exception ex)
            {
                throw new JSToCSharpException(s, ex);
            }
        }
    }
}