﻿using MyInsurance.BusinessLogic.Data;
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

namespace MyInsurance.CustomerGui.Controls.Messaging
{
    /// <summary>
    /// Interaction logic for MessagePeopleControl.xaml
    /// </summary>
    public partial class MessagePeopleControl : UserControl
    {
        public List<Case> CaseList
        {
            get { return (List<Case>)GetValue(CaseListProperty); }
            set { SetValue(CaseListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaseList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaseListProperty =
            DependencyProperty.Register("CaseList", typeof(List<Case>), typeof(MessagePeopleControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessagePeopleControl;
                var value = e.NewValue as List<Case>;
                source.lvCustomers.ItemsSource = value;
            })));

        public SelectionChangedEventHandler SelectionChanged
        {
            get { return (SelectionChangedEventHandler)GetValue(SelectionChangedProperty); }
            set { SetValue(SelectionChangedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionChangedProperty =
            DependencyProperty.Register("SelectionChanged", typeof(SelectionChangedEventHandler), typeof(MessagePeopleControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessagePeopleControl;
                var value = e.NewValue as SelectionChangedEventHandler;
                source.lvCustomers.SelectionChanged += value;
            })));

        public MessagePeopleControl()
        {
            InitializeComponent();
        }
    }
}
