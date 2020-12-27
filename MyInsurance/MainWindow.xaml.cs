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
using MyInsurance.BusinessLogic.Services;
using MyInsurance.BusinessLogic.Services.Dto;

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

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeService service = new EmployeeService();
            using (LoginService<EmployeeDto, EmployeeService> loginService = new LoginService<EmployeeDto, EmployeeService>(service, new CryptoService()))
            {
                tbResult.Text = loginService.Login("admin", "admin").ToString();
            }
        }
    }
}
