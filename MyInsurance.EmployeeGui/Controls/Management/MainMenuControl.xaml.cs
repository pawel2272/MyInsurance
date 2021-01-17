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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyInsurance.EmployeeGui.Controls.Management
{
    /// <summary>
    /// Interaction logic for MainMenuControl.xaml
    /// </summary>
    public partial class MainMenuControl : UserControl
    {
        public Brush ButtonsForeground
        {
            get { return (Brush)GetValue(ButtonsForegroundProperty); }
            set { SetValue(ButtonsForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MainMenuControl;
                var value = e.NewValue as Brush;
                source.btnPolicies.Foreground = value;
                source.btnCases.Foreground = value;
                source.btnMessages.Foreground = value;
                source.btnEmployees.Foreground = value;
                source.btnAccount.Foreground = value;
                source.btnLogout.Foreground = value;
            })));

        public Brush ButtonsBackground
        {
            get { return (Brush)GetValue(ButtonsBackgroundProperty); }
            set { SetValue(ButtonsBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsBackgroundProperty =
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MainMenuControl;
                var value = e.NewValue as Brush;
                source.btnPolicies.Background = value;
                source.btnCases.Background = value;
                source.btnMessages.Background = value;
                source.btnEmployees.Background = value;
                source.btnAccount.Background = value;
                source.btnLogout.Background = value;
            })));

        public RoutedEventHandler BtnNavigationClick
        {
            get { return (RoutedEventHandler)GetValue(BtnNavigationClickProperty); }
            set { SetValue(BtnNavigationClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnPoliciesClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnNavigationClickProperty =
            DependencyProperty.Register("BtnNavigationClick", typeof(RoutedEventHandler), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s,e) => {
                var source = s as MainMenuControl;
                var value = e.NewValue as RoutedEventHandler;
                source.btnPolicies.Click += value;
                source.btnCases.Click += value;
                source.btnMessages.Click += value;
                source.btnEmployees.Click += value;
                source.btnAccount.Click += value;
                source.btnLogout.Click += value;
            })));

        public MainMenuControl()
        {
            InitializeComponent();
        }

        private void btnNavigation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is INavigable)
            {
                INavigable navigable = (INavigable)sender;
                if (sender == this.btnAccount)
                {
                    navigable.WindowMode = Enums.NavigationMode.Account;
                    return;
                }
                if (sender == this.btnLogout)
                {
                    navigable.WindowMode = Enums.NavigationMode.Logout;
                    return;
                }
                if (sender == this.btnPolicies)
                {
                    navigable.WindowMode = Enums.NavigationMode.Policies;
                    return;
                }
                if (sender == this.btnCases)
                {
                    navigable.WindowMode = Enums.NavigationMode.Cases;
                    return;
                }
                if (sender == this.btnMessages)
                {
                    navigable.WindowMode = Enums.NavigationMode.Messages;
                    return;
                }
                if (sender == this.btnEmployees)
                {
                    navigable.WindowMode = Enums.NavigationMode.Employees;
                    return;
                }
                navigable.WindowMode = Enums.NavigationMode.NoSelected;
                return;
            }
        }
    }
}
