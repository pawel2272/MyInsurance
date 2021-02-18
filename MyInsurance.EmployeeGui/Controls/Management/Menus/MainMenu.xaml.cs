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

namespace MyInsurance.EmployeeGui.Controls.Management.Menus
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public ICommand CommandExit
        {
            get { return (ICommand)GetValue(CommandExitProperty); }
            set { SetValue(CommandExitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandExitProperty =
            DependencyProperty.Register("CommandExit", typeof(ICommand), typeof(MainMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandLogout
        {
            get { return (ICommand)GetValue(CommandLogoutProperty); }
            set { SetValue(CommandLogoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandLogoutProperty =
            DependencyProperty.Register("CommandLogout", typeof(ICommand), typeof(MainMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandAbout
        {
            get { return (ICommand)GetValue(CommandAboutProperty); }
            set { SetValue(CommandAboutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandAboutProperty =
            DependencyProperty.Register("CommandAbout", typeof(ICommand), typeof(MainMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandManageAcc
        {
            get { return (ICommand)GetValue(CommandManageAccProperty); }
            set { SetValue(CommandManageAccProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandManageAccProperty =
            DependencyProperty.Register("CommandManageAcc", typeof(ICommand), typeof(MainMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandManageEmp
        {
            get { return (ICommand)GetValue(CommandManageEmpProperty); }
            set { SetValue(CommandManageEmpProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandManageEmpProperty =
            DependencyProperty.Register("CommandManageEmp", typeof(ICommand), typeof(MainMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));
    }
}
