using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Interfaces
{
    public interface IPerson
    {
        ILoginable GetPerson(string username);
    }
}
