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
using System.Windows.Shapes;

namespace MyInsurance.EmployeeGui.Controls.Management
{
    /// <summary>
    /// Interaction logic for UserAccountControl.xaml
    /// </summary>
    public partial class UserAccountControl : UserControl, INavigator
    {
        public Brush ButtonsForeground
        {
            get { return (Brush)GetValue(ButtonsForegroundProperty); }
            set { SetValue(ButtonsForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(UserAccountControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as UserAccountControl;
                var value = e.NewValue as Brush;
                source.btnChangeData.Foreground = value;
                source.btnClose.Foreground = value;
            })));

        public Brush ButtonsBackground
        {
            get { return (Brush)GetValue(ButtonsBackgroundProperty); }
            set { SetValue(ButtonsBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsBackgroundProperty =
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(UserAccountControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as UserAccountControl;
                var value = e.NewValue as Brush;
                source.btnChangeData.Background = value;
                source.btnClose.Background = value;
            })));

        public ICommand CommandBack
        {
            get { return (ICommand)GetValue(CommandBackProperty); }
            set { SetValue(CommandBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandBack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandBackProperty =
            DependencyProperty.Register("CommandBack", typeof(ICommand), typeof(UserAccountControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as UserAccountControl;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public UserAccountControl()
        {
            InitializeComponent();
        }
        public Enums.NavigationMode ControlMode
        {
            get
            {
                return NavigationMode.Account;
            }
        }
    }
}
