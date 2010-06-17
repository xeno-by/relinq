using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Relinq.Exceptions.JsonSerializer;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Script.Semantics.Literals.Metadata;
using Relinq.Script.Semantics.Literals.AuxiliaryTypes;

namespace Relinq.Script.Semantics.Literals
{
    // http://code.google.com/p/relinq/wiki/JsonSerialization
    public class JsonSerializer
    {
        public Object Root { get; private set; }
        public Type ExpectedType { get; private set; }

        public JsonSerializer(Object root, Type expectedType)
        {
            Root = root;
            ExpectedType = expectedType;
        }

        public static String Serialize(Object root, Type expectedType)
        {
            return new JsonSerializer(root, expectedType).Serialize();
        }

        public String Serialize()
        {
            return SerializeImpl(Root, ExpectedType);
        }

        private String SerializeImpl(object @object, Type expectedType)
        {
            try
            {
                if (expectedType == null)
                {
                    throw new JsonSerializationException(
                        JsonSerializationExceptionType.ExpectedTypeNotSpecified, 
                        Root, ExpectedType, @object, expectedType);
                }

                if (@object == null)
                {
                    return "null";
                }
                else
                {
                    if (expectedType.SameMetadataToken(typeof(IEnumerable<>)) ||
                        expectedType.SameMetadataToken(typeof(IOrderedEnumerable<>)) ||
                        expectedType.SameMetadataToken(typeof(IQueryable<>)) ||
                        expectedType.SameMetadataToken(typeof(IOrderedQueryable<>)) ||
                        expectedType.SameMetadataToken(typeof(ICollection<>)) ||
                        expectedType.SameMetadataToken(typeof(IList<>)) ||
                    // todo. update the doc
                        expectedType.IsArray)
                    {
                        var wrapperType = typeof(List<>).MakeGenericType(expectedType.GetEnumerableElement());
                        var wrapper = Activator.CreateInstance(wrapperType);
                        wrapperType.GetMethod("AddRange").Invoke(wrapper, @object.AsArray());
                        return SerializeImpl(wrapper, wrapperType);
                    }
                    else if (expectedType.SameMetadataToken(typeof(IDictionary<,>)))
                    {
                        var elType = expectedType.GetDictionaryElement().Value;
                        var wrapperType = typeof(Dictionary<,>).MakeGenericType(elType.Key, elType.Value);
                        var wrapperIfaceType = typeof(IDictionary<,>).MakeGenericType(elType.Key, elType.Value);
                        var wrapperCtor = wrapperType.GetConstructor(wrapperIfaceType.AsArray());
                        var wrapper = wrapperCtor.Invoke(@object.AsArray());
                        return SerializeImpl(wrapper, wrapperType);
                    }
                    else if (expectedType.SameMetadataToken(typeof(IGrouping<,>)))
                    {
                        var keyType = expectedType.GetGenericArguments()[0];
                        var valueType = expectedType.GetGenericArguments()[1];
                        return SerializeImpl(Grouping.CreateSerializationStub(@object),
                            typeof(GroupingImpl<,>).MakeGenericType(keyType, valueType));
                    }
                    else if (@object.IsNullable() || expectedType.IsNullable())
                    {
                        return SerializeImpl(@object.UndecorateNullable(), expectedType.UndecorateNullable());
                    }
                    else
                    {
                        if (expectedType != @object.GetType())
                        {
                            throw new JsonSerializationException(
                                JsonSerializationExceptionType.ActualVsExpectedTypeMismatch,
                                Root, ExpectedType, @object, expectedType);
                        }

                        if (expectedType.IsTOrNullableT<bool>())
                        {
                            return @object.ToInvariantString().ToLower();
                        }
                        else if (expectedType.IsJSNumericOrNullable())
                        {
                            return @object.ToInvariantString();
                        }
                        else if (expectedType.IsEnumOrNullable())
                        {
                            var underlyingInt = ((IConvertible)@object).ToInt64(CultureInfo.InvariantCulture);
                            return underlyingInt.ToInvariantString();
                        }
                        else if (expectedType == typeof(String))
                        {
                            return @object.ToInvariantString().ToJsonString();
                        }
                        else if (expectedType.IsTOrNullableT<char>())
                        {
                            return @object.ToInvariantString().ToJsonString();
                        }
                        else
                        {
                            if (expectedType.SupportsSerializationToString())
                            {
                                return @object.ToInvariantString().ToJsonString();
                            }
                            else
                            {
                                var metadata = MetadataRepository.Get(expectedType);
                                if (!metadata.IsPropertyBag && !metadata.IsList && !metadata.IsDictionary)
                                {
                                    throw new JsonSerializationException(
                                        JsonSerializationExceptionType.InsufficientMetadata,
                                        Root, ExpectedType, @object, expectedType);
                                }
                                else
                                {
                                    var result = String.Empty;
                                    if (metadata.IsPropertyBag && !metadata.IsList && !metadata.IsDictionary)
                                    {
                                        result += SerializeAsPropertyBag(@object);
                                    }
                                    else if (metadata.IsPropertyBag && metadata.IsList && !metadata.IsDictionary)
                                    {
                                        result += SerializeAsPropertyBag(@object);
                                        result = result.Substring(0, result.Length - 1) + ", $ : ";
                                        result += SerializeAsListType(@object);
                                    }
                                    else if (metadata.IsPropertyBag && metadata.IsDictionary)
                                    {
                                        result += SerializeAsPropertyBag(@object);
                                        result = result.Substring(0, result.Length - 1) + ", $ : ";
                                        result += SerializeAsDictionaryType(@object);
                                    }
                                    else if (!metadata.IsPropertyBag && metadata.IsList && !metadata.IsDictionary)
                                    {
                                        result += SerializeAsListType(@object);
                                    }
                                    else if (!metadata.IsPropertyBag && metadata.IsDictionary)
                                    {
                                        result += SerializeAsDictionaryType(@object);
                                    }

                                    return result;
                                }
                            }
                        }
                    }
                }
            }
            catch (JsonSerializationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException(
                    JsonSerializationExceptionType.Unexpected,
                    Root, ExpectedType, @object, expectedType, ex);
            }
        }

