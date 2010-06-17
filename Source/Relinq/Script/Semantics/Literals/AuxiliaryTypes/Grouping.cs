using System;

namespace Relinq.Script.Semantics.Literals.AuxiliaryTypes
{
    public class Grouping
    {
        public static Grouping CreateSerializationStub(object igrouping)
        {
            if (igrouping == null)
            {
                return null;
            }
            else
            {
                var groupingIface = igrouping.GetType().GetInterface("IGrouping`2");
                if (groupingIface != null)
                {
                    var groupingStubType = typeof(GroupingImpl<,>).MakeGenericType(
                        groupingIface.GetGenericArguments()[0],
                        groupingIface.GetGenericArguments()[1]);
                    var groupingKey = groupingStubType.GetProperty("Key");
                    var groupingCollection = groupingStubType.GetProperty("Collection");

                    var groupingStub = (Grouping)Activator.CreateInstance(groupingStubType);
                    groupingKey.SetValue(groupingStub, groupingIface.GetProperty("Key").GetValue(igrouping, null), null);
                    groupingCollection.SetValue(groupingStub, igrouping, null);
                    return groupingStub;
                }
                else
                {
                    throw new NotSupportedException(igrouping.GetType().FullName);
                }
            }
        }
    }
}