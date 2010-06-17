using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Relinq.V2.Helpers.Debug;
using Relinq.V2.RelinqScript.Compiler2.Expressions;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.Contradictions;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference
{
    public abstract class FuzzyMetadata : DebuggableObject
    {
        protected TypeInferenceEngine Engine { get { return TypeInferenceEngine.Current; } }
        private TypeInferenceUnit _host;
        public IEnumerable<TypeLink> Links { get; set; }
        public abstract bool IsFullyInferred { get; }

        public event EventHandler SomeMoreStuffInferred;
        public event ContradictionEventHandler ContradictionReported;

        // Sometimes type inference engine needs to break a tie between two fuzzy metadata entities.
        //
        // When there's no other ways to fix the stuff, Rank provides the last resort 
        // of determistic tiebreaking. The type with lower Rank wins (i.e. changes are propagated downstairs).
        //
        // A simple example when it's necessary is Identity syncing of two fuzzy types 
        // that both have Parameter fuzziness level but have different generic parameter types.
        //
        // Obviously, we don't need two different types to represent absolutely the same info, 
        // so we must decide what type is to stay and what will be removed from the AST.
        // From the other side, our decision needs to be deterministic, so that we don't enter infinite cycle.
        //
        // In that case ranks save the day.

        // upd. So far I can't come with non-nasty implementation of this concept.
        // Current one requires setting up these fucking Ranks whenever the fuzzy metadata is created
        // and this really sucks because it can be created God knows where.
        // What's more Ranks are the sole reason why fuzzy metadata stores references to its Host.
        // Setting the Host is a comparable PITA to the situation described above.

//        public int Rank { get; set; }

        // upd. Yeah, I've fucked up the principles in the sake of simplified debug...
        // upd2. "Simplified" debug. Mwahahahaha.

        public virtual TypeInferenceUnit Host
        {
            get { return _host; }
            set
            {
                _host = value;
                RegDebuggableParent(value);
            }
        }

        protected FuzzyMetadata()
        {
            Host = TypeInferenceUnit.Current;
            Links = Enumerable.Empty<TypeLink>();
        }

        public virtual void FireSomeMoreStuffInferred()
        {
            if (SomeMoreStuffInferred != null)
                SomeMoreStuffInferred(this, EventArgs.Empty);
        }

        public virtual void ReportContradiction(String whatHappened)
        {
            if (ContradictionReported != null)
                ContradictionReported(this, new ContradictionEventArgs(whatHappened));
        }

        protected abstract String FuzzyTypeCode { get; }
        protected abstract String ContentToString();

        public override String ToString()
        {
            var stack = new Stack<String>();

            for (var current = this; current != null; current = current.DebuggableParents
                .OfType<FuzzyMetadata>().SingleOrDefault())
                stack.Push("/" + current.ContentWithRoleToString());

            var path = new StringBuilder();
            while (stack.Count != 0) path.Append(stack.Pop());

            return String.Format("({0}{1}) {2} at {3}", FuzzyTypeCode, Id, path, Host == null ? "null" : Host.ToString());
        }

        private String ContentWithRoleToString()
        {
            var prefix = String.IsNullOrEmpty(Desc) ? String.Empty : (Desc + ":");
            return prefix + ContentToString();
        }

        public String ShortToString()
        {
            return String.Format("({0}{1}) {2}", FuzzyTypeCode, Id, ContentWithRoleToString());
        }
    }
}