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

namespace MyInsurance.EmployeeGui.Controls.Start
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
        }

        private void cmdExit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdExit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void cmdLogout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdLogout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Wiadomośc");
        }

        private void cmdAbout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdAbout_Executed(object sender, ExecutedRoutedEventArgs e)
        {

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
            e.CanExecute = true;
        }

        private void cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
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
            e.CanExecute = true;
        }

        private void cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void cmdSaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void cmdSaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void HideNavigationControl()
        {
            this.mmC.Visibility = Visibility.Hidden;
        }

        private void ShowNavigationControl()
        {
            this.mmC.Visibility = Visibility.Visible;
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
                NavigationMode windowMode = navigable.WindowMode;
                this.HideNavigationControl();
                this.ShowCrudMenu();
                switch (windowMode)
                {
                    case NavigationMode.NoSelected:
                        this.ShowNavigationControl();
                        MessageBox.Show("Błąd wewnętrzny aplikacji: " + new Exception().StackTrace, "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case NavigationMode.Policies:
                        break;
                    case NavigationMode.Cases:
                        break;
                    case NavigationMode.Messages:
                        break;
                    case NavigationMode.Employees:
                        this.emcControl.Visibility = Visibility.Visible;
                        using (IEmployeeService service = new EmployeeService())
                        {
                            this.emcControl.dgEmployees.ItemsSource = service.GetAllEmployees();
                        }
                        break;
                    case NavigationMode.Account:

                        break;
                    case NavigationMode.Logout:
                        this.ShowNavigationControl();
                        this.ShowMainMenu();
                        this.Logout();
                        break;
                }
            }
        }

        private void cmdBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void cmdBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
