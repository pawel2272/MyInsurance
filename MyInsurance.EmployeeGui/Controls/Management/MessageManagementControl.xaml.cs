using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyInsurance.EmployeeGui.Controls.Management
{
    /// <summary>
    /// Interaction logic for MessageManagementControl.xaml
    /// </summary>
    public partial class MessageManagementControl : UserControl, INavigator
    {
        public List<Case> CaseList
        {
            get { return (List<Case>)GetValue(CaseListProperty); }
            set { SetValue(CaseListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaseList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaseListProperty =
            DependencyProperty.Register("CaseList", typeof(List<Case>), typeof(MessageManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessageManagementControl;
                var value = e.NewValue as List<Case>;
                source.peopleControl.CaseList = value;
            })));

        public List<Message> MessageList
        {
            get { return (List<Message>)GetValue(MessageListProperty); }
            set { SetValue(MessageListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageListProperty =
            DependencyProperty.Register("MessageList", typeof(List<Message>), typeof(MessageManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessageManagementControl;
                var value = e.NewValue as List<Message>;
                source.messageContainerControl.MessageList = value;
            })));

        public ICommand CommandBack
        {
            get { return (ICommand)GetValue(CommandBackProperty); }
            set { SetValue(CommandBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandBack.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandBackProperty =
            DependencyProperty.Register("CommandBack", typeof(ICommand), typeof(MessageManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessageManagementControl;
                var value = e.NewValue as CommandBinding;
                source.CommandBindings.Add(value);
            })));

        public Brush ButtonsForeground
        {
            get { return (Brush)GetValue(ButtonsForegroundProperty); }
            set { SetValue(ButtonsForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), typeof(MessageManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessageManagementControl;
                var value = e.NewValue as Brush;
                source.btnClose.Foreground = value;
                source.btnSend.Foreground = value;
            })));

        public Brush ButtonsBackground
        {
            get { return (Brush)GetValue(ButtonsBackgroundProperty); }
            set { SetValue(ButtonsBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonsBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsBackgroundProperty =
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), typeof(MessageManagementControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessageManagementControl;
                var value = e.NewValue as Brush;
                source.btnClose.Background = value;
                source.btnSend.Background = value;
            })));

        public int SelectedCase
        {
            get
            {
                if (this.peopleControl.lvCustomers.SelectedItem != null)
                    return ((Case)this.peopleControl.lvCustomers.SelectedItem).Id;
                return 0;
            }
            set
            {
                this.peopleControl.lvCustomers.SelectedIndex = value;
            }
        }

        public MessageManagementControl()
        {
            InitializeComponent();
        }

        private void lvCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            if (lv.Items.Count > 0)
            {
                var casee = lv.SelectedItem as Case;
                if (casee.Messages != null)
                    this.messageContainerControl.MessageList = casee.Messages.ToList();
                else
                    this.messageContainerControl.MessageList = new List<Message>();
            }
        }

        public Enums.NavigationMode ControlMode
        {
            get
            {
                return NavigationMode.Messages;
            }
        }

        private void RefreshMessages()
        {
            Case cas = (Case)this.peopleControl.lvCustomers.SelectedItem;
            using (var service = new MessageService(Database.DBCONTEXT))
            {
                this.MessageList = service.GetCaseMessages(cas.Id);
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Case cas = (Case)this.peopleControl.lvCustomers.SelectedItem;
            var rtb = this.msgTextBox;
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            if (textRange.Text.Length > 0)
            {
                using (var service = new MessageService(Database.DBCONTEXT))
                {
                    service.Add(cas.Id, textRange.Text, true, CommonConstants.LOGGED_EMPLOYEE.Id, cas.CustomerId);
                }
                rtb.Document.Blocks.Clear();
                this.RefreshMessages();
            }
        }
    }
}
