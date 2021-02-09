using MyInsurance.BusinessLogic.Data;
using System.Collections.Generic;
using System.Windows;

namespace MyInsurance.BusinessLogic.Constants
{
    public static class CommonConstants
    {
        public static Employee LOGGED_EMPLOYEE;
        public static Customer LOGGED_CUSTOMER;
        public static Window LOGIN_WINDOW;
        public static readonly List<Window> OPENED_WINDOWS = new List<Window>();
    }
}
