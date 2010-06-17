namespace Relinq.Helpers
{
    public static class BoolHelper
    {
        public static bool? Accumulate(this bool? b1, bool? b2)
        {
            if (b2.HasValue)
            {
                return b2.Value ? b1 : false;
            }
            else
            {
                return b1 == false ? false : b2;
            }
        }
    }
}