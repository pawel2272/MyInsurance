using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyInsurance.CustomerGui.Controls.Management.Interfaces
{
    public interface IHasDataGrid
    {
        DataGrid MainGrid { get; }
    }
}
