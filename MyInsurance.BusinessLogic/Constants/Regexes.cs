using System;
using System.Text.RegularExpressions;

namespace MyInsurance.BusinessLogic.Constants
{
    public static class Regexes
    {
        public static readonly Regex NAME_REGEX = new Regex(@"^[A-ZŻŹĆĄŚĘŁÓŃ]'?[a-zżźćńółęąśA-ZŻŹĆĄŚĘŁÓŃ]+(-[a-zżźćńółęąśA-ZŻŹĆĄŚĘŁÓŃ]+)?$");
        public static readonly Regex EMAIL_REGEX = new Regex(@"[a-z0-9_.-]+@[a-z0-9_.-]+\.\w{2,4}");
        public static readonly Regex ZIPCODE_REGEX = new Regex(@"[0-9]{2}-[0-9]{3}");
        public static readonly Regex PHONENUMBER_REGEX = new Regex(@"^(?:\(?\?)?(?:[-\.\(\)\s]*(\d)){9}\)?$");
        public static readonly Regex PESEL_REGEX = new Regex(@"^\d{11}$");
        public static readonly Regex NIP_REGEX = new Regex(@"^\d{10}$");
        public static readonly Regex CITY_REGEX = new Regex(@"[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]+[\s]?[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]*");
    }
}
