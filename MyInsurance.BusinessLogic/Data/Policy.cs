//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyInsurance.BusinessLogic.Data
{
    using System;
    using System.Collections.Generic;

    public partial class Policy
    {
        public Policy()
        {
            this.CustomerId = 0;
            this.EmployeeId = 0;
            this.Amount = 0;
            this.Type = String.Empty;
            this.Name = String.Empty;
            this.DateOfEnding = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
        }

        public Policy(Policy temp) : this()
        {
            this.Id = temp.Id;
            this.CustomerId = temp.CustomerId;
            this.EmployeeId = temp.EmployeeId;
            this.Amount = temp.Amount;
            this.Type = temp.Type;
            this.Name = temp.Name;
            this.DateOfEnding = new DateTime(temp.DateOfEnding.Year, temp.DateOfEnding.Month, temp.DateOfEnding.Day);
            this.Customer = temp.Customer;
            this.Employee = temp.Employee;
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public System.DateTime DateOfEnding { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }

        public void ChangeData(Policy temp)
        {
            this.CustomerId = temp.CustomerId;
            this.EmployeeId = temp.EmployeeId;
            this.Amount = temp.Amount;
            this.Type = temp.Type;
            this.Name = temp.Name;
            this.DateOfEnding = new DateTime(temp.DateOfEnding.Year, temp.DateOfEnding.Month, temp.DateOfEnding.Day);
        }
    }
}
