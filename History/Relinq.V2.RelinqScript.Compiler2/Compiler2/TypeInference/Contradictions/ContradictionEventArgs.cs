using System;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference.Contradictions
{
    public class ContradictionEventArgs : EventArgs
    {
        public string WhatHappened { get; private set; }

        public ContradictionEventArgs(String whatHappened)
        {
            WhatHappened = whatHappened;
        }
    }
}