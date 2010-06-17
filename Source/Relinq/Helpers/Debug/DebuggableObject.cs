using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Relinq.Helpers.Debug
{
#if DEBUG
    public abstract class DebuggableObject : IDebuggableObject
    {
        public int Id { get; private set; }
        public String Desc { get; private set; }
        public IEnumerable<IDebuggableObject> DebuggableParents { get; protected set; }
        public IEnumerable<IDebuggableObject> DebuggableChildren { get { return this.GetDebuggableChildren(); } }

        protected DebuggableObject()
        {
            Id = this.RegisterForDebug();
            DebuggableParents = Enumerable.Empty<IDebuggableObject>();
        }

        public DebuggableObject SetDesc(String desc)
        {
            Desc = desc;
            return this;
        }

        public DebuggableObject RegDebuggableParent(IDebuggableObject wannabeParent)
        {
            if (!DebuggableParents.Any(parent => parent == wannabeParent))
            {
                var list = new List<IDebuggableObject>();
                list.AddRange(DebuggableParents);
                list.Add(wannabeParent);
                DebuggableParents = list.AsReadOnly();
            }

            return this;
        }

        public virtual void Dump(StreamWriter sw)
        {
            sw.WriteLine(ToString());
        }

        public void DisposeDebuggableObject()
        {
            this.UnregisterFromDebug();
            DebuggableParents = Enumerable.Empty<IDebuggableObject>();
        }
    }
#else
    public abstract class DebuggableObject
    {
        public int Id { get; protected set; }

        protected DebuggableObject()
        {
            Id = this.RegisterForDebug();
        }

        protected void RegDebuggableParent(IDebuggableObject parent)
        {
        }

        protected virtual void Dump(StreamWriter sw)
        {
            sw.WriteLine(ToString());
        }

        public void DisposeDebuggableObject()
        {
        }
    }
#endif
}