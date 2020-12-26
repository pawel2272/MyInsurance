using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Services
{
    public class KlasaTest
    {
        public static void Main(string[] args)
        {

        }

        public void testing()
        {
            EmployeeService service = new EmployeeService();
            service.Add("admin", "admin", "admin@app.creator", "Jan", "Kowalski", new DateTime(1980, 6, 18), true, true, 99999);
        }
    }
}
