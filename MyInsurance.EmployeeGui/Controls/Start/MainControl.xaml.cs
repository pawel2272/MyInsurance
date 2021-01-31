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

namespace MyInsurance.EmployeeGui.Controls.Start
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public bool IsExiting { get; set; }
        private NavigationMode navigationMode;
        private MainMenuControl mainMenuControl;
        private EmployeeManagementControl employeeManagementControl;
        private CaseManagementControl caseManagementControl;
        private PolicyManagementControl policyManagementControl;
        private UserAccountControl userAccountControl;
        public MainControl()
        {
            this.IsExiting = false;
            InitializeComponent();
            this.mainMenuControl = mainMenu;
            this.employeeManagementControl = (EmployeeManagementControl)((TabItem)this.Resources["tiEmployeeManagement"]).Content;
            this.caseManagementControl = (CaseManagementControl)((TabItem)this.Resources["tiCaseManagement"]).Content;
            this.policyManagementControl = (PolicyManagementControl)((TabItem)this.Resources["tiPolicyManagement"]).Content;
            this.userAccountControl = (UserAccountControl)((TabItem)this.Resources["tiAccountManagement"]).Content;
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
            userAccountControl.DataContext = App.loggedPerson;
        }

        private void cmdManageEmp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!tcControl.Items.Contains(this.Resources["tiEmployeeManagement"] as TabItem))
                e.CanExecute = true;
        }

        private void cmdManageEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (IEmployeeService service = new EmployeeService())
            {
                ((IHasDataGrid)this.AddItemFromResourcesToTabControl("tiEmployeeManagement")).MainGrid.ItemsSource = service.GetAllEmployees();
            }
        }

        private void cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(this.navigationMode, CrudMode.New);
            switch (this.navigationMode)
            {
                case NavigationMode.Employees:
                    Employee employee = new Employee(employeeManagementControl.dgEmployees.SelectedItem as Employee);
                    editWindow.eecEdit.DataContext = employee;
                    break;
                case NavigationMode.Cases:
                    Case casee = new Case();
                    editWindow.cccEdit.DataContext = casee;
                    break;
                case NavigationMode.Policies:
                    Policy policy = new Policy();
                    editWindow.pecEdit.DataContext = policy;
                    break;
            }
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
            EditWindow editWindow = new EditWindow(this.navigationMode, CrudMode.Edit);
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
                                using (ICaseService caseService = new CaseService())
                                {
                                    caseService.RemoveCase(hasDataGrid.MainGrid.SelectedItem as Case);
                                    hasDataGrid.MainGrid.ItemsSource = caseService.GetAllCases(App.loggedPerson.Id, App.loggedPerson.FirstName);
                                }
                                break;
                            case NavigationMode.Employees:
                                using (IEmployeeService employeeService = new EmployeeService())
                                {
                                    employeeService.RemoveEmployee(hasDataGrid.MainGrid.SelectedItem as Employee);
                                    hasDataGrid.MainGrid.ItemsSource = employeeService.GetAllEmployees();
                                }
                                break;
                            case NavigationMode.Policies:
                                using (IPolicyService policyService = new PolicyService())
                                {
                                    policyService.RemovePolicy(hasDataGrid.MainGrid.SelectedItem as Policy);
                                    hasDataGrid.MainGrid.ItemsSource = policyService.GetAllPolicies(App.loggedPerson.Id);
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void cmdOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

        private void cmdOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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

        private void cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {

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

        private void cmdSaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {

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
            App.openedWindows.First(w => w is MainWindow).Close();
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
                if (tabItem.Content == mainMenu || tabItem.Content == userAccountControl)
                    ShowMainMenu();
                else
                    ShowCrudMenu();
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
