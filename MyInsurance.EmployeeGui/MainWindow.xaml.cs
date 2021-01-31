using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyInsurance.EmployeeGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Employee employee, List<Window> openedWindows, Window loginWindow)
        {
            InitializeComponent();
            App.loggedPerson = employee;
            App.loginWindow = loginWindow;
            App.openedWindows = openedWindows;
            openedWindows.Add(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!mcControl.IsExiting)
            {
                App.openedWindows.Remove(this);
                foreach (Window window in App.openedWindows)
                {
                    if (window == App.loginWindow)
                    {
                        if (!window.IsVisible)
                            window.Show();
                    }
                }
            }
        }
    }
}
