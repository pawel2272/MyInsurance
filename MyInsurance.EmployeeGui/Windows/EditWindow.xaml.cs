using MyInsurance.EmployeeGui.Controls.Management.Enums;
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

namespace MyInsurance.EmployeeGui.Windows
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
            this.RenderSize = this.eecEdit.DesiredSize;
        }

        public EditWindow(NavigationMode type, CrudMode crudMode)
        {
            InitializeComponent();
            switch (type)
            {
                case NavigationMode.Employees:
                    this.eecEdit.Visibility = Visibility.Visible;
                    this.eecEdit.Mode = crudMode;
                    this.RenderSize = this.eecEdit.DesiredSize;
                    break;
                case NavigationMode.Cases:
                    this.cccEdit.Visibility = Visibility.Visible;
                    this.cccEdit.Mode = crudMode;
                    this.RenderSize = this.cccEdit.DesiredSize;
                    break;
                case NavigationMode.Policies:
                    this.pecEdit.Visibility = Visibility.Visible;
                    this.pecEdit.Mode = crudMode;
                    this.RenderSize = this.pecEdit.DesiredSize;
                    break;
            }
        }
    }
}
