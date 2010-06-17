using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;

namespace Relinq.Script.Semantics.Literals.Metadata
{
    public class DefaultSerializationMetadata : ITypeSerializationMetadata
    {
        private readonly Type _type;
        private readonly bool _isList;
        private readonly bool _isDictionary;
        private readonly bool _isPropertyBag;
        private readonly IEnumerable<String> _properties;

        public DefaultSerializationMetadata(Type type)
        {
            _type = type;

            if (_type.IsAnonymous())
            {
                _isList = false;
                _isDictionary = false;

                _properties =
                    from prop in type.GetProperties(BF.PublicInstance)
                    select prop.Name;
                _isPropertyBag = _properties.Any();
            }
            else
            {
                _isList = type.IsListType();
                _isDictionary = type.IsDictionaryType();

                _properties =
                    from prop in type.GetProperties(BF.Instance)
                    where prop.HasAttr<JsonPropertyAttribute>()
                    select prop.Name;
                _isPropertyBag = _properties.Any();
            }
        }

        public Type Type
        {
            get { return _type; }
        }

        public bool IsList
        {
            get { return _isList; }
        }

        public bool IsDictionary
        {
            get { return _isDictionary; }
        }

        public bool IsPropertyBag
        {
            get { return _isPropertyBag; }
        }

        public IEnumerable<String> Properties
        {
            get { return _properties; }
        }

        public override string ToString()
        {
            return String.Format(
                "Serialization metadata for type '{0}': " +
                "IsList = {1}, IsDictionary = {2}, IsPropertyBag = {3}, " +
                "Properties = {4}",
                Type.FullName,
                IsList,
                IsDictionary,
                IsPropertyBag,
                Properties.StringJoin());
        }
    }
}