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

namespace MyInsurance.CustomerGui
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
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CommonConstants.LOGGED_CUSTOMER = null;
            CommonConstants.OPENED_WINDOWS.Remove(this);
            foreach (Window window in CommonConstants.OPENED_WINDOWS)
            {
                if (window == CommonConstants.LOGIN_WINDOW)
                    window.Show();
            }
        }
    }
}
