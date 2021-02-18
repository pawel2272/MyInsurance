using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.CustomerGui.Controls.Management;
using MyInsurance.CustomerGui.Controls.Management.Enums;
using MyInsurance.CustomerGui.Controls.Management.Interfaces;
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
using System.Windows.Shapes;

namespace MyInsurance.CustomerGui.Windows
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly NavigationMode navigationMode;
        private readonly CrudMode crudMode;
        private readonly IHasDataGrid hasDataGrid;
        private readonly object invoker;
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(NavigationMode type, CrudMode crudMode, object hasDataGrid)
        {
            InitializeComponent();
            this.navigationMode = type;
            this.crudMode = crudMode;
            if (hasDataGrid is IHasDataGrid)
            {
                this.hasDataGrid = hasDataGrid as IHasDataGrid;
                this.invoker = hasDataGrid;
            }
            else
            {
                this.invoker = hasDataGrid;
            }
            switch (type)
            {
                case NavigationMode.Customer:
                    this.cecEdit.Visibility = Visibility.Visible;
                    this.cecEdit.Mode = crudMode;
                    this.Height = 500;
                    switch (this.crudMode)
                    {
                        case CrudMode.New:
                            this.cecEdit.DataContext = new Employee();
                            break;
                        case CrudMode.Edit:
                            if (hasDataGrid != null)
                                this.cecEdit.DataContext = new Employee(((IHasDataGrid)hasDataGrid).MainGrid.SelectedItem as Employee);
                            break;
                    }
                    break;
                case NavigationMode.Cases:
                    this.cccEdit.Visibility = Visibility.Visible;
                    this.cccEdit.Mode = crudMode;
                    this.Height = 440;
                    switch (this.crudMode)
                    {
                        case CrudMode.New:
                            this.cccEdit.DataContext = new Case();
                            break;
                        case CrudMode.Edit:
                            this.cccEdit.DataContext = new Case(((IHasDataGrid)hasDataGrid).MainGrid.SelectedItem as Case);
                            break;
                    }
                    break;
                case NavigationMode.Policies:
                    this.pecEdit.Visibility = Visibility.Visible;
                    this.pecEdit.Mode = crudMode;
                    this.Height = 230;
                    switch (this.crudMode)
                    {
                        case CrudMode.New:
                            this.pecEdit.DataContext = new Policy();
                            break;
                        case CrudMode.Edit:
                            this.pecEdit.DataContext = new Policy(((IHasDataGrid)hasDataGrid).MainGrid.SelectedItem as Policy);
                            break;
                    }
                    break;
            }
        }

        private bool AnyFieldsEmpty(Grid grid)
        {
            foreach (Control ctl in grid.Children)
            {
                if (ctl is TextBox)
                {
                    var tb = (TextBox)ctl;
                    if (tb.Equals(string.Empty))
                    {
                        MessageBox.Show("Uzupełnij wszystkie pola.", "Brak danych.", MessageBoxButton.OK, MessageBoxImage.Information);
                        return true;
                    }
                }
                if (ctl is PasswordBox)
                {
                    var pb = (PasswordBox)ctl;
                    if (pb.Password.Length < 8 && pb.Password.Length > 0)
                    {
                        MessageBox.Show("Hasło nie może być krótsze niż 8 znaków.", "Uzupełnij dane.", MessageBoxButton.OK, MessageBoxImage.Information);
                        return true;
                    }
                }
            }
            return false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz wprowadzić zmiany?", "Zmieniono dane", MessageBoxButton.YesNo, MessageBoxImage.Question);
            Grid grid;
            if (result == MessageBoxResult.No)
                return;
            switch (this.navigationMode)
            {
                case NavigationMode.Customer:
                    if (AnyFieldsEmpty(this.cecEdit.grdCustomer))
                        return;
                    Customer customer = this.cecEdit.DataContext as Customer;
                    using (CustomerService service = new CustomerService(Database.DBCONTEXT))
                    {
                        switch (this.crudMode)
                        {
                            case CrudMode.New:
                                service.Add(customer);
                                this.hasDataGrid.MainGrid.ItemsSource = service.GetAllCustomers();
                                break;
                            case CrudMode.Edit:
                                using (CryptoService crypto = new CryptoService(CryptoConstants.USER_KEY))
                                {
                                    if (this.cecEdit.pbPassword.Password.Length > 0 && !this.cecEdit.pbPassword.Password.Equals(service.GetCustomer(customer.Id).Password))
                                    {
                                        customer.Password = crypto.Encrypt(this.cecEdit.pbPassword.Password);
                                    }
                                    service.GetCustomer(customer.Id).ChangeData(customer);
                                    service.DBContext.SaveChanges();
                                    if (this.invoker != null)
                                    {
                                        if (this.invoker is UserAccountControl)
                                        {
                                            var uac = this.invoker as UserAccountControl;
                                            CommonConstants.LOGGED_CUSTOMER = service.GetCustomer(CommonConstants.LOGGED_CUSTOMER.Id);
                                            uac.DataContext = CommonConstants.LOGGED_CUSTOMER;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case NavigationMode.Cases:
                    if (AnyFieldsEmpty(this.cccEdit.grdCustomer))
                        return;
                    Case casee = this.cccEdit.DataContext as Case;
                    using (CaseService service = new CaseService(Database.DBCONTEXT))
                    {
                        switch (this.crudMode)
                        {
                            case CrudMode.New:
                                service.Add(casee);
                                this.hasDataGrid.MainGrid.ItemsSource = service.GetAllCases(casee.CustomerId);
                                break;
                            case CrudMode.Edit:
                                service.GetCase(casee.Id).ChangeData(casee);
                                service.DBContext.SaveChanges();
                                break;
                        }
                    }
                    break;
                case NavigationMode.Policies:
                    if (AnyFieldsEmpty(this.pecEdit.grdPolicy))
                        return;
                    Policy policy = this.pecEdit.DataContext as Policy;
                    using (PolicyService service = new PolicyService(Database.DBCONTEXT))
                    {
                        switch (this.crudMode)
                        {
                            case CrudMode.New:
                                service.Add(policy);
                                this.hasDataGrid.MainGrid.ItemsSource = service.GetAllPolicies(policy.EmployeeId);
                                break;
                            case CrudMode.Edit:
                                var pol = service.GetPolicy(policy.Id);
                                pol.ChangeData(policy);
                                Console.WriteLine(pol.CustomerId);
                                service.DBContext.SaveChanges();
                                break;
                        }
                    }
                    break;
            }
            this.Close();
        }
    }
}
