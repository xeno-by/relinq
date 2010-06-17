using System;
using System.Collections.Generic;
using Relinq.V2.Helpers.Collections;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using Relinq.V2.Helpers;
using System.Linq;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference
{
    public class FuzzyMethodBinding : FuzzyMetadata
    {
        public String Name { get; private set; }
        public TrackableList<FuzzyMethod> Alternatives { get; private set; }
        public override bool IsFullyInferred { get {
            return Alternatives.Count == 1 && Alternatives[0].IsFullyInferred;
        } }

        public FuzzyType Target { get; private set; }
        public FuzzyType ReturnValue { get; private set; }
        public FuzzyType[] Args { get; private set; }

        private FuzzyMethodBinding(String name, int argCount)
        {
            Name = name;
            Alternatives = null;

            Target = FuzzyType.Unknown();
            ReturnValue = FuzzyType.Unknown();

            var args = new List<FuzzyType>();
            for (var i = 0; i < argCount; ++i) args.Add(FuzzyType.Unknown());
            Args = args.ToArray();

            // yeah, this is enough to keep the all this complex binding, all its alternatives
            // and public type points synced. typelinks really work magic.
            //
            // in short, if we've just found out crisp basis type of the target, 
            // it's high time to fill in the alternatives for this binding
            // 
            // that's the one and only time we need to do anything upon target/arg/retval changes:
            // GetMethods will link all target-related stuff to all yielded alternatives
            // and they will be updated automatically

            Target.TypeHasBecomeCrisp += Target_TypeHasBecomeCrisp;

            // SomeMoreStuffInferred handlers for children just escalate the event
            // no need to process it since everything necessary is already done by links
            //
            // upd. currently noone cares about this event from this very class, 
            // but imo it's still important to comply to FuzzyMetadata contract

            Target.SomeMoreStuffInferred += (o, e) => FireSomeMoreStuffInferred();
            ReturnValue.SomeMoreStuffInferred += (o, e) => FireSomeMoreStuffInferred();
            Args.ForEach(arg => arg.SomeMoreStuffInferred += (o, e) => FireSomeMoreStuffInferred());

            // Register typepoints for debug -> 0 downtime in release, but immensely useful in debug mode
            Target.RegDebuggableParent(this).SetDesc("tar");
            ReturnValue.RegDebuggableParent(this).SetDesc("retval");
            Args.ForEach((arg, i) => arg.RegDebuggableParent(this).SetDesc("arg"+i));
        }

        protected override String FuzzyTypeCode { get { return "Mb"; } }
        protected override String ContentToString() { return Name + "[" + (Alternatives == null ? "?" : Alternatives.Count.ToString()) + "]"; }

        public static FuzzyMethodBinding Unknown(String name, int argCount)
        {
            return new FuzzyMethodBinding(name, argCount);
        }

        private void Target_TypeHasBecomeCrisp(object sender, EventArgs args)
        {
            var alts = Target.GetMethods(Name);

            // fuck varargs for now
            alts = alts.Where(mi => mi.Args.Length == Args.Count());

            alts.ForEach(alt => alt.Host = Host);
            alts.ForEach(alt => alt.ContradictionReported += (o, e) =>
            {
                Alternatives.Remove(alt);
                Engine.Dispose(alt);
            });

            Alternatives = new TrackableList<FuzzyMethod>(alts);
            Alternatives.ListChanged += (o, e) => FireAlternativesGotChanged();

            foreach (var alt in alts)
            {
                // atm unnecessary since alt.Target == Target for all alts
//                this.BindToOneOfAlternatives(Target, alt.Target);

                this.BindToOneOfAlternatives(ReturnValue, alt.ReturnValue);

                // fuck varargs for now
                for (var i = 0; i < alt.Args.Length; i++)
                {
                    var bindingArg = Args[i];
                    var altArg = alt.Args[i];
                    this.BindToOneOfAlternatives(bindingArg, altArg);
                }
            }
        }

        private void FireAlternativesGotChanged()
        {
            if (Alternatives != null)
            {
                if (Alternatives.Count == 0)
                {
                    ReportContradiction(String.Format(
                        "No available alternatives for method '{0}'",
                        this));
                }
                else if (Alternatives.Count == 1)
                {
                    Engine.EngageTheEngine();
                }
                else
                {
                    // todo. concept of unifying typepoints of alternatives
                    // suppose that we have only three alts that feature the following return types:
                    // 1) IEnumerable<int>, no constraints
                    // 2) IEnumerable<T1>, no constraints
                    // 3) IEnumerable<T2>, no constraints
                    // then we should assume that return type of the entire binding is IEnumerable<T1>
                    // (or IEnumerable<T2> - it's irrelevant).

//                    throw new NotImplementedException();
                }
            }

            FireSomeMoreStuffInferred();
        }
    }
}