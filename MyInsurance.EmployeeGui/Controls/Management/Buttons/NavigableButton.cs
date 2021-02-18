using MyInsurance.EmployeeGui.Controls.Management.Enums;
using MyInsurance.EmployeeGui.Controls.Management.Interfaces;
using System.Windows.Controls;

namespace MyInsurance.EmployeeGui.Controls.Management.Buttons
{
    public class NavigableButton : Button, INavigable
    {
        public NavigationMode WindowMode { get; set; }
    }
}
