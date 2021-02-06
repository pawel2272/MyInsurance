using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
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
        private NavigationMode navigationMode;
        private CrudMode crudMode;
        private IHasDataGrid hasDataGrid;
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(NavigationMode type, CrudMode crudMode, IHasDataGrid hasDataGrid)
        {
            InitializeComponent();
            this.navigationMode = type;
            this.crudMode = crudMode;
            this.hasDataGrid = hasDataGrid;
            switch (type)
            {
                case NavigationMode.Employees:
                    this.eecEdit.Visibility = Visibility.Visible;
                    this.eecEdit.Mode = crudMode;
                    this.RenderSize = this.eecEdit.DesiredSize;
                    this.Height = 500;
                    switch (this.crudMode)
                    {
                        case CrudMode.New:
                            this.eecEdit.DataContext = new Employee();
                            break;
                        case CrudMode.Edit:
                            this.eecEdit.DataContext = new Employee(hasDataGrid.MainGrid.SelectedItem as Employee);
                            break;
                    }
                    break;
                case NavigationMode.Cases:
                    this.cccEdit.Visibility = Visibility.Visible;
                    this.cccEdit.Mode = crudMode;
                    this.Height = 410;
                    switch (this.crudMode)
                    {
                        case CrudMode.New:
                            this.cccEdit.DataContext = new Case();
                            break;
                        case CrudMode.Edit:
                            this.cccEdit.DataContext = new Case(hasDataGrid.MainGrid.SelectedItem as Case);
                            break;
                    }
                    break;
                case NavigationMode.Policies:
                    this.pecEdit.Visibility = Visibility.Visible;
                    this.pecEdit.Mode = crudMode;
                    this.Height = 210;
                    switch (this.crudMode)
                    {
                        case CrudMode.New:
                            this.pecEdit.DataContext = new Policy();
                            break;
                        case CrudMode.Edit:
                            this.pecEdit.DataContext = new Policy(hasDataGrid.MainGrid.SelectedItem as Policy);
                            break;
                    }
                    break;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz wprowadzić zmiany?", "Zmieniono dane", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            switch (this.navigationMode)
            {
                case NavigationMode.Employees:
                    Employee employee = this.DataContext as Employee;
                    using (IEmployeeService service = new EmployeeService())
                    {
                        switch (this.crudMode)
                        {
                            case CrudMode.New:
                                service.Add(employee);
                                this.hasDataGrid.MainGrid.ItemsSource = service.GetAllEmployees();
                                break;
                            case CrudMode.Edit:
                                if (!service.GetEmployee(employee.Id).Password.Equals(employee.Password))
                                {
                                    using (CryptoService crypto = new CryptoService(CryptoConstants.USER_KEY))
                                    {
                                        employee.Password = crypto.Encrypt(employee.Password);
                                    }
                                }
                                service.GetEmployee(employee.Id).ChangeData(employee);
                                service.DBContext.SaveChanges();
                                break;
                        }
                    }
                    break;
                case NavigationMode.Cases:
                    Case casee = this.DataContext as Case;
                    using (ICaseService service = new CaseService())
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
                    Policy policy = this.DataContext as Policy;
                    using (IPolicyService service = new PolicyService())
                    {
                        switch (this.crudMode)
                        {
                            case CrudMode.New:
                                service.Add(policy);
                                this.hasDataGrid.MainGrid.ItemsSource = service.GetAllPolicies(policy.EmployeeId);
                                break;
                            case CrudMode.Edit:
                                service.GetPolicy(policy.Id).ChangeData(policy);
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
