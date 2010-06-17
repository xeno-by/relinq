using System;

namespace Relinq.Helpers.Collections
{
    [Flags]
    public enum ItemListChangeAction
    {
        Add = 1,
        Remove = 2,
        Replace = 3
    }
}