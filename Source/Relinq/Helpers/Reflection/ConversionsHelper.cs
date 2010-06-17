using System;
using System.ComponentModel;

namespace Relinq.Helpers.Reflection
{
    public static class ConversionsHelper
    {
        public static bool SupportsSerializationToString<T>()
        {
            return SupportsSerializationToString(typeof(T));
        }

        public static bool SupportsSerializationToString(this Type t)
        {
            var converter = TypeDescriptor.GetConverter(t);
            return converter.CanConvertTo(typeof(String)) && converter.CanConvertFrom(typeof(String));
        }

        public static T FromInvariantString<T>(String s)
        {
            return (T)FromInvariantString(typeof(T), s);
        }

        public static Object FromInvariantString(this Type t, String s)
        {
            if (s == null)
            {
                return null;
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(t);
                if (converter.CanConvertFrom(typeof(String)))
                {
                    return converter.ConvertFromInvariantString(s);
                }
                else
                {
                    throw new NotSupportedException(t.ToString());
                }
            }
        }

        public static String ToInvariantString(this Object @object)
        {
            if (@object == null)
            {
                return null;
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(@object);
                if (converter.CanConvertTo(typeof(String)))
                {
                    return converter.ConvertToInvariantString(@object);
                }
                else
                {
                    return @object.ToString();
                }
            }
        }
    }
}