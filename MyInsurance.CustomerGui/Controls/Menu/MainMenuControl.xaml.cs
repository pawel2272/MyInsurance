using MyInsurance.CustomerGui.Controls.Management.Enums;
using MyInsurance.CustomerGui.Controls.Management.Interfaces;
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

namespace MyInsurance.CustomerGui.Controls.Menu
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
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as Brush;
                source.btnPolicies.Foreground = value;
                source.btnCases.Foreground = value;
                source.btnMessages.Foreground = value;
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
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as Brush;
                source.btnPolicies.Background = value;
                source.btnCases.Background = value;
                source.btnMessages.Background = value;
                source.btnAccount.Background = value;
                source.btnLogout.Background = value;
            })));

        public ICommand CommandLogout
        {
            get { return (ICommand)GetValue(CommandLogoutProperty); }
            set { SetValue(CommandLogoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandLogout.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandLogoutProperty =
            DependencyProperty.Register("CommandLogout", typeof(ICommand), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandPolicies
        {
            get { return (ICommand)GetValue(CommandPoliciesProperty); }
            set { SetValue(CommandPoliciesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandPolicies.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandPoliciesProperty =
            DependencyProperty.Register("CommandPolicies", typeof(int), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandEmployees
        {
            get { return (ICommand)GetValue(CommandEmployeesProperty); }
            set { SetValue(CommandEmployeesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandEmployees.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandEmployeesProperty =
            DependencyProperty.Register("CommandEmployees", typeof(ICommand), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));


        public ICommand CommandCases
        {
            get { return (ICommand)GetValue(CommandCasesProperty); }
            set { SetValue(CommandCasesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandCases.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandCasesProperty =
            DependencyProperty.Register("CommandCases", typeof(ICommand), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandMessages
        {
            get { return (ICommand)GetValue(CommandMessagesProperty); }
            set { SetValue(CommandMessagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandMessages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandMessagesProperty =
            DependencyProperty.Register("CommandMessages", typeof(ICommand), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public ICommand CommandAccount
        {
            get { return (ICommand)GetValue(CommandAccountProperty); }
            set { SetValue(CommandAccountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandAccount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandAccountProperty =
            DependencyProperty.Register("CommandAccount", typeof(ICommand), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public RoutedEventHandler BtnNavigationClick
        {
            get { return (RoutedEventHandler)GetValue(BtnNavigationClickProperty); }
            set { SetValue(BtnNavigationClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnNavigationClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnNavigationClickProperty =
            DependencyProperty.Register("BtnNavigationClick", typeof(RoutedEventHandler), typeof(MainMenuControl), new PropertyMetadata(new PropertyChangedCallback((s, e) =>
            {
                var source = s as MainMenuControl;
                var value = e.NewValue as RoutedEventHandler;
                source.btnAccount.Click += value;
                source.btnCases.Click += value;
                source.btnLogout.Click += value;
                source.btnMessages.Click += value;
                source.btnPolicies.Click += value;
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
                    navigable.WindowMode = NavigationMode.Account;
                    return;
                }
                if (sender == this.btnLogout)
                {
                    navigable.WindowMode = NavigationMode.Logout;
                    return;
                }
                if (sender == this.btnPolicies)
                {
                    navigable.WindowMode = NavigationMode.Policies;
                    return;
                }
                if (sender == this.btnCases)
                {
                    navigable.WindowMode = NavigationMode.Cases;
                    return;
                }
                if (sender == this.btnMessages)
                {
                    navigable.WindowMode = NavigationMode.Messages;
                    return;
                }
                navigable.WindowMode = NavigationMode.NoSelected;
                return;
            }
        }
    }
}
