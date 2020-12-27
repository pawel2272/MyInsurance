using MyInsurance.BusinessLogic.Interfaces;

namespace MyInsurance.BusinessLogic.Services.Dto
{
    public class EmployeeDto : ILoginable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public decimal Salary { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBoss { get; set; }
        public string PhoneNumber { get; set; }
    }
}
