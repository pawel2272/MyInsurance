using MyInsurance.BusinessLogic.Data;
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

namespace MyInsurance.EmployeeGui.Controls.Management.Menus.Messaging
{
    /// <summary>
    /// Interaction logic for MessagePeopleControl.xaml
    /// </summary>
    public partial class MessagePeopleControl : UserControl
    {
        public List<Customer> CustomerList
        {
            get { return (List<Customer>)GetValue(CustomerListProperty); }
            set { SetValue(CustomerListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomerList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomerListProperty =
            DependencyProperty.Register("CustomerList", typeof(List<Customer>), typeof(MessagePeopleControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessagePeopleControl;
                var value = e.NewValue as List<Customer>;
                source.lvCustomers.ItemsSource = value;
            })));

        public MessagePeopleControl()
        {
            InitializeComponent();
        }
    }
}
