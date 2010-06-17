using System;
using System.Reflection;
using Relinq.Linq.Evaluation;
using Relinq.Script.Semantics.Literals;

namespace Relinq.Infrastructure.Shared
{
    public static class RemoteTransportMock
    {
        public static String Relinq(String serialized)
        {
            var asm = Assembly.Load("Relinq.Playground");
            var ctxType = asm.GetType("Relinq.Playground.DataContexts.TestServerDataContext");
            var ctx = Activator.CreateInstance(ctxType);

            var transformationFramework = new TransformationFramework();
            transformationFramework.Integration.RegisterJS("ctx", ctx);

            var queryAtServer = transformationFramework.JSToCSharp(serialized);
            var resultAtServer = queryAtServer.Evaluate();
            var serializedResultAtServer = JsonSerializer.Serialize(resultAtServer, queryAtServer.Type);
            return serializedResultAtServer;
        }
    }
}