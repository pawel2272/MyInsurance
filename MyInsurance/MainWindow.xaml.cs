using System.Windows;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.BusinessLogic.Constants;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace MyInsurance
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void testBtn_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => Login()));
        }

        private void Login()
        {
            using (EmployeeService service = new EmployeeService())
            {
                using (LoginService<Employee, EmployeeService> loginService = new LoginService<Employee, EmployeeService>(service, new CryptoService(CryptoConstants.USER_KEY)))
                {
                    if (loginService.Login("admin", "admin"))
                    {
                        //tbResult.Text = loginService.GetLoggedPerson.Login;
                    }
                }
            }
        }


    }
}
