using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyInsurance.EmployeeGui.TemplateSelectors
{
    class EmployeeMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SenderTemplate { get; set; }
        public DataTemplate RecieverTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var message = item as Message;
            if (message.IsFromAgent)
                return this.SenderTemplate;
            return this.RecieverTemplate;
        }
    }
}
