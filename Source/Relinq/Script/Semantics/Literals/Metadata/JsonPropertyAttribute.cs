using System;

namespace Relinq.Script.Semantics.Literals.Metadata
{

#warning I would remove this attribute at all

    // actually, the result JSON bean may be represented as a runtime generated object implementing some "data" interfaces
    // it's a hard task to resolve attributes on interface properties and requires addition business rules
    // I don't like to have a new BRs on simple tasks :)

    // I prefer just one rule:
    // (RULE) if an entity has a public property - the framework assumes it as a JSON property
    // (COMMENT) if you want to hide something - convert it to method(s) or decrease its visibility
    // in extreme cases we may introduce a kind of RelinqIgnorableAttribute, but it sounds like a way to nowhere :)

    [AttributeUsage(AttributeTargets.Property)]
    public class JsonPropertyAttribute : Attribute
    {
    }
}