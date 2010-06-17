using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Relinq.V2.Helpers;
using Relinq.V2.RelinqScript.Compiler2.Expressions;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.Constraints;
using Relinq.V2.RelinqScript.Reflection.ForgedMethods;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;
using Relinq.V2.RelinqScript.Reflection;
using Relinq.V2.RelinqScript.Reflection.Operators;

namespace Relinq.V2.RelinqScript.Compiler2.TypeInference
{
    // Fuzzy type is a fun thingie.
    // Its structure is drastically differently treated by different parts of the program.
    //
    // From one side, it represents an entire tree of FuzzyTypes since its underlying type
    // might have generic arguments and all of those are important to infer.
    //
    // From the other side, most type inference routines such as IdentityLink.Sync or field/method resolution
    // logics of FieldExpression/CallExpression care mostly about the basis type (top node of the tree).

    public class FuzzyType : FuzzyMetadata
    {
        private Type _runtimeType;
        public event EventHandler TypeHasBecomeCrisp;
        public override bool IsFullyInferred{ get {
            return this.IsCrisp() && GenericArgs.All(arg => arg.IsFullyInferred);
        } }

        public IList<FuzzyType> GenericArgs { get; private set; }

        private IDictionary<String, FuzzyType> FieldsOrProperties { get; set; }
//        public FuzzyType GetFieldOrProperty(String name)
//        public IEnumerable<FuzzyMethod> GetMethods(String name)

        public IEnumerable<TypeConstaint> Constraints { get; private set; }

        protected FuzzyType()
        {
            _runtimeType = null;
            Constraints = Enumerable.Empty<TypeConstaint>();

            GenericArgs = new List<FuzzyType>().AsReadOnly();
            FieldsOrProperties = new Dictionary<String, FuzzyType>();
            this.TypeHasBecomeCrisp += (o, e) => PayOffFieldCredits();
        }

        protected FuzzyType(Type runtimeType)
            : this()
        {
            RuntimeType = runtimeType;
        }

        protected override String FuzzyTypeCode { get { return "T"; } }
        protected override String ContentToString() { return (_runtimeType == null ? "null" : _runtimeType.Name); }

        public override TypeInferenceUnit Host
        {
            set
            {
                base.Host = value;
                GenericArgs.ForEach(arg => arg.Host = Host);
            }
        }

        public static FuzzyType Unknown()
        {
            return new FuzzyType();
        }

        public static FuzzyType FromRuntimeType(Type runtimeType)
        {
            return new FuzzyType(runtimeType);
        }

        private void InitializeOrUpdateConstraints()
        {
            // see DotNetFrameworkTests.TestGenericConstraints2

            // todo. possible refinement of type inference engine
            // we might infer and store type constraints to get even more hints for type inference
        }

        private void InitializeOrUpdateGenericArgs()
        {
            // If runtime type is crisp enough to contain generic arguments, 
            // we should immediately initialize them and be ready for external queries.
            //
            // To provide callability from wherever we can fancy, I've introduced 
            // another branch of logics which is kinda unrelated to args initialization.

            if (_runtimeType != null && _runtimeType.IsGenericType)
            {
                var runtimeArgs = _runtimeType.GetGenericArguments();
                if (GenericArgs.Count == 0)
                {
                    var genericArgs = new List<FuzzyType>();
                    foreach (var genericArg in runtimeArgs)
                        genericArgs.Add(FromRuntimeType(genericArg));

                    GenericArgs = genericArgs.AsReadOnly();
                    GenericArgs.ForEach(arg => arg.Host = Host);
                    GenericArgs.ForEach((arg, i) => arg.RegDebuggableParent(this).SetDesc("g" + i));
                    GenericArgs.ForEach(arg => arg.SomeMoreStuffInferred += genericArg_SomeMoreStuffInferred);
                }
                else
                {
                    if (GenericArgs.Count != runtimeArgs.Length)
                    {
                        throw new ArgumentException("This can't be: " + 
                            "new underlying type has different number of generic arguments.");
                    }
                    else
                    {
                        for (var i = 0; i < runtimeArgs.Length; ++i)
                        {
                            GenericArgs[i].RuntimeType = runtimeArgs[i];
                        }
                    }
                }
            }
        }

        private void genericArg_SomeMoreStuffInferred(object sender, EventArgs e)
        {
            // the line below can't be expressed by links only:
            // in case of FuzzyMethod we can delay underlying method's synchronization,
            // but here the underlying type is just used too often
            //
            // this also implicitly escalates the SomeMoreInferred event

            RuntimeType = RuntimeType.GetGenericTypeDefinition().MakeGenericType(
                GenericArgs.Select(arg => arg.RuntimeType).ToArray());
        }

