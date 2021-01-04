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
    using MyInsurance.BusinessLogic.Constants;
    using MyInsurance.BusinessLogic.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Customer : ILoginable, IDataErrorInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Cases = new HashSet<Case>();
            this.Policies = new HashSet<Policy>();
            this.FirstName = String.Empty;
            this.LastName = String.Empty;
            this.Street = String.Empty;
            this.HouseNumber = 0;
            this.City = String.Empty;
            this.ZipCode = String.Empty;
            this.CompanyName = String.Empty;
            this.PhoneNumber = String.Empty;
            this.NIPNumber = String.Empty;
            this.Login = String.Empty;
            this.Password = String.Empty;
            this.EmailAddress = String.Empty;
            this.Discount = 0;
            this.IsActive = false;
        }

        public string this[string propertyName]
        {
            get
            {
                string errorMessage = String.Empty;
                switch (propertyName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName))
                            errorMessage = "Imi� musi by� wpisane!";
                        else if (FirstName.Length < 3)
                            errorMessage = "Imi� musi mie� minimum 3 znaki!";
                        else if (!Regexes.PERSON_VALIDATION["name"].IsMatch(FirstName))
                            errorMessage = "Imi� mo�e zawiera� wy��cznie litery i musi si� zaczyna� z du�ej!";
                        break;
                    case "LastName":
                        break;
                    case "Street":
                        break;
                    case "HouseNumber":
                        break;
                    case "City":
                        break;
                    case "ZipCode":
                        if (!Regexes.PERSON_VALIDATION["zipcode"].IsMatch(ZipCode))
                            errorMessage = "Wpisz prawid�owy kod pocztowy!";
                        break;
                    case "BirthDate":
                        break;
                    case "Pesel":
                        break;
                    case "EmailAddress":
                        if (!Regexes.PERSON_VALIDATION["email"].IsMatch(EmailAddress))
                            errorMessage = "Wpisz prawid�owy adres e-mail!";
                        break;
                    case "PhoneNumber":
                        break;
                };
                return errorMessage;
            }
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string NIPNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public decimal Discount { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Policy> Policies { get; set; }

        public string Error => throw new NotImplementedException();
    }
}
