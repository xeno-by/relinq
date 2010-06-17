using System;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks
{
    public class IdentityLink : TypeLink
    {
        public IdentityLink(FuzzyType first, FuzzyType second) 
            : base(first, second)
        {
        }

        protected override TypeLinkPolarity Polarity 
        {
            get
            {
                return TypeLinkPolarity.Neutral;
            }
        }

        protected override void Sync(FuzzyMetadata sender, FuzzyMetadata recipient)
        {
            Sync((FuzzyType)sender, (FuzzyType)recipient);
        }

        private void Sync(FuzzyType sender, FuzzyType recipient)
        {
            // todo. possible refinement of type inference engine
            // here we might also match constraints to crash early or 
            // mix them to get more hints to aid us in type inference
            //
            // note that constraints cant be implemented in fuzzinesslevel-style, since
            // even completely Unknown type can define constraints useful for a Crisp type
            // the best way would be simply synchronize contents of the Constraints collection
            // regardless of fuzziness level of sender and recipient

            if (sender.FuzzinessLevel < recipient.FuzzinessLevel)
            {
                recipient.RuntimeType = sender.RuntimeType;
            }
            else if (sender.FuzzinessLevel > recipient.FuzzinessLevel)
            {
                // sender has nothing to say to the recipient
            }
            else if (sender.IsCrisp() && recipient.IsCrisp())
            {
                if (!sender.SameBasisType(recipient))
                {
                    ReportContradiction(String.Format(
                        "Sender '{0}' and recipient '{1}' have different basis types",
                        sender,
                        recipient));
                }
                else
                {
                    var senderArgs = sender.GenericArgs;
                    var recipientArgs = recipient.GenericArgs;

                    if (senderArgs.Count != recipientArgs.Count)
                    {
                        throw new ArgumentException(String.Format("This can't be: " +
                            "Sender '{0}' and recipient '{1}' claimed that they share basis type",
                            sender,
                            recipient));
                    }
                    else
                    {
                        for (var i = 0; i < senderArgs.Count; ++i)
                        {
                            Sync(senderArgs[i], recipientArgs[i]);
                        }
                    }
                }
            }
        }
    }
}