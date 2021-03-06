﻿using MyInsurance.CustomerGui.Controls.Management.Enums;
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

namespace MyInsurance.CustomerGui.Controls.Management
{
    /// <summary>
    /// Interaction logic for PolicyManagementControl.xaml
    /// </summary>
    public partial class PolicyManagementControl : UserControl, IHasDataGrid, INavigator
    {
        public ICommand CommandBack
        {
            get { return (ICommand)GetValue(CommandBackProperty); }
            set { SetValue(CommandBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandBack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandBackProperty =
            DependencyProperty.Register("CommandBack", typeof(ICommand), typeof(PolicyManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as PolicyManagementControl;
                var value = e.NewValue as CommandBinding;
                source.cbButtons.CommandBindings.Add(value);
            })));

        public ICommand CommandNew
        {
            get { return (ICommand)GetValue(CommandNewProperty); }
            set { SetValue(CommandNewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandNewProperty =
            DependencyProperty.Register("CommandNew", typeof(ICommand), typeof(PolicyManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as PolicyManagementControl;
                var value = e.NewValue as CommandBinding;
                source.cbButtons.CommandBindings.Add(value);
            })));

        public ICommand CommandEdit
        {
            get { return (ICommand)GetValue(CommandEditProperty); }
            set { SetValue(CommandEditProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandExit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandEditProperty =
            DependencyProperty.Register("CommandEdit", typeof(ICommand), typeof(PolicyManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as PolicyManagementControl;
                var value = e.NewValue as CommandBinding;
                source.cbButtons.CommandBindings.Add(value);
            })));

        public Brush ButtonsForeground
        {
            get { return (Brush)GetValue(ButtonsForegroundProperty); }
            set { SetValue(ButtonsForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(PolicyManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as PolicyManagementControl;
                var value = e.NewValue as Brush;
                source.cbButtons.btnBack.Foreground = value;
                source.cbButtons.btnNew.Foreground = value;
                source.cbButtons.btnEdit.Foreground = value;
            })));

        public Brush ButtonsBackground
        {
            get { return (Brush)GetValue(ButtonsBackgroundProperty); }
            set { SetValue(ButtonsBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsBackgroundProperty =
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(PolicyManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as PolicyManagementControl;
                var value = e.NewValue as Brush;
                source.cbButtons.btnBack.Background = value;
                source.cbButtons.btnNew.Background = value;
                source.cbButtons.btnEdit.Background = value;
            })));

        public DataGrid MainGrid
        {
            get
            {
                return this.dgPolicies;
            }
        }

        public Enums.NavigationMode ControlMode
        {
            get
            {
                return NavigationMode.Policies;
            }
        }

        public PolicyManagementControl()
        {
            InitializeComponent();
        }
    }
}
