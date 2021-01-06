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
    /// Interaction logic for CrudMenu.xaml
    /// </summary>
    public partial class CrudMenu : UserControl
    {
        public CrudMenu()
        {
            ApplicationCommands.SaveAs.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
            InitializeComponent();
        }

        public ICommand CommandExit
        {
            get { return (ICommand)GetValue(CommandExitProperty); }
            set { SetValue(CommandExitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandExitProperty =
            DependencyProperty.Register("CommandExit", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
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
            DependencyProperty.Register("CommandLogout", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s,e) => {
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
            DependencyProperty.Register("CommandAbout", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
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
            DependencyProperty.Register("CommandManageAcc", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
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
            DependencyProperty.Register("CommandManageEmp", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandNew
        {
            get { return (ICommand)GetValue(CommandNewProperty); }
            set { SetValue(CommandNewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandNewProperty =
            DependencyProperty.Register("CommandNew", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandEdit
        {
            get { return (ICommand)GetValue(CommandEditProperty); }
            set { SetValue(CommandEditProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandEditProperty =
            DependencyProperty.Register("CommandEdit", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandDelete
        {
            get { return (ICommand)GetValue(CommandDeleteProperty); }
            set { SetValue(CommandDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandDeleteProperty =
            DependencyProperty.Register("CommandDelete", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandOpen
        {
            get { return (ICommand)GetValue(CommandOpenProperty); }
            set { SetValue(CommandOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandOpenProperty =
            DependencyProperty.Register("CommandOpen", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandSave
        {
            get { return (ICommand)GetValue(CommandSaveProperty); }
            set { SetValue(CommandSaveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandSaveProperty =
            DependencyProperty.Register("CommandSave", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandSaveAs
        {
            get { return (ICommand)GetValue(CommandSaveAsProperty); }
            set { SetValue(CommandSaveAsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandSaveAsProperty =
            DependencyProperty.Register("CommandSaveAs", typeof(ICommand), typeof(CrudMenu), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as CrudMenu;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));
    }
}
