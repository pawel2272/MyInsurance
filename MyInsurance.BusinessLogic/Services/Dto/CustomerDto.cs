using MyInsurance.BusinessLogic.Interfaces;

namespace MyInsurance.BusinessLogic.Services.Dto
{
    public class CustomerDto : ILoginable
    {
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
    }
}
