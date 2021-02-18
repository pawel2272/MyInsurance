using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.CustomerGui.Controls.Management.Enums;
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

namespace MyInsurance.CustomerGui.Controls.Edit
{
    /// <summary>
    /// Interaction logic for PolicyEditControl.xaml
    /// </summary>
    public partial class PolicyEditControl : UserControl
    {
        public Brush ButtonsForeground
        {
            get { return (Brush)GetValue(ButtonsForegroundProperty); }
            set { SetValue(ButtonsForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(PolicyEditControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as PolicyEditControl;
                var value = e.NewValue as Brush;
                source.btnOK.Foreground = value;
            })));

        public Brush ButtonsBackground
        {
            get { return (Brush)GetValue(ButtonsBackgroundProperty); }
            set { SetValue(ButtonsBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsBackgroundProperty =
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(PolicyEditControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as PolicyEditControl;
                var value = e.NewValue as Brush;
                source.btnOK.Background = value;
            })));

        public CrudMode Mode
        {
            get { return (CrudMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(CrudMode), typeof(PolicyEditControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as PolicyEditControl;
                var value = (CrudMode)e.NewValue;
            })));

        public RoutedEventHandler BtnOKClick
        {
            get { return (RoutedEventHandler)GetValue(BtnOKClickProperty); }
            set { SetValue(BtnOKClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnOKClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnOKClickProperty =
            DependencyProperty.Register("BtnOKClick", typeof(RoutedEventHandler), typeof(PolicyEditControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as PolicyEditControl;
                var value = e.NewValue as RoutedEventHandler;
                source.btnOK.Click += value;
            })));

        public PolicyEditControl()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue))
            {
                Policy policy;
                using (EmployeeService service = new EmployeeService(Database.DBCONTEXT))
                {
                    List<Employee> employees = service.GetAllEmployees();
                    cbEmployee.ItemsSource = employees;
                    if (this.Mode == CrudMode.Edit)
                    {
                        if (this.DataContext != null)
                        {
                            policy = this.DataContext as Policy;
                            cbEmployee.SelectedItem = employees.FirstOrDefault(emp => emp.Id == policy.EmployeeId);
                        }
                    }

                    if (this.Mode == CrudMode.New)
                    {
                        if (this.DataContext != null)
                        {
                            policy = this.DataContext as Policy;
                            cbEmployee.SelectedItem = ((List<Employee>)cbEmployee.ItemsSource).FirstOrDefault(emp => emp.Id == CommonConstants.LOGGED_EMPLOYEE.Id);
                        }
                    }
                }

                using (CustomerService service = new CustomerService(Database.DBCONTEXT))
                {
                    List<Customer> customers = service.GetAllCustomers();
                    cbCustomer.ItemsSource = customers;
                    if (this.Mode == CrudMode.Edit)
                    {
                        if (this.DataContext != null)
                        {
                            policy = this.DataContext as Policy;
                            cbCustomer.SelectedItem = customers.FirstOrDefault(cust => cust.Id == policy.CustomerId);
                        }
                    }

                    if (this.Mode == CrudMode.New)
                    {
                        if (this.DataContext != null)
                        {
                            policy = this.DataContext as Policy;
                            cbCustomer.SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext != null)
            {
                Policy policy = this.DataContext as Policy;
                policy.Employee = this.cbEmployee.SelectedItem as Employee;
                policy.EmployeeId = (this.cbEmployee.SelectedItem as Employee).Id;
            }
        }

        private void cbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext != null)
            {
                Policy policy = this.DataContext as Policy;
                policy.Customer = this.cbCustomer.SelectedItem as Customer;
                policy.CustomerId = (this.cbCustomer.SelectedItem as Customer).Id;
            }
        }
    }
}
