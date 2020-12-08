using MyInsurance.BusinessLogic.Services.Dto;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IPolicyService
    {
        void Add(PolicyDto policy);
        CustomerDto GetPolicyCustomer(int policyId);
        EmployeeDto GetPolicyEmployee(int policyId);
    }
}
