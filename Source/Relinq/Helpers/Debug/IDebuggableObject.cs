using System;
using System.Collections.Generic;
using System.IO;

namespace Relinq.Helpers.Debug
{
#if DEBUG
    public interface IDebuggableObject
    {
        int Id { get; }
        String Desc { get; }
        IEnumerable<IDebuggableObject> DebuggableParents { get; }
        IEnumerable<IDebuggableObject> DebuggableChildren { get; }

        void Dump(StreamWriter sw);
        void DisposeDebuggableObject();
    }
#endif
}
