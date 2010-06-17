namespace Relinq.Script.Semantics.TypeSystem
{
    public class Null : RelinqScriptType
    {
        public override string ToString()
        {
            return "<null>";
        }

        public override bool Equals(object obj)
        {
            return obj is Null;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}