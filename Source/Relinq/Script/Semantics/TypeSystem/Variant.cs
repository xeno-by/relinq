namespace Relinq.Script.Semantics.TypeSystem
{
    public class Variant : RelinqScriptType
    {
        public override string ToString()
        {
            return "<variant>";
        }

        public override bool Equals(object obj)
        {
            return obj is Variant;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}