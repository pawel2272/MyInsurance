﻿using System.Windows;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
using MyInsurance.BusinessLogic.Constants;
using System;

namespace MyInsurance
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.loginWindow = this;
            App.openedWindows.Add(this);
            lcLogin.tbLogin.Text = "admin";
            lcLogin.pbPassword.Password = "admin";
            lcLogin.rbEmployee.IsChecked = true;
            lcLogin.LogIn();
        }
    }
}
