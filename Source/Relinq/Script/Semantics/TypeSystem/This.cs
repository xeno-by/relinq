namespace Relinq.Script.Semantics.TypeSystem
{
    public class This : RelinqScriptType
    {
        public override string ToString()
        {
            return "<this>";
        }

        public override bool Equals(object obj)
        {
            return obj is This;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}