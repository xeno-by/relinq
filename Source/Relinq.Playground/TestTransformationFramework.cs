using System;
using System.Linq.Expressions;
using Relinq;
using Relinq.Infrastructure.Client.Beans;
using Relinq.Playground.DataContexts;
using Relinq.Script.Integration;

namespace Relinq.Playground
{
    public static class TestTransformationFramework
    {
        private static readonly TransformationFramework _tranformationFramework = new TransformationFramework();
        public static IntegrationContext Integration { get { return _tranformationFramework.Integration; } }

        static TestTransformationFramework()
        {
            _tranformationFramework.Integration.RegisterCSharp(
                o => o is IBean,
                o => "ctx." + (String)o.GetType().GetProperty("Name").GetValue(o, null));
            _tranformationFramework.Integration.RegisterJS("ctx", new TestServerDataContext());
        }

        public static String CSharpToJS(Expression e)
        {
            return _tranformationFramework.CSharpToJS(e);
        }

        public static Expression JSToCSharp(String s)
        {
            return _tranformationFramework.JSToCSharp(s);
        }
    }
}