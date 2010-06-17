using System;
using System.Collections.Generic;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.Contradictions;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using Relinq.V2.Helpers.Debug;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks
{
    public abstract class TypeLink : DebuggableObject 
    {
        protected TypeInferenceEngine Engine { get { return TypeInferenceEngine.Current; } }
        public FuzzyMetadata First { get; set; }
        public FuzzyMetadata Second { get; set; }
        protected abstract void Sync(FuzzyMetadata sender, FuzzyMetadata recipient);
        protected abstract TypeLinkPolarity Polarity { get; }

        protected TypeLink(FuzzyMetadata first, FuzzyMetadata second)
        {
            First = first;
            AssociateMetadataAndLink(first, this);

            Second = second;
            AssociateMetadataAndLink(second, this);

            Engine.RegisterLink(this);
        }

        private void AssociateMetadataAndLink(FuzzyMetadata metadata, TypeLink link)
        {
            link.RegDebuggableParent(metadata);

            var links = new List<TypeLink>(metadata.Links);
            links.Add(link);
            metadata.Links = links.AsReadOnly();

            metadata.SomeMoreStuffInferred += (o, e) => Engine.ScheduleSync(this, (FuzzyMetadata)o);
            metadata.ContradictionReported += (o, e) => ReportContradiction(String.Format(
                "'{0}' reports: '{1}'", o, e.WhatHappened));
        }

        public void Sync()
        {
            Sync(First, Second);
            Sync(Second, First);
        }

        public void Sync(FuzzyMetadata sender)
        {
            if (sender != First && sender != Second)
            {
                throw new ArgumentException("I don't care about alien type points");
            }
            else
            {
                Sync(sender, sender == First ? Second : First);
            }
        }

        public event ContradictionEventHandler ContradictionReported;

        protected virtual void ReportContradiction(String whatHappened)
        {
            if (ContradictionReported != null)
                ContradictionReported(this, new ContradictionEventArgs(whatHappened));
        }

        protected virtual String LinkTypeCode { get { return new String(GetType().Name[0], 1); } }
        protected String PolarityCode
        {
            get
            {
                switch (Polarity)
                {
                    case TypeLinkPolarity.Left:
                        return "<--";
                    case TypeLinkPolarity.Neutral:
                        return "<->";
                    case TypeLinkPolarity.Right:
                        return "-->";
                    default:
                        throw new NotSupportedException(Polarity.ToString());
                }
            }
        }

        public override string ToString()
        {
            return String.Format("({0}{1}) [{2} {3} {4}]", LinkTypeCode, Id, First.ShortToString(), PolarityCode, Second.ShortToString());
        }
    }
}