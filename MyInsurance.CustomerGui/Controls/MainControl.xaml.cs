using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.CustomerGui.Controls.Management;
using MyInsurance.CustomerGui.Controls.Management.Enums;
using MyInsurance.CustomerGui.Controls.Management.Interfaces;
using MyInsurance.CustomerGui.Controls.Menu;
using MyInsurance.CustomerGui.Windows;
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

namespace MyInsurance.CustomerGui.Controls
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public bool IsExiting { get; set; }
        private NavigationMode navigationMode;
        private readonly MainMenuControl mainMenuControl;
        private readonly CaseManagementControl caseManagementControl;
        private readonly PolicyManagementControl policyManagementControl;
        private readonly UserAccountControl userAccountControl;
        private readonly MessageManagementControl messageManagementControl;

        public MainControl()
        {
            this.IsExiting = false;
            InitializeComponent();
            this.mainMenuControl = mainMenu;
            this.caseManagementControl = (CaseManagementControl)((TabItem)this.Resources["tiCaseManagement"]).Content;
            this.policyManagementControl = (PolicyManagementControl)((TabItem)this.Resources["tiPolicyManagement"]).Content;
            this.userAccountControl = (UserAccountControl)((TabItem)this.Resources["tiAccountManagement"]).Content;
            this.messageManagementControl = (MessageManagementControl)((TabItem)this.Resources["tiMessageManagement"]).Content;
        }

        private void RefreshCases()
        {
            using (CaseService service = new CaseService(Database.DBCONTEXT))
            {
                caseManagementControl.dgCases.ItemsSource = service.GetAllCases(CommonConstants.LOGGED_EMPLOYEE.Id);
            }
        }

        private void RefreshMessages()
        {
            using (var service = new CaseService(Database.DBCONTEXT))
            {
                this.messageManagementControl.CaseList = service.GetAllCases(CommonConstants.LOGGED_CUSTOMER.Id);
                this.messageManagementControl.MessageList = service.GetCaseMessages(this.messageManagementControl.SelectedCase);
            }
        }

        private void RefreshPolicies()
        {
            using (PolicyService service = new PolicyService(Database.DBCONTEXT))
            {
                policyManagementControl.dgPolicies.ItemsSource = service.GetAllPolicies(CommonConstants.LOGGED_CUSTOMER.Id, true);
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

        private void cmdManageAcc_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!tcControl.Items.Contains(this.Resources["tiAccountManagement"] as TabItem))
                e.CanExecute = true;
        }

        private void cmdManageAcc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.AddItemFromResourcesToTabControl("tiAccountManagement");
            userAccountControl.DataContext = CommonConstants.LOGGED_CUSTOMER;
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
            var temp = tcControl.SelectedItem;
            this.tcControl.SelectedItem = this.mainMenuControl;
            this.tcControl.Items.Remove(temp);
        }

        private void tcControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((TabItem)((TabControl)sender).SelectedItem != null)
            {
                TabItem tabItem = (TabItem)((TabControl)sender).SelectedItem;
                if (tabItem.Content == this.caseManagementControl)
                {
                    this.navigationMode = NavigationMode.Cases;
                    this.RefreshCases();
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
