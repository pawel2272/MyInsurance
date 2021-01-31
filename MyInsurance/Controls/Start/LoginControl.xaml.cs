using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        public RegisterControl RegisterControl
        {
            get { return (RegisterControl)GetValue(RegisterControlProperty); }
            set { SetValue(RegisterControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegisterControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegisterControlProperty =
            DependencyProperty.Register("RegisterControl", typeof(RegisterControl), typeof(LoginControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as LoginControl;
                var value = e.NewValue as RegisterControl;
                source.RegisterControl = value;
            })));

        public LoginControl()
        {
            InitializeComponent();
        }

        private void rbChoose_Click(object sender, RoutedEventArgs e)
        {
            grdLoginData.IsEnabled = true;
        }

        public void LogIn()
        {
            this.btnLogin_Click(new object(), new RoutedEventArgs());
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text.Length > 0 && pbPassword.Password.Length > 0)
            {
                if ((bool)rbEmployee.IsChecked)
                    this.EmployeeLogin();

                if ((bool)rbCustomer.IsChecked)
                    this.CustomerLogin();
            }
            else
            {
                MessageBox.Show("Uzupełnij wszystkie pola!", "Brak danych.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void HideParentWindow()
        {
            foreach (Window window in App.openedWindows)
            {
                if (window is MainWindow)
                    window.Hide();
            }
        }

        private void ClearLoginAndPasswordTb()
        {
            this.tbLogin.Text = String.Empty;
            this.pbPassword.Password = String.Empty;
        }

        private void EmployeeLogin()
        {
            using (EmployeeService service = new EmployeeService())
            using (LoginService<Employee, EmployeeService> loginService = new LoginService<Employee, EmployeeService>(service, new CryptoService(CryptoConstants.USER_KEY)))
            {
                if (!loginService.Login(tbLogin.Text, pbPassword.Password))
                {
                    MessageBox.Show("Nieprawidłowy login lub/i hasło.", "Nieprawidłowe dane.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    new EmployeeGui.MainWindow(loginService.GetLoggedPerson, App.openedWindows, App.loginWindow).Show();
                    this.HideParentWindow();
                }
                this.ClearLoginAndPasswordTb();
            }
        }

        private void CustomerLogin()
        {
            using (CustomerService service = new CustomerService())
            {
                using (LoginService<Customer, CustomerService> loginService = new LoginService<Customer, CustomerService>(service, new CryptoService(CryptoConstants.CUSTOMER_KEY)))
                {
                    if (!loginService.Login(tbLogin.Text, pbPassword.Password))
                    {
                        MessageBox.Show("Nieprawidłowy login lub/i hasło.", "Nieprawidłowe dane.", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        new CustomerGui.MainWindow(loginService.GetLoggedPerson, App.openedWindows, App.loginWindow).Show();
                        this.HideParentWindow();
                    }
                    this.ClearLoginAndPasswordTb();
                }
            }

        }

        private void HideControl()
        {
            this.Visibility = Visibility.Hidden;
            this.RegisterControl.Visibility = Visibility.Visible;
        }

        private void SetRegistrationType()
        {
            if ((bool)this.rbCustomer.IsChecked)
            {
                this.RegisterControl.CustomerRegister = false;
                this.RegisterControl.CustomerRegister = true;
            }
            if ((bool)this.rbEmployee.IsChecked)
            {
                this.RegisterControl.CustomerRegister = true;
                this.RegisterControl.CustomerRegister = false;
            }
        }

        private void SendDataToRegistrationControl()
        {
            this.RegisterControl.NewUserLogin = tbLogin.Text;
            this.RegisterControl.NewUserPassword = pbPassword.Password;
        }

        private bool CheckIfUserExists()
        {
            if ((bool)this.rbCustomer.IsChecked)
            {
                using (CustomerService customerService = new CustomerService())
                {
                    return customerService.CheckIfExists(tbLogin.Text);
                }
            }
            if ((bool)this.rbEmployee.IsChecked)
            {
                using (EmployeeService employeeService = new EmployeeService())
                {
                    return employeeService.CheckIfExists(tbLogin.Text);
                }
            }
            return false;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text.Length > 0 && pbPassword.Password.Length > 8)
            {
                if (!CheckIfUserExists())
                {
                    this.HideControl();
                    this.SetRegistrationType();
                    this.SendDataToRegistrationControl();
                    grdLoginData.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Użytkownik o podanej nazwie już istnieje! Proszę podać inną nazwę.", "Użytkownik już istnieje.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Uzupełnij login i hasło! Hasło nie może być krótsze niż 8 znaków.", "Uzupełnij dane.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        if (e.Key == Key.Enter)
                        {
                            this.btnLogin_Click(sender, e);
                        }
                        break;
                }
            }
        }
    }
}
