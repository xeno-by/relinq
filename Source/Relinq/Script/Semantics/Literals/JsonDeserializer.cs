using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Relinq.Exceptions.JsonSerializer;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Script.Semantics.Literals.AuxiliaryTypes;
using Relinq.Script.Semantics.Literals.Metadata;

namespace Relinq.Script.Semantics.Literals
{
    // http://code.google.com/p/relinq/wiki/JsonSerialization
    public class JsonDeserializer
    {
        public String Root { get; private set; }
        public Type ExpectedType { get; private set; }

        public JsonDeserializer(string root, Type expectedType)
        {
            Root = root;
            ExpectedType = expectedType;
        }

        public static Object Deserialize(String root, Type expectedType)
        {
            return new JsonDeserializer(root, expectedType).Deserialize();
        }

        public object Deserialize()
        {
            return DeserializeImpl(Root, ExpectedType);
        }

        private object DeserializeImpl(String s, Type expectedType)
        {
            try
            {
                if (expectedType == null)
                {
                    throw new JsonDeserializationException(
                        JsonDeserializationExceptionType.ExpectedTypeNotSpecified,
                        Root, ExpectedType, s, expectedType);
                }

                if (s == "null")
                {
                    return null;
                }
                else
                {
                    if (expectedType.SameMetadataToken(typeof(IEnumerable<>)) ||
                        expectedType.SameMetadataToken(typeof(IOrderedEnumerable<>)) ||
                        expectedType.SameMetadataToken(typeof(IQueryable<>)) ||
                        expectedType.SameMetadataToken(typeof(IOrderedQueryable<>)) ||
                        expectedType.SameMetadataToken(typeof(ICollection<>)) ||
                        expectedType.SameMetadataToken(typeof(IList<>)) ||
                        expectedType.IsArray)
                    {
                        var wrapperType = typeof(List<>).MakeGenericType(expectedType.GetEnumerableElement());
                        var instance = DeserializeImpl(s, wrapperType);

                        if (expectedType.SameMetadataToken(typeof(IEnumerable<>)) ||
                            expectedType.SameMetadataToken(typeof(ICollection<>)) ||
                            expectedType.SameMetadataToken(typeof(IList<>)))
                        {
                            return instance;
                        }
                        else
                        {
                            MethodInfo conv = null;
                            if (expectedType.SameMetadataToken(typeof(IOrderedEnumerable<>)))
                            {
                                conv = typeof(ListConversions).GetMethod("ToOrderedEnumerable");
                            }
                            else if (expectedType.SameMetadataToken(typeof(IQueryable<>)))
                            {
                                conv = typeof(ListConversions).GetMethod("ToOrderedEnumerable");
                            }
                            else if (expectedType.SameMetadataToken(typeof(IOrderedQueryable<>)))
                            {
                                conv = typeof(ListConversions).GetMethod("ToOrderedEnumerable");
                            }
                            else if (expectedType.IsArray)
                            {
                                conv = typeof(Enumerable).GetMethod("ToArray");
                            }

                            return conv.Invoke(null, instance.AsArray());
                        }
                    }
                    else if (expectedType.SameMetadataToken(typeof(IDictionary<,>)))
                    {
                        var elType = expectedType.GetDictionaryElement().Value;
                        var wrapperType = typeof(Dictionary<,>).MakeGenericType(elType.Key, elType.Value);
                        return DeserializeImpl(s, wrapperType);
                    }
                    else if (expectedType.SameMetadataToken(typeof(IGrouping<,>)))
                    {
                        var keyType = expectedType.GetGenericArguments()[0];
                        var valueType = expectedType.GetGenericArguments()[1];
                        return DeserializeImpl(s, typeof(GroupingImpl<,>).MakeGenericType(keyType, valueType));
                    }
                    else
                    {
                        if (expectedType.IsTOrNullableT<bool>())
                        {
                            return typeof(bool).FromInvariantString(s);
                        }
                        else if (expectedType.IsJSNumericOrNullable())
                        {
                            var numericType = expectedType.UndecorateNullable();
                            return numericType.FromInvariantString(s);
                        }
                        else if (expectedType.IsEnumOrNullable())
                        {
                            var enumType = expectedType.UndecorateNullable();
                            var underlyingInt = DeserializeImpl(s, Enum.GetUnderlyingType(enumType));
                            return enumType.FromInvariantString(underlyingInt.ToInvariantString());
                        }
                        else if (expectedType == typeof(String))
                        {
                            return s.FromJsonString();
                        }
                        else if (expectedType.IsTOrNullableT<char>())
                        {
                            var underlyingString = (String)DeserializeImpl(s, typeof(String));
                            return underlyingString.Single();
                        }
                        else
                        {
                            if (expectedType.SupportsSerializationToString())
                            {
                                return expectedType.FromInvariantString(s.FromJsonString());
                            }
                            else
                            {
                                var metadata = MetadataRepository.Get(expectedType);
                                if (!metadata.IsPropertyBag && !metadata.IsList && !metadata.IsDictionary)
                                {
                                    throw new JsonDeserializationException(
                                        JsonDeserializationExceptionType.InsufficientMetadata,
                                        Root, ExpectedType, s, expectedType);
                                }

                                if (metadata.IsPropertyBag)
                                {
                                    return DeserializeAsPropertyBag(s, expectedType);
                                }
                                else
                                {
                                    return DeserializeAsCollectionType(s, expectedType);
                                }
                            }
                        }
                    }
                }
            }
            catch (JsonDeserializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new JsonDeserializationException(
                    JsonDeserializationExceptionType.Unexpected,
                    Root, ExpectedType, s, expectedType, ex);
            }
        }

