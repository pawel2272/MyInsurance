﻿using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MyInsurance.Controls.Start
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        public Brush ButtonsForeground
        {
            get { return (Brush)GetValue(ButtonsForegroundProperty); }
            set { SetValue(ButtonsForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(RegisterControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as RegisterControl;
                var value = e.NewValue as Brush;
                source.btnRegister.Foreground = value;
                source.btnCancel.Foreground = value;
            })));

        public Brush ButtonsBackground
        {
            get { return (Brush)GetValue(ButtonsBackgroundProperty); }
            set { SetValue(ButtonsBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsBackgroundProperty =
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(RegisterControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as RegisterControl;
                var value = e.NewValue as Brush;
                source.btnRegister.Background = value;
                source.btnCancel.Background = value;
            })));

        public bool CustomerRegister
        {
            get { return (bool)GetValue(CustomerRegisterProperty); }
            set { SetValue(CustomerRegisterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomerRegister.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomerRegisterProperty =
            DependencyProperty.Register("CustomerRegister", typeof(bool), typeof(RegisterControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as RegisterControl;
                var value = (bool) e.NewValue;
                if (value)
                {
                    source.rbCustomer.IsChecked = true;
                    source.rbEmployee.IsChecked = false;
                    source.customerData.Visibility = Visibility.Visible;
                    source.employeeData.Visibility = Visibility.Hidden;
                    source.customerData.DataContext = new Customer();
                }
                if (!value)
                {
                    source.rbCustomer.IsChecked = false;
                    source.rbEmployee.IsChecked = true;
                    source.customerData.Visibility = Visibility.Hidden;
                    source.employeeData.Visibility = Visibility.Visible;
                    source.employeeData.DataContext = new Employee();
                }
            })));

        public LoginControl LoginControl
        {
            get { return (LoginControl)GetValue(LoginControlProperty); }
            set { SetValue(LoginControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegisterControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginControlProperty =
            DependencyProperty.Register("LoginControl", typeof(LoginControl), typeof(RegisterControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as RegisterControl;
                var value = e.NewValue as LoginControl;
                source.LoginControl = value;
            })));

        public string NewUserLogin { get; set; }
        public string NewUserPassword { get; set; }

        public RegisterControl()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            foreach (Control ctl in grdData.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
            }
            this.Visibility = Visibility.Hidden;
            this.LoginControl.Visibility = Visibility.Visible;
            this.LoginControl.tbLogin.Text = String.Empty;
            this.LoginControl.pbPassword.Password = String.Empty;
        }

        private bool CheckIfTextFieldsAreNotEmpty(UIElementCollection children)
        {
            foreach (Control ctl in children)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    if (((TextBox)ctl).Text == String.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfTextFieldsAreNotEmpty(grdRegister.Children))
                return;
            bool isRegistered = false;
            if (this.CustomerRegister)
            {
                using (ICustomerService service = new CustomerService())
                {
                    Customer customer = customerData.DataContext as Customer;
                    customer.Login = NewUserLogin;
                    customer.Password = NewUserPassword;
                    service.Add(customer);
                    isRegistered = true;
                }
            }

            if (!this.CustomerRegister)
            {
                using (IEmployeeService service = new EmployeeService())
                {
                    Employee employee = employeeData.DataContext as Employee;
                    employee.Login = NewUserLogin;
                    employee.Password = NewUserPassword;
                    service.Add(employee);
                    isRegistered = true;
                }
            }

            if (isRegistered)
                MessageBox.Show("Użytkownik zostanie dodany po zatwierdzeniu przez administratora.", "Rejestracja zakończona pomyślnie.", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Rejestracja zakończona niepowodzeniem. Skontaktuj się z administratorem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        btnRegister_Click(sender, e);
                        break;
                    case Key.Escape:
                        btnCancel_Click(sender, e);
                        break;
                }
            }
        }
    }
}
