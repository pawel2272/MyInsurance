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
        public MainControl()
        {
            this.IsExiting = false;
            InitializeComponent();
            this.mainMenuControl = mainMenu;
            this.employeeManagementControl = (EmployeeManagementControl)((TabItem)this.Resources["tiEmployeeManagement"]).Content;
            this.caseManagementControl = (CaseManagementControl)((TabItem)this.Resources["tiCaseManagement"]).Content;
            this.policyManagementControl = (PolicyManagementControl)((TabItem)this.Resources["tiPolicyManagement"]).Content;
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
            e.CanExecute = true;
        }

        private void cmdManageAcc_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void cmdManageEmp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdManageEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
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

        }

        private void cmdOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
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

        private void btnNavigation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is INavigable)
            {
                INavigable navigable = (INavigable)sender;
                NavigationMode mode = navigable.WindowMode;
                this.navigationMode = mode;
                TabItem tabItem;
                switch (mode)
                {
                    case NavigationMode.NoSelected:
                        MessageBox.Show("Błąd wewnętrzny aplikacji: " + new Exception().StackTrace, "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case NavigationMode.Policies:
                        break;
                    case NavigationMode.Cases:
                        tabItem = this.Resources["tiCaseManagement"] as TabItem;
                        using (ICaseService service = new CaseService())
                        {
                            (tabItem.Content as CaseManagementControl).dgCases.ItemsSource = service.GetAllCases(App.loggedPerson.Id, App.loggedPerson.FirstName);
                        }
                        tcControl.Items.Add(tabItem);
                        break;
                    case NavigationMode.Messages:
                        break;
                    case NavigationMode.Employees:
                        tabItem = this.Resources["tiEmployeeManagement"] as TabItem;
                        using (IEmployeeService service = new EmployeeService())
                        {
                            (tabItem.Content as EmployeeManagementControl).dgEmployees.ItemsSource = service.GetAllEmployees();
                        }
                        tcControl.Items.Add(tabItem);
                        break;
                    case NavigationMode.Account:
                        
                        break;
                    case NavigationMode.Logout:
                        this.Logout();
                        break;
                }
            }
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
                if (tabItem.Content == mainMenu)
                    ShowMainMenu();
                else
                    ShowCrudMenu();
            }
        }
    }
}
