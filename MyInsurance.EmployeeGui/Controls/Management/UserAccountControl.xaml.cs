﻿using MyInsurance.EmployeeGui.Controls.Management.Enums;
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
