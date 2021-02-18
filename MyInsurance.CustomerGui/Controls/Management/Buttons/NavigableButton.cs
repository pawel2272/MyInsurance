using MyInsurance.CustomerGui.Controls.Management.Enums;
using MyInsurance.CustomerGui.Controls.Management.Interfaces;
using System.Windows.Controls;

namespace MyInsurance.CustomerGui.Controls.Management.Buttons
{
    public class NavigableButton : Button, INavigable
    {
        public NavigationMode WindowMode { get; set; }
    }
}
