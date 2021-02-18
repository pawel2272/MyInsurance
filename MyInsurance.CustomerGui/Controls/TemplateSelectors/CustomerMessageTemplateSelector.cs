using MyInsurance.BusinessLogic.Data;
using System.Windows;
using System.Windows.Controls;

namespace MyInsurance.CustomerGui.Controls.TemplateSelectors
{
    class CustomerMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SenderTemplate { get; set; }
        public DataTemplate RecieverTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var message = item as Message;
            if (message.IsFromAgent)
                return this.RecieverTemplate;
            return this.SenderTemplate;
        }
    }
}
