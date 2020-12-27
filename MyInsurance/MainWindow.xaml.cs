using System.Windows;
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
