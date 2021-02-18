using CsvHelper.Configuration;
using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Constants
{
    /// <summary>
    /// Klasa <c>PM</c> zawierająca mapę klasy Policy.
    /// </summary>
    public sealed class PM : ClassMap<Policy>
    {
        public PM()
        {
            Map(m => m.CustomerId).Index(0);
            Map(m => m.EmployeeId).Index(1);
            Map(m => m.Amount).Index(2);
            Map(m => m.Type).Index(3);
            Map(m => m.Name).Index(4);
            Map(m => m.DateOfEnding).Index(5);
        }
    }

    /// <summary>
    /// Klasa <c>CM</c> zawierająca mapę klasy Case.
    /// </summary>
    public sealed class CM : ClassMap<Case>
    {
        public CM()
        {
            Map(m => m.EmployeeId).Index(0);
            Map(m => m.Description).Index(1);
            Map(m => m.Decision).Index(2);
            Map(m => m.IsEnded).Index(3);
            Map(m => m.CustomerId).Index(4);
        }
    }

    /// <summary>
    /// Klasa <c>EM</c> zawierająca mapę klasy Employee.
    /// </summary>
    public sealed class EM : ClassMap<Employee>
    {
        public EM()
        {
            Map(m => m.FirstName).Index(0);
            Map(m => m.LastName).Index(1);
            Map(m => m.Street).Index(2);
            Map(m => m.HouseNumber).Index(3);
            Map(m => m.City).Index(4);
            Map(m => m.ZipCode).Index(5);
            Map(m => m.Salary).Index(6);
            Map(m => m.BirthDate).Index(7);
            Map(m => m.Pesel).Index(8);
            Map(m => m.EmailAddress).Index(9);
            Map(m => m.Login).Index(10);
            Map(m => m.Password).Index(11);
            Map(m => m.IsAdmin).Index(12);
            Map(m => m.IsBoss).Index(13);
            Map(m => m.IsActive).Index(14);
            Map(m => m.PhoneNumber).Index(15);
        }
    }
}
