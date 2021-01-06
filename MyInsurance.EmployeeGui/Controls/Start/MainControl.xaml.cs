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
    }
}
