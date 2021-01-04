using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyInsurance.BusinessLogic.Constants
{
    public static class Regexes
    {
        public static Dictionary<string, Regex> PERSON_VALIDATION = new Dictionary<string, Regex>()
        {
            { "name", new Regex("^[A-ZŻŹĆĄŚĘŁÓŃ]'?[a-zżźćńółęąśA-ZŻŹĆĄŚĘŁÓŃ]+(-[a-zżźćńółęąśA-ZŻŹĆĄŚĘŁÓŃ]+)?$")},
            { "email", new Regex("[a-z0-9_.-]+@[a-z0-9_.-]+\\.\\w{2,4}") },
            { "zipcode", new Regex("[0-9]{2}-[0-9]{3}") }
        };
    }
}
