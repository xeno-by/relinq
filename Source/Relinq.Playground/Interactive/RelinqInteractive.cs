using System;
using Relinq;
using Relinq.Linq.Evaluation;

namespace Relinq.Playground.Interactive
{
    public class RelinqInteractive
    {
        private TransformationFramework _fwk = new TransformationFramework();

        public Object Run(String js)
        {
            return _fwk.JSToCSharp(js).Evaluate();
        }

        public void RegisterKeyword(String name, Object instance)
        {
            _fwk.Integration.RegisterJS(name, instance);
        }

        public void UnregisterKeyword(String name)
        {
            throw new NotImplementedException();
        }
    }
}
