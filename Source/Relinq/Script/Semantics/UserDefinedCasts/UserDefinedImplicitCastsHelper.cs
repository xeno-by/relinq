using System;

namespace Relinq.Script.Semantics.UserDefinedCasts
{
    public static class UserDefinedImplicitCastsHelper
    {
        public static bool IsCastImpl(String wannabeImpl)
        {
            return wannabeImpl == "op_Implicit";
        }
    }
}
