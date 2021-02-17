using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using MyInsurance.EmployeeGui.Controls.Management;
using MyInsurance.EmployeeGui.Controls.Management.Enums;
using MyInsurance.EmployeeGui.Controls.Management.Interfaces;
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

namespace MyInsurance.EmployeeGui.Windows
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
                case NavigationMode.Employees:
                    this.eecEdit.Visibility = Visibility.Visible;
                    this.eecEdit.Mode = crudMode;
                    this.Height = 500;
                    switch (this.crudMode)
                    {
                        case CrudMode.New:
                            this.eecEdit.DataContext = new Employee();
                            break;
                        case CrudMode.Edit:
                            if (hasDataGrid != null)
                                this.eecEdit.DataContext = new Employee(((IHasDataGrid)hasDataGrid).MainGrid.SelectedItem as Employee);
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
                case NavigationMode.Employees:
                    if (AnyFieldsEmpty(this.eecEdit.grdEmployee))
                        return;
                    Employee employee = this.eecEdit.DataContext as Employee;
                    using (EmployeeService service = new EmployeeService(Database.DBCONTEXT))
                    {
                        switch (this.crudMode)
                        {
                            case CrudMode.New:
                                service.Add(employee);
                                this.hasDataGrid.MainGrid.ItemsSource = service.GetAllEmployees();
                                break;
                            case CrudMode.Edit:
                                using (CryptoService crypto = new CryptoService(CryptoConstants.USER_KEY))
                                {
                                    if (this.eecEdit.pbPassword.Password.Length > 0 && !this.eecEdit.pbPassword.Password.Equals(service.GetEmployee(employee.Id).Password))
                                    {
                                        employee.Password = crypto.Encrypt(this.eecEdit.pbPassword.Password);
                                    }
                                    service.GetEmployee(employee.Id).ChangeData(employee);
                                    service.DBContext.SaveChanges();
                                    if (this.invoker != null)
                                    {
                                        if (this.invoker is UserAccountControl)
                                        {
                                            var uac = this.invoker as UserAccountControl;
                                            CommonConstants.LOGGED_EMPLOYEE = service.GetEmployee(CommonConstants.LOGGED_EMPLOYEE.Id);
                                            uac.DataContext = CommonConstants.LOGGED_EMPLOYEE;
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
