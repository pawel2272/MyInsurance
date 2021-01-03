using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Constants
{
    public static class CryptoConstants
    {
        public static readonly Dictionary<string, string> ENCRYPTION_KEYS = new Dictionary<string, string>
        {
            { "user", "projekt2k21crypto" },
            { "message", "projekt2k21cryptomess" },
            { "customer", "projekt2k21cryptocust" }
        };
    }
}
