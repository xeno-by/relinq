using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Script.Semantics.Casts;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Semantics.Lookup;
using Relinq.Helpers.Reflection;

namespace Relinq.Script.Semantics.TypeInference
{
    public static class TypeInferenceImplicitCasts
    {
        public static MethodInfo Resolve(this MethodGroup mg, Type src, Type dest)
        {
            Func<Type, Type, bool> encompasses = (t1, t2) => t2.HasPredefinedImplicitCastTo(t1);
            Func<Type, Type, bool> isEncompassedBy = (t1, t2) => t1.HasPredefinedImplicitCastTo(t2);
            Func<IEnumerable<Type>, Type> mostEncompassing = types => {
                var alts = types.Where(type => types.All(type2 => encompasses(type, type2))).Distinct();
                return alts.Count() == 1 ? alts.Single() : null; };
            Func<IEnumerable<Type>, Type> mostEncompassed = types => {
                var alts = types.Where(type => types.All(type2 => isEncompassedBy(type, type2))).Distinct();
                return alts.Count() == 1 ? alts.Single() : null; };
            
            var sx = mg.Alts.Any(alt => src == alt.CastSource()) ? src :
                mostEncompassed(mg.Alts.Select(alt => alt.CastSource()).Distinct());
            var dx = mg.Alts.Any(alt => dest == alt.CastDestination()) ? dest :
                mostEncompassing(mg.Alts.Select(alt => alt.CastDestination()).Distinct());

            if (sx != null && dx != null)
            {
                var mostSpecific = mg.Alts.Where(
                    alt => alt.CastSource() == sx && alt.CastDestination() == dx);

                var mostSpecificNonLifted = mostSpecific.Where(alt => !alt.IsLifted());
                if (mostSpecificNonLifted.Count() == 1)
                {
                    return mostSpecificNonLifted.Single();
                }

                var mostSpecificLifted = mostSpecific.Where(alt => alt.IsLifted());
                if (mostSpecificLifted.Count() == 1)
                {
                    return mostSpecificLifted.Single();
                }
            }

            return null;
        }
    }
}