        private object DeserializeAsPropertyBag(String s, Type expectedType)
        {
            var listMatch = Regex.Match(s, @"^\{(?<contents>.*)\}$");
            if (listMatch.Success)
            {
                var kvps = new Dictionary<string, string>();

                var items = listMatch.Result("${contents}").DisassembleJsonObject();
                foreach (var item in items)
                {
                    // before modifying the regex, make sure that you understand the key matching part
                    var kvpMatch = Regex.Match(item, @"^(?<key>.*?):(?<value>.*)$");

                    if (kvpMatch.Success)
                    {
                        var key = kvpMatch.Result("${key}").Trim();
                        var value = kvpMatch.Result("${value}").Trim();
                        kvps.Add(key, value);
                    }
                    else
                    {
                        throw new JsonDeserializationException(
                            JsonDeserializationExceptionType.JsonParseError,
                            Root, ExpectedType, s, expectedType);
                    }
                }

                if (expectedType.IsAnonymous())
                {
                    var ctor = expectedType.GetConstructors().Single();
                    if (ctor.GetParameters().Length != kvps.Count)
                    {
                        throw new JsonDeserializationException(
                            JsonDeserializationExceptionType.NoMatchingCtor,
                            Root, ExpectedType, s, expectedType);
                    }
                    else
                    {
                        var realArgs = new List<object>();
                        foreach (var formalArg in ctor.GetParameters())
                        {
                            if (kvps.ContainsKey(formalArg.Name))
                            {
                                realArgs.Add(DeserializeImpl(kvps[formalArg.Name], formalArg.ParameterType));
                            }
                            else
                            {
                                throw new JsonDeserializationException(
                                    JsonDeserializationExceptionType.NoMatchingCtor,
                                    Root, ExpectedType, s, expectedType);
                            }
                        }

                        return ctor.Invoke(realArgs.ToArray());
                    }
                }
                else
                {
                    object @object;
                    if (!kvps.ContainsKey("$"))
                    {
                        @object = Activator.CreateInstance(expectedType);
                    }
                    else
                    {
                        @object = DeserializeAsCollectionType(kvps["$"], expectedType);
                        kvps.Remove("$");
                    }

                    var metadata = MetadataRepository.Get(expectedType);
                    var jsonProps =
                        from prop in expectedType.GetProperties(BF.Instance)
                        where metadata.Properties.Contains(prop.Name)
                        select prop;

                    foreach (var key in kvps.Keys)
                    {
                        var pi = jsonProps.Where(prop => String.Equals(prop.Name, key)).SingleOrDefault();
                        if (pi != null)
                        {
                            pi.SetValue(@object, DeserializeImpl(kvps[key], pi.PropertyType), null);
                        }
                        else
                        {
                            throw new JsonDeserializationException(
                                JsonDeserializationExceptionType.NoMatchingProperty,
                                Root, ExpectedType, s, expectedType);
                        }
                    }

                    return @object;
                }
            }
            else
            {
                throw new JsonDeserializationException(
                    JsonDeserializationExceptionType.JsonParseError,
                    Root, ExpectedType, s, expectedType);
            }
        }

        private object DeserializeAsCollectionType(String s, Type expectedType)
        {
            var listMatch = Regex.Match(s, @"^\[(?<contents>.*)\]$");
            if (listMatch.Success)
            {
                var collection = Activator.CreateInstance(expectedType);

                var metadata = MetadataRepository.Get(expectedType);
                if (!metadata.IsDictionary)
                {
                    var elType = expectedType.GetListElement();
                    var add = expectedType.GetMethod("Add", elType.AsArray());

                    var items = listMatch.Result("${contents}").DisassembleJsonObject();
                    foreach (var sItem in items)
                    {
                        var item = DeserializeImpl(sItem, elType);
                        add.Invoke(collection, item.AsArray());
                    }
                }
                else
                {
                    var elType = expectedType.GetDictionaryElement().Value;
                    var add = expectedType.GetMethod("Add", new[] { elType.Key, elType.Value });

                    var items = listMatch.Result("${contents}").DisassembleJsonObject();
                    foreach (var sItem in items)
                    {
                        var kvpMatch = Regex.Match(sItem, @"^\[(?<contents>.*)\]$");
                        if (kvpMatch.Success)
                        {
                            var kvp = kvpMatch.Result("${contents}").DisassembleJsonObject();
                            if (kvp.Count() == 2)
                            {
                                var key = DeserializeImpl(kvp.ElementAt(0), elType.Key);
                                var value = DeserializeImpl(kvp.ElementAt(1), elType.Value);
                                add.Invoke(collection, new[] { key, value });
                            }
                            else
                            {
                                throw new JsonDeserializationException(
                                    JsonDeserializationExceptionType.JsonParseError,
                                    Root, ExpectedType, s, expectedType);
                            }
                        }
                        else
                        {
                            throw new JsonDeserializationException(
                                JsonDeserializationExceptionType.JsonParseError,
                                Root, ExpectedType, s, expectedType);
                        }
                    }
                }

                return collection;
            }
            else
            {
                throw new JsonDeserializationException(
                    JsonDeserializationExceptionType.JsonParseError,
                    Root, ExpectedType, s, expectedType);
            }
        }
    }
}