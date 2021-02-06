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

    public partial class Employee : ILoginable, IDataErrorInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Cases = new HashSet<Case>();
            this.Policies = new HashSet<Policy>();
            this.FirstName = String.Empty;
            this.LastName = String.Empty;
            this.Street = String.Empty;
            this.HouseNumber = 0;
            this.City = String.Empty;
            this.ZipCode = String.Empty;
            this.Salary = 0;
            this.BirthDate = DateTime.Now;
            this.Pesel = String.Empty;
            this.EmailAddress = String.Empty;
            this.Login = String.Empty;
            this.Password = String.Empty;
            this.IsAdmin = false;
            this.IsBoss = false;
            this.IsActive = false;
            this.PhoneNumber = String.Empty;
        }

        public Employee(Employee old)
        {
            this.Cases = new HashSet<Case>();
            this.Policies = new HashSet<Policy>();
            this.Id = old.Id;
            this.FirstName = old.FirstName;
            this.LastName = old.LastName;
            this.Street = old.Street;
            this.HouseNumber = old.HouseNumber;
            this.City = old.City;
            this.ZipCode = old.ZipCode;
            this.Salary = old.Salary;
            this.BirthDate = new DateTime(old.BirthDate.Year, old.BirthDate.Month, old.BirthDate.Day);
            this.Pesel = old.Pesel;
            this.EmailAddress = old.EmailAddress;
            this.Login = old.Login;
            this.Password = old.Password;
            this.IsAdmin = old.IsAdmin;
            this.IsBoss = old.IsBoss;
            this.IsActive = old.IsActive;
            this.PhoneNumber = old.PhoneNumber;
        }

        public Employee ChangeData(Employee old)
        {
            this.FirstName = old.FirstName;
            this.LastName = old.LastName;
            this.Street = old.Street;
            this.HouseNumber = old.HouseNumber;
            this.City = old.City;
            this.ZipCode = old.ZipCode;
            this.Salary = old.Salary;
            this.BirthDate = new DateTime(old.BirthDate.Year, old.BirthDate.Month, old.BirthDate.Day);
            this.Pesel = old.Pesel;
            this.EmailAddress = old.EmailAddress;
            this.Login = old.Login;
            this.Password = old.Password;
            this.IsAdmin = old.IsAdmin;
            this.IsBoss = old.IsBoss;
            this.IsActive = old.IsActive;
            this.PhoneNumber = old.PhoneNumber;
            return this;
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
                        else if (!Regexes.NAME_REGEX.IsMatch(FirstName))
                            errorMessage = "Imi� mo�e zawiera� wy��cznie litery i musi si� zaczyna� z du�ej!";
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(LastName))
                            errorMessage = "Nazwisko musi by� wpisane!";
                        else if (LastName.Length < 3)
                            errorMessage = "Nazwisko musi mie� minimum 3 znaki!";
                        else if (!Regexes.NAME_REGEX.IsMatch(LastName))
                            errorMessage = "Nazwisko mo�e zawiera� wy��cznie litery i musi si� zaczyna� z du�ej!";
                        break;
                    case "Street":
                        if (string.IsNullOrEmpty(Street))
                            errorMessage = "Nazwa ulicy musi by� wpisana!";
                        else if (Street.Length < 3)
                            errorMessage = "Nazwa ulicy musi mie� minimum 3 znaki!";
                        else if (!Regexes.CITY_REGEX.IsMatch(Street))
                            errorMessage = "Nazwa ulicy mo�e zawiera� wy��cznie litery i musi si� zaczyna� z du�ej!";
                        break;
                    case "City":
                        if (string.IsNullOrEmpty(City))
                            errorMessage = "Nazwa miasta musi by� wpisana!";
                        else if (City.Length < 3)
                            errorMessage = "Nazwa miasta mie� minimum 3 znaki!";
                        else if (!Regexes.CITY_REGEX.IsMatch(City))
                            errorMessage = "Nazwa miasta mo�e zawiera� wy��cznie litery i musi si� zaczyna� z du�ej!";
                        break;
                    case "ZipCode":
                        if (!Regexes.ZIPCODE_REGEX.IsMatch(ZipCode))
                            errorMessage = "Wpisz prawid�owy kod pocztowy!";
                        break;
                    case "Pesel":
                        if (!Regexes.PESEL_REGEX.IsMatch(Pesel))
                            errorMessage = "Wpisz prawid�owy numer PESEL!";
                        break;
                    case "EmailAddress":
                        if (!Regexes.EMAIL_REGEX.IsMatch(EmailAddress))
                            errorMessage = "Wpisz prawid�owy adres e-mail!";
                        break;
                    case "PhoneNumber":
                        if (!Regexes.PHONENUMBER_REGEX.IsMatch(PhoneNumber))
                            errorMessage = "Wpisz prawid�owy numer telefonu!";
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
        public decimal Salary { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Pesel { get; set; }
        public string EmailAddress { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBoss { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Policy> Policies { get; set; }

        public string Error => throw new NotImplementedException();
    }
}
