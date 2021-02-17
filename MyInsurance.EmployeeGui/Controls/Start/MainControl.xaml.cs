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
using MyInsurance.EmployeeGui.Controls.Management.Enums;
using System.Windows.Shapes;
using MyInsurance.EmployeeGui.Controls.Management;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using MyInsurance.EmployeeGui.Windows;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Constants;

namespace MyInsurance.EmployeeGui.Controls.Start
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public bool IsExiting { get; set; }
        private NavigationMode navigationMode;
        private readonly MainMenuControl mainMenuControl;
        private readonly EmployeeManagementControl employeeManagementControl;
        private readonly CaseManagementControl caseManagementControl;
        private readonly PolicyManagementControl policyManagementControl;
        private readonly UserAccountControl userAccountControl;
        private readonly MessageManagementControl messageManagementControl;
        private readonly FileService<Case> caseFileService = new FileService<Case>();
        private readonly FileService<Policy> policyFileService = new FileService<Policy>();
        private readonly FileService<Employee> employeeFileService = new FileService<Employee>();

        public MainControl()
        {
            this.IsExiting = false;
            InitializeComponent();
            this.mainMenuControl = mainMenu;
            this.employeeManagementControl = (EmployeeManagementControl)((TabItem)this.Resources["tiEmployeeManagement"]).Content;
            this.caseManagementControl = (CaseManagementControl)((TabItem)this.Resources["tiCaseManagement"]).Content;
            this.policyManagementControl = (PolicyManagementControl)((TabItem)this.Resources["tiPolicyManagement"]).Content;
            this.userAccountControl = (UserAccountControl)((TabItem)this.Resources["tiAccountManagement"]).Content;
            this.messageManagementControl = (MessageManagementControl)((TabItem)this.Resources["tiMessageManagement"]).Content;
        }

        private void RefreshCases()
        {
            using (CaseService service = new CaseService(Database.DBCONTEXT))
            {
                caseManagementControl.dgCases.ItemsSource = service.GetAllCases(CommonConstants.LOGGED_EMPLOYEE.Id, CommonConstants.LOGGED_EMPLOYEE.FirstName);
            }
        }

        private void RefreshEmployees()
        {
            using (EmployeeService service = new EmployeeService(Database.DBCONTEXT))
            {
                employeeManagementControl.dgEmployees.ItemsSource = service.GetAllEmployees();
            }
        }

        private void RefreshMessages()
        {
            using (var service = new CaseService(Database.DBCONTEXT))
            {
                this.messageManagementControl.CaseList = service.GetAllCases(CommonConstants.LOGGED_EMPLOYEE.Id, CommonConstants.LOGGED_EMPLOYEE.FirstName);
                this.messageManagementControl.MessageList = service.GetCaseMessages(this.messageManagementControl.SelectedCase);
            }
        }

        private void RefreshPolicies()
        {
            using (PolicyService service = new PolicyService(Database.DBCONTEXT))
            {
                policyManagementControl.dgPolicies.ItemsSource = service.GetAllPolicies(CommonConstants.LOGGED_EMPLOYEE.Id);
            }
        }

        INavigator AddItemFromResourcesToTabControl(string resourceKey)
        {
            TabItem tabItem = this.Resources[resourceKey] as TabItem;
            tcControl.Items.Add(tabItem);
            tcControl.SelectedItem = tabItem;
            return (INavigator)tabItem.Content;
        }

        private void cmdExit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdExit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.IsExiting = true;
            Application.Current.Shutdown();
        }

        private void cmdLogout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdLogout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Logout();
        }

        private void cmdAbout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdAbout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Autor: Paweł Harbuz\nNumer albumu: 60050", "O programie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void cmdManageAcc_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!tcControl.Items.Contains(this.Resources["tiAccountManagement"] as TabItem))
                e.CanExecute = true;
        }

        private void cmdManageAcc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AddItemFromResourcesToTabControl("tiAccountManagement");
            userAccountControl.DataContext = CommonConstants.LOGGED_EMPLOYEE;
        }

        private void cmdManageEmp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!tcControl.Items.Contains(this.Resources["tiEmployeeManagement"] as TabItem))
                e.CanExecute = true;
        }

        private void cmdManageEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AddItemFromResourcesToTabControl("tiEmployeeManagement");
        }

        private void cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(this.navigationMode, CrudMode.New, ((IHasDataGrid)((TabItem)tcControl.SelectedItem).Content));
            editWindow.ShowDialog();
        }

        private void cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TabItem tabItem = this.tcControl.SelectedItem as TabItem;
            if (tabItem.Content is IHasDataGrid)
            {
                IHasDataGrid hasDataGrid = tabItem.Content as IHasDataGrid;
                if (hasDataGrid.MainGrid.SelectedItem != null)
                {
                    e.CanExecute = true;
                }
            }
        }

        private void cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(this.navigationMode, CrudMode.Edit, ((IHasDataGrid)((TabItem)tcControl.SelectedItem).Content));
            switch (this.navigationMode)
            {
                case NavigationMode.Employees:
                    Employee employee = new Employee(employeeManagementControl.dgEmployees.SelectedItem as Employee);
                    editWindow.eecEdit.DataContext = employee;
                    break;
                case NavigationMode.Cases:
                    Case casee = new Case(caseManagementControl.dgCases.SelectedItem as Case);
                    editWindow.cccEdit.DataContext = casee;
                    break;
                case NavigationMode.Policies:
                    Policy policy = new Policy(policyManagementControl.dgPolicies.SelectedItem as Policy);
                    editWindow.pecEdit.DataContext = policy;
                    break;
            }
            editWindow.ShowDialog();
        }

        private void cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TabItem tabItem = this.tcControl.SelectedItem as TabItem;
            if (tabItem.Content is IHasDataGrid)
            {
                IHasDataGrid hasDataGrid = tabItem.Content as IHasDataGrid;
                if (hasDataGrid.MainGrid.SelectedItem != null)
                {
                    e.CanExecute = true;
                }
            }
        }

        private void cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TabItem tabItem = this.tcControl.SelectedItem as TabItem;
            if (tabItem.Content is IHasDataGrid)
            {
                IHasDataGrid hasDataGrid = tabItem.Content as IHasDataGrid;
                if (hasDataGrid.MainGrid.SelectedItem != null)
                {
                    if (hasDataGrid is INavigator)
                    {
                        INavigator navigator = (INavigator)hasDataGrid;
                        switch (navigator.ControlMode)
                        {
                            case NavigationMode.Cases:
                                using (CaseService caseService = new CaseService(Database.DBCONTEXT))
                                {
                                    caseService.RemoveCase(hasDataGrid.MainGrid.SelectedItem as Case);
                                    hasDataGrid.MainGrid.ItemsSource = caseService.GetAllCases(CommonConstants.LOGGED_EMPLOYEE.Id, CommonConstants.LOGGED_EMPLOYEE.FirstName);
                                }
                                break;
                            case NavigationMode.Employees:
                                using (EmployeeService employeeService = new EmployeeService(Database.DBCONTEXT))
                                {
                                    employeeService.RemoveEmployee(hasDataGrid.MainGrid.SelectedItem as Employee);
                                    hasDataGrid.MainGrid.ItemsSource = employeeService.GetAllEmployees();
                                }
                                break;
                            case NavigationMode.Policies:
                                using (PolicyService policyService = new PolicyService(Database.DBCONTEXT))
                                {
                                    policyService.RemovePolicy(hasDataGrid.MainGrid.SelectedItem as Policy);
                                    hasDataGrid.MainGrid.ItemsSource = policyService.GetAllPolicies(CommonConstants.LOGGED_EMPLOYEE.Id);
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void cmdOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.navigationMode == NavigationMode.Cases || this.navigationMode == NavigationMode.Policies || this.navigationMode == NavigationMode.Employees)
            {
                e.CanExecute = true;
            }
        }

        private void OpenCase()
        {
            using (CaseService service = new CaseService())
            {
                this.caseFileService.Open(service);
            }
        }

        private void OpenPolicy()
        {
            using (PolicyService service = new PolicyService())
            {
                this.policyFileService.Open(service);
            }
        }

        private void OpenEmployee()
        {
            using (EmployeeService service = new EmployeeService())
            {
                this.employeeFileService.Open(service);
            }
        }

        private void cmdOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (this.navigationMode)
            {
                case NavigationMode.Cases:
                    this.OpenCase();
                    this.RefreshCases();
                    break;
                case NavigationMode.Policies:
                    this.OpenPolicy();
                    this.RefreshPolicies();
                    break;
                case NavigationMode.Employees:
                    this.OpenEmployee();
                    this.RefreshEmployees();
                    break;
            }
        }

        private void cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TabItem tabItem = this.tcControl.SelectedItem as TabItem;
            if (tabItem.Content is IHasDataGrid)
            {
                IHasDataGrid hasDataGrid = tabItem.Content as IHasDataGrid;
                if (hasDataGrid.MainGrid.SelectedItem != null)
                {
                    switch (this.navigationMode)
                    {
                        case NavigationMode.Cases:
                            if (this.caseFileService.SavedLastTime())
                            {
                                e.CanExecute = true;
                            }
                            break;
                        case NavigationMode.Policies:
                            if (this.policyFileService.SavedLastTime())
                            {
                                e.CanExecute = true;
                            }
                            break;
                        case NavigationMode.Employees:
                            if (this.employeeFileService.SavedLastTime())
                            {
                                e.CanExecute = true;
                            }
                            break;
                    }
                }
            }
        }

        private void SaveCase(IHasDataGrid hasDataGrid)
        {
            using (CaseService service = new CaseService())
            {
                this.caseFileService.Save(hasDataGrid.MainGrid.SelectedItem as Case);
            }
        }

        private void SavePolicy(IHasDataGrid hasDataGrid)
        {
            using (PolicyService service = new PolicyService())
            {
                this.policyFileService.Save(hasDataGrid.MainGrid.SelectedItem as Policy);
            }
        }

        private void SaveEmployee(IHasDataGrid hasDataGrid)
        {
            using (EmployeeService service = new EmployeeService())
            {
                this.employeeFileService.Save(hasDataGrid.MainGrid.SelectedItem as Employee);
            }
        }

        private void cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TabItem tabItem = this.tcControl.SelectedItem as TabItem;
            var hdg = tabItem.Content as IHasDataGrid;
            switch (this.navigationMode)
            {
                case NavigationMode.Cases:
                    this.SaveCase(hdg);
                    break;
                case NavigationMode.Policies:
                    this.SavePolicy(hdg);
                    break;
                case NavigationMode.Employees:
                    this.SaveEmployee(hdg);
                    break;
            }
        }

        private void cmdSaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TabItem tabItem = this.tcControl.SelectedItem as TabItem;
            if (tabItem.Content is IHasDataGrid)
            {
                IHasDataGrid hasDataGrid = tabItem.Content as IHasDataGrid;
                if (hasDataGrid.MainGrid.SelectedItem != null)
                {
                    e.CanExecute = true;
                }
            }
        }

        private void SaveCaseAs(IHasDataGrid hasDataGrid)
        {
            using (CaseService service = new CaseService())
            {
                this.caseFileService.SaveAs(hasDataGrid.MainGrid.SelectedItem as Case);
            }
        }

        private void SavePolicyAs(IHasDataGrid hasDataGrid)
        {
            using (PolicyService service = new PolicyService())
            {
                this.policyFileService.SaveAs(hasDataGrid.MainGrid.SelectedItem as Policy);
            }
        }

        private void SaveEmployeeAs(IHasDataGrid hasDataGrid)
        {
            using (EmployeeService service = new EmployeeService())
            {
                this.employeeFileService.SaveAs(hasDataGrid.MainGrid.SelectedItem as Employee);
            }
        }

        private void cmdSaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TabItem tabItem = this.tcControl.SelectedItem as TabItem;
            var hdg = tabItem.Content as IHasDataGrid;
            switch (this.navigationMode)
            {
                case NavigationMode.Cases:
                    this.SaveCaseAs(hdg);
                    break;
                case NavigationMode.Policies:
                    this.SavePolicyAs(hdg);
                    break;
                case NavigationMode.Employees:
                    this.SaveEmployeeAs(hdg);
                    break;
            }
        }

        private void ShowMainMenu()
        {
            this.mmMenu.Visibility = Visibility.Visible;
            this.cmMenu.Visibility = Visibility.Hidden;
        }

        private void ShowCrudMenu()
        {
            this.mmMenu.Visibility = Visibility.Hidden;
            this.cmMenu.Visibility = Visibility.Visible;
        }

        private void Logout()
        {
            CommonConstants.OPENED_WINDOWS.First(w => w is MainWindow).Close();
        }

        private void cmdBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.tcControl.Items.Remove(tcControl.SelectedItem);
        }

        private void tcControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((TabItem)((TabControl)sender).SelectedItem != null)
            {
                TabItem tabItem = (TabItem)((TabControl)sender).SelectedItem;
                if (tabItem.Content == mainMenu || tabItem.Content == userAccountControl || tabItem.Content == messageManagementControl)
                    ShowMainMenu();
                else
                    ShowCrudMenu();
                if (tabItem.Content == this.caseManagementControl)
                {
                    this.navigationMode = NavigationMode.Cases;
                    this.RefreshCases();
                }
                if (tabItem.Content == this.employeeManagementControl)
                {
                    this.navigationMode = NavigationMode.Employees;
                    this.RefreshEmployees();
                }
                if (tabItem.Content == this.messageManagementControl)
                {
                    this.navigationMode = NavigationMode.Messages;
                    this.RefreshMessages();
                }
                if (tabItem.Content == this.policyManagementControl)
                {
                    this.navigationMode = NavigationMode.Policies;
                    this.RefreshPolicies();
                }
            }
        }

        private void cmdCases_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!tcControl.Items.Contains(this.Resources["tiCaseManagement"] as TabItem))
                e.CanExecute = true;
        }

        private void cmdCases_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AddItemFromResourcesToTabControl("tiCaseManagement");
        }

        private void cmdMessages_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!tcControl.Items.Contains(this.Resources["tiMessageManagement"] as TabItem))
                e.CanExecute = true;
        }

        private void cmdMessages_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AddItemFromResourcesToTabControl("tiMessageManagement");
            this.messageManagementControl.SelectedCase = 0;
        }

        private void cmdPolicies_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!tcControl.Items.Contains(this.Resources["tiPolicyManagement"] as TabItem))
                e.CanExecute = true;
        }

        private void cmdPolicies_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AddItemFromResourcesToTabControl("tiPolicyManagement");
        }

        private void btnNavigation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is INavigable)
            {
                this.navigationMode = ((INavigable)sender).WindowMode;
            }
        }
    }
}
