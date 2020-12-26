using MyInsurance.BusinessLogic.Services.Dto;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface ICustomerService
    {
        void Add(string username, string password, string email, string firstName, string lastName, string street, int houseNumber, string city, string zipCode, string companyName, string phoneNumber, string nipNumber = "00000000000", decimal discount = 0);
        CustomerDto GetCustomer(int customerId);
        List<PolicyDto> GetCustomerPolicies(int customerId);
        List<CaseDto> GetCustomerCases(int customerId);
        bool CheckIfExists(string username);
        bool CheckIfExists(int customerId);
    }
}
