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

    public partial class Case
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Case()
        {
            this.Messages = new HashSet<Message>();
            this.EmployeeId = 0;
            this.Description = String.Empty;
            this.Decision = String.Empty;
            this.IsEnded = false;
            this.CustomerId = 0;
        }

        public Case(Case temp) : this()
        {
            this.Id = temp.Id;
            this.EmployeeId = temp.EmployeeId;
            this.Description = temp.Description;
            this.Decision = temp.Decision;
            this.IsEnded = temp.IsEnded;
            this.CustomerId = temp.CustomerId;
            this.Customer = temp.Customer;
            this.Employee = temp.Employee;
            this.Messages = temp.Messages;
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Description { get; set; }
        public string Decision { get; set; }
        public bool IsEnded { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        public void ChangeData(Case temp)
        {
            this.EmployeeId = temp.EmployeeId;
            this.Description = temp.Description;
            this.Decision = temp.Decision;
            this.IsEnded = temp.IsEnded;
            this.CustomerId = temp.CustomerId;
            this.Customer = temp.Customer;
            this.Employee = temp.Employee;
            this.Messages = temp.Messages;
        }
    }
}
