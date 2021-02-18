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

namespace MyInsurance.CustomerGui.Controls.Messaging
{
    /// <summary>
    /// Interaction logic for MessageContainerControl.xaml
    /// </summary>
    public partial class MessageContainerControl : UserControl
    {
        public List<Message> MessageList
        {
            get { return (List<Message>)GetValue(MessageListProperty); }
            set { SetValue(MessageListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageListProperty =
            DependencyProperty.Register("MessageList", typeof(List<Message>), typeof(MessageContainerControl), new PropertyMetadata(new PropertyChangedCallback((s, e) => {
                var source = s as MessageContainerControl;
                var value = e.NewValue as List<Message>;
                source.lvMessages.ItemsSource = value;
            })));

        public MessageContainerControl()
        {
            InitializeComponent();
        }
    }
}