        private String SerializeAsPropertyBag(object @object)
        {
            var builder = new StringBuilder();
            builder.Append("{");

            var metadata = MetadataRepository.Get(@object.GetType());
            var allProperties = @object.GetType().GetProperties(BF.Instance);
            var jsonProperties = allProperties.Where(prop => metadata.Properties.Contains(prop.Name));

            foreach (var jsonProp in jsonProperties)
            {
                var propValue = jsonProp.GetValue(@object, null);
                builder.AppendFormat("{0} : {1}, ", jsonProp.Name, SerializeImpl(propValue, jsonProp.PropertyType));
            }

            if (builder[builder.Length - 1] != '{') builder.Remove(builder.Length - 2, 2);
            return builder.Append("}").ToString();
        }

        private String SerializeAsListType(object @object)
        {
            var builder = new StringBuilder();
            builder.Append("[");

            var elType = @object.GetType().GetListElement();
            foreach (var item in (IEnumerable)@object)
            {
                builder.Append(SerializeImpl(item, elType)).Append(", ");
            }

            if (builder[builder.Length - 1] != '[') builder.Remove(builder.Length - 2, 2);
            return builder.Append("]").ToString();
        }

        private String SerializeAsDictionaryType(object @object)
        {
            var builder = new StringBuilder();
            builder.Append("[");

            var elType = @object.GetType().GetDictionaryElement().Value;
            foreach (var item in (IEnumerable)@object)
            {
                var key = item.GetType().GetProperty("Key").GetValue(item, null);
                var value = item.GetType().GetProperty("Value").GetValue(item, null);
                builder.AppendFormat("[{0}, {1}], ", SerializeImpl(key, elType.Key), SerializeImpl(value, elType.Value));
            }

            if (builder[builder.Length - 1] != '[') builder.Remove(builder.Length - 2, 2);
            return builder.Append("]").ToString();
        }
    }
}