        public Type RuntimeType
        {
            get { return _runtimeType; }
            set
            {
                if (!_runtimeType.SameType(value))
                {
                    if (FuzzinessLevel < value.GetFuzzinessLevel())
                    {
                        throw new ArgumentException("This can't be: " +
                            "assigned value has bigger fuzziness than original value has");
                    }
                    else
                    {
                        _runtimeType = value;

                        // todo. possible refinement of type inference engine
                        // merge two collections of constraints here
                        //InitializeOrUpdateConstraints();

                        InitializeOrUpdateGenericArgs();
                        FireSomeMoreStuffInferred();
                    }
                }
            }
        }

        public bool SameBasisType(FuzzyType t2)
        {
            return _runtimeType.SameBasisType(t2.RuntimeType);
        }

        public bool SameType(FuzzyType t2)
        {
            return _runtimeType.SameType(t2.RuntimeType);
        }

        public int FuzzinessLevel 
        { 
            get
            {
                return RuntimeType.GetFuzzinessLevel();
            }
        }

        // Let's think about how the [type <-> field] virtual typelink works.
        //
        // If basis type of the type itself !IsInferred(), we can't even get an idea about 
        // what fields the type has, i.e. field will have TypeFuzzinessLevel.Unknown.
        // However, at this point we'll be receiving requests about specific fields
        // for creation of IsFieldOf typelinks. Thus, we'll have to give them on credit.
        //
        // Once type becomes crisp, we should update all emitted fields with less fuzzy types
        // (we also need to check that all emitted fields actually exist in the crisp type, too)
        //
        // upd. Current implementation doesn't update emitted fuzzy types explicitly, because
        // by the time when basis type becomes inferred, the field type might have some fancy info collected
        // Instead, when the types pays off its field credits, it creates new fuzzy types as fields, and
        // establishes IdentityLinks between credit and actual fields.
        //
        // If field's type is crisp at the instant when the declaring type becomes crisp 
        // (tho the latter doesn't guarantee that all generic arguments of the declaring type are crisp)
        // the we don't have any problems.
        //
        // For example, if the declaring type is Dictionary<K, V> then the Count field 
        // is always int no matter what types K and V are.
        //
        // However, if field's type depends on one or several generic arguments of the declaring type,
        // things becomes tricky when it comes to synchronizing the virtual typelink. For example, 
        // if the declaring type is Dictionary<K, V> then the Keys collection depends on the K type. 
        // 
        // If we've just inferred some more stuff about the declaring type, it's no problem
        // to update the field type -> we just use GetField/GetProperty and replace underlying runtime type
        // of the field type with the new value.
        //
        // Tho we face the glitch when the field type is updated, e.g. KeyCollection<K, V> becomes
        // KeyCollection<int, V> becomes somewhere else we use method Add(K item) and its argument is int.
        // Now how we know that K in Dictionary<K, V> should now become int?
        //
        // The trick here is to preserve the relation between the declaring type and the field 
        // not by constructing field's type from FieldInfo/PropertyInfo that .NET reflection yields, 
        // but by using the very same fuzzy generic arguments as generic arguments of field's type
        //
        // upd. Latest implementation doesn't force fuzzy generic arguments of type and field to be 
        // referentially equal. Instead, it just links them with an IdentityLink.

        public FuzzyType GetFieldOrProperty(String name)
        {
            if (!FieldsOrProperties.ContainsKey(name))
            {
                FieldsOrProperties.Add(name, EmitFuzzyField(name));
                FieldsOrProperties[name].RegDebuggableParent(this).SetDesc("f");
            }

            return FieldsOrProperties[name];
        }

        private FuzzyType EmitFuzzyField(String name)
        {
            if (!this.IsCrisp())
            {
                // emit field type as a credit
                // once crisp basis type is found out, we should pay off the credits
                // see TryPayOffFieldCredits method

                return Unknown();
            }
            else
            {
                var mi = RuntimeType.GetFieldOrProperty(name);
                if (mi == null)
                {
                    ReportContradiction(String.Format(
                        "Field or property '{0}' is not found within runtime type '{1}'",
                        name,
                        RuntimeType));

                    return null;
                }
                else
                {
                    var field = FromRuntimeType(mi.GetFieldOrPropertyType());

                    // horrible boilerplate

                    field.Host = Host;
                    field.RegDebuggableParent(this).SetDesc("f");

                    // establishing links between typedef generic args and field's generic args
                    // read more comments above

                    foreach (var pair in mi.GetGenericArgsMapping())
                    {
                        var typeDefArg = this.SelectGenericArg(pair.Value);
                        var fieldArg = field.SelectGenericArg(pair.Key);
                        fieldArg.IsIdenticalTo(typeDefArg);
                    }

                    return field;
                }
            }
        }

        private void PayOffFieldCredits()
        {
            // we've just acquired crisp version of the runtime type!
            foreach (var creditKvp in FieldsOrProperties)
            {
                var creditField = creditKvp.Value;
                var realField = EmitFuzzyField(creditKvp.Key);

                var link = creditField.IsIdenticalTo(realField);
                Engine.ScheduleSync(link, realField);
            }
        }

