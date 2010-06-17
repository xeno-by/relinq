using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using System.Linq;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks
{
    public class MethodBindingToOneOfAlternativesLink : IdentityLink
    {
        private readonly FuzzyType _first;
        private readonly FuzzyType _second;

        private readonly FuzzyMethodBinding _binding;
        private readonly FuzzyMethod _alt;

        public MethodBindingToOneOfAlternativesLink(FuzzyMethodBinding binding, FuzzyType first, FuzzyType second) 
            : base(first, second)
        {
            _first = first;
            _second = second;
            _binding = binding;

            // first of all check whether _second is Arg or RetVal of one of the alts
            var secondPointHost = _binding.Alternatives.SingleOrDefault(alt => HostsSecondTypePoint(alt));

            // if it's not, then _second is the Target, i.e. we can take it from any alt we wish for now
            _alt = secondPointHost ?? _binding.Alternatives.First();
        }

        protected override TypeLinkPolarity Polarity
        {
            get
            {
                // if the alternative isn't the only one for this binding, 
                // then it has no right to propagate changes to binding typepoints
                //
                // kek... at the very start of TIEN v2 I've classified links to simplex and duplex
                // then I just made links to be innately duplex since there wasn't need in simplex ones
                // however, current case proves the contrary

                if (_binding.Alternatives.Contains(_alt) &&
                    _binding.Alternatives.Count == 1)
                {
                    return TypeLinkPolarity.Neutral;
                }
                else
                {
                    return TypeLinkPolarity.Right;
                }
            }
        }
        
        private bool HostsSecondTypePoint(FuzzyMethod alt)
        {
            return //every freaking alt references the same target, so I have to comment this
                // alt.Target == _second ||
                alt.ReturnValue == _second ||
                alt.Args.Any(altArg => altArg == _second);
        }

        protected override void Sync(FuzzyMetadata sender, FuzzyMetadata recipient)
        {
            if (sender == _first)
            {
                base.Sync(sender, recipient);
            }
            else
            {
                // if the alternative isn't the only one for this binding, 
                // then it has no right to propagate changes to binding typepoints
                //
                // kek... at the very start of TIEN v2 I've classified links to simplex and duplex
                // then I just made links to be innately duplex since there wasn't need in simplex ones
                // however, current case proves the contrary

                if (Polarity != TypeLinkPolarity.Right)
                {
                    base.Sync(sender, recipient);
                }
            }
        }

        protected override void ReportContradiction(string whatHappened)
        {
            // just report contradiction using FuzzyMethod
            // this will get the alternative to disappear from the list
            // according to FuzzyMethodBinding constructor

            _alt.ReportContradiction(whatHappened);
        }
    }
}