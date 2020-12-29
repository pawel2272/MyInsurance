using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Interfaces;
using MyInsurance.BusinessLogic.Services;
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

namespace MyInsurance.Controls.Start
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public Brush ButtonsForeground
        {
            get { return (Brush)GetValue(ButtonsForegroundProperty); }
            set { SetValue(ButtonsForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(LoginControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as LoginControl;
                var value = e.NewValue as Brush;
                source.btnLogin.Foreground = value;
                source.btnRegister.Foreground = value;
            })));

        public Brush ButtonsBackground
        {
            get { return (Brush)GetValue(ButtonsBackgroundProperty); }
            set { SetValue(ButtonsBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsBackgroundProperty =
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(LoginControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as LoginControl;
                var value = e.NewValue as Brush;
                source.btnLogin.Background = value;
                source.btnRegister.Background = value;
            })));

        public LoginControl()
        {
            InitializeComponent();
        }

        private void rbChoose_Click(object sender, RoutedEventArgs e)
        {
            tbLogin.IsEnabled = true;
            pbPassword.IsEnabled = true;
            btnLogin.IsEnabled = true;
            if (sender == rbCustomer)
                btnRegister.IsEnabled = true;
            else
                btnRegister.IsEnabled = false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if ((bool) rbEmployee.IsChecked)
                Dispatcher.Invoke(new Action(() => EmployeeLogin()));

            if ((bool) rbCustomer.IsChecked)
                Dispatcher.Invoke(new Action(() => CustomerLogin()));
        }

        private void EmployeeLogin()
        {
            using (EmployeeService service = new EmployeeService())
            {
                using (LoginService<Employee, EmployeeService> loginService = new LoginService<Employee, EmployeeService>(service, new CryptoService(CryptoConstants.ENCRYPTION_KEYS["user"])))
                {
                    if (loginService.Login(tbLogin.Text, pbPassword.Password))
                    {
                        MessageBox.Show("Zalogowano");
                    }
                }
            }
        }

        private void CustomerLogin()
        {
            using (CustomerService service = new CustomerService())
            {
                using (LoginService<Customer, CustomerService> loginService = new LoginService<Customer, CustomerService>(service, new CryptoService(CryptoConstants.ENCRYPTION_KEYS["customer"])))
                {
                    if (loginService.Login(tbLogin.Text, pbPassword.Password))
                    {
                        MessageBox.Show("Zalogowano");
                    }
                }
            }
        }
    }
}
