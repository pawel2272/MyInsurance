using MyInsurance.BusinessLogic.Data;
using MyInsurance.EmployeeGui.Controls.Management.Enums;
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

namespace MyInsurance.EmployeeGui.Controls.Edit
{
    /// <summary>
    /// Interaction logic for EmployeeEditControl.xaml
    /// </summary>
    public partial class EmployeeEditControl : UserControl
    {
        public Brush ButtonsForeground
        {
            get { return (Brush)GetValue(ButtonsForegroundProperty); }
            set { SetValue(ButtonsForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(EmployeeEditControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as EmployeeEditControl;
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
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(EmployeeEditControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as EmployeeEditControl;
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
            DependencyProperty.Register("Mode", typeof(CrudMode), typeof(EmployeeEditControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as EmployeeEditControl;
                var value = (CrudMode)e.NewValue;
            })));

        public RoutedEventHandler BtnOKClick
        {
            get { return (RoutedEventHandler)GetValue(BtnOKClickProperty); }
            set { SetValue(BtnOKClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnOKClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnOKClickProperty =
            DependencyProperty.Register("BtnOKClick", typeof(RoutedEventHandler), typeof(EmployeeEditControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as EmployeeEditControl;
                var value = e.NewValue as RoutedEventHandler;
                source.btnOK.Click += value;
            })));

        public EmployeeEditControl()
        {
            InitializeComponent();
            ((Employee)this.Resources["loggedEmployee"]).ChangeData(BusinessLogic.Constants.CommonConstants.LOGGED_EMPLOYEE).NotifyPermissionsChanged();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var employee = this.DataContext as Employee;
                pbPassword.Password = employee.Password;
            }
        }
    }
}
