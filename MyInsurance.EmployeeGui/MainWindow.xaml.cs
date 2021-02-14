using MyInsurance.BusinessLogic.Constants;
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
            CommonConstants.OPENED_WINDOWS.Add(this);
            ((Employee)this.Resources["loggedEmployee"]).ChangeData(CommonConstants.LOGGED_EMPLOYEE);
            //MessageBox.Show(((Employee)this.Resources["loggedEmployee"]).IsAdmin.ToString());
            //todo
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!mcControl.IsExiting)
            {
                CommonConstants.OPENED_WINDOWS.Remove(this);
                foreach (Window window in CommonConstants.OPENED_WINDOWS)
                {
                    if (window == CommonConstants.LOGIN_WINDOW)
                    {
                        if (!window.IsVisible)
                            window.Show();
                    }
                }
            }
        }
    }
}
