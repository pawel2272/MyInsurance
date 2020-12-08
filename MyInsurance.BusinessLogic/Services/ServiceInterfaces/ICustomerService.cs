using MyInsurance.BusinessLogic.Services.Dto;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface ICustomerService
    {
        void Add(CustomerDto customer);
        CustomerDto GetCustomer(int customerId);
        List<PolicyDto> GetCustomerPolicies(int customerId);
        List<CaseDto> GetCustomerCases(int customerId);
    }
}