        public FuzzyType SelectGenericArg(String path)
        {
            var match = Regex.Match(path, @"^(\[(?<index>\d*)\])*$");
            if (match.Success)
            {
                var type = this;

                for (match = Regex.Match(path, @"(\[(?<index>\d*)\])");
                    match.Success;
                    match = match.NextMatch())
                {
                    var index = int.Parse(match.Result("${index}"));
                    type = type.GenericArgs[index];
                }

                return type;
            }
            else
            {
                throw new ArgumentException(String.Format("Invalid path '{0}'", path));
            }
        }

        public IEnumerable<FuzzyMethod> GetMethods(String name)
        {
            if (!this.IsCrisp())
            {
                throw new ArgumentException("Unlike GetField() " +
                    "this method doesn't support emitting methods in credit");
            }
            else
            {
                IEnumerable<IMethodInfo> alts;
                if (name == OperatorHelper.ForgedConversionOp)
                {
                    // todo. strictly saying this doesn't cover all possible conversion cases:
                    // 1) (stupid but possible example) series of conversions (Class2)(Class1)obj
                    // 2) conversion operators can be declared in either of related types

                    return GetMethods(OperatorHelper.ForgedNaturalCoercionOp).Union(
                        GetMethods("op_Implicit")).Union(
                        GetMethods("op_Explicit"));
                }
                else if (name == OperatorHelper.ForgedNaturalCoercionOp)
                {
                    // todo. stuff that is yet to be implemented:
                    // 1) Func<int, int> <-> int delegate(int)
                    // 2) Func<int, int> -> Action<int>
                    // 3) Expression<F> -> G, if F -> G
                    // 3) Expression<F> <- G, if F <- G

                    alts = RuntimeType.ForgeNaturalCoercionMethods();
                }
                else if (OperatorHelper.IsUnaryOpMethodImpl(name))
                {
                    Func<Type, IEnumerable<IMethodInfo>> filterOperatorMethods = type => type
                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .Where(mi => mi.Name == name)
                        .Select(mi => DotNetMethodInfo.FromRuntimeMethod(mi));

                    alts = filterOperatorMethods(RuntimeType).Union(
                        RuntimeType.ForgePrimitiveUnaryOperators(name));
                }
                else if (OperatorHelper.IsBinaryOpMethodImpl(name) || 
                    name == "op_Implicit" || name == "op_Explicit")
                {
                    // todo. strictly saying this stuff doesn't cover all possible cases:
                    // 1) conversion operators can be declared in either of related types

                    Func<Type, IEnumerable<MethodInfo>> filterOperatorMethods = type => type
                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .Where(mi => mi.Name == name);

                    alts = filterOperatorMethods(RuntimeType)
                        .Select(op => op.ForgeFromUserDefinedOperator())
                        .Union(RuntimeType.ForgePrimitiveBinaryOperators(name));
                }
                else
                {
                    // todo. revise visibility modifiers policy for member reflection
                    // public instance methods so far

                    Func<Type, IEnumerable<IMethodInfo>> filterInstanceMethods = type => type
                        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(mi => mi.Name == name)
                        .Select(mi => DotNetMethodInfo.FromRuntimeMethod(mi));

                    // todo. revise visibility modifiers policy for member reflection
                    // public extension methods so far

                    Func<Type, IEnumerable<IMethodInfo>> filterExtensionMethods = type => type
                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .Where(mi => mi.IsDefined(typeof(ExtensionAttribute), false))
                        .Where(mi => mi.Name == name)
                        .Where(mi => mi.GetParameters().Length > 0)
                        .Where(mi => RuntimeType.BasisTypeIsInheritedFrom(mi.GetParameters()[0].ParameterType))
                        .Select(mi => mi.ForgeFromExtensionMethod());

                    alts = filterInstanceMethods(RuntimeType).Union(
                        // bug: i wonder how much more WTFs do I have to introduce
//                        filterExtensionMethods(typeof(Enumerable))).Union(
                        filterExtensionMethods(typeof(Queryable)));
                }

                alts = alts.ToArray();
                return alts.Select(alt => FuzzyMethod.FromRuntimeMethod(this, alt)).ToArray();
            }
        }

        private bool _lastTimeTypeWasntCrisp = true;

        public override void FireSomeMoreStuffInferred()
        {
            if (this.IsCrisp() && _lastTimeTypeWasntCrisp)
            {
                _lastTimeTypeWasntCrisp = false;
                FireTypeHasBecomeCrisp();
            }

            base.FireSomeMoreStuffInferred();
        }

        protected virtual void FireTypeHasBecomeCrisp()
        {
            if (TypeHasBecomeCrisp != null)
                TypeHasBecomeCrisp(this, EventArgs.Empty);
        }
    }

    public enum TypeFuzzinessLevel
    {
        Crisp = 0,
        Parameter = 1,
        Unknown = 2
    }
}