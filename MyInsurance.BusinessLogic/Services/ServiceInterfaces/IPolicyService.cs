using MyInsurance.BusinessLogic.Services.Dto;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IPolicyService
    {
        void Add(PolicyDto policy);
        CustomerDto GetPolicyCustomer(int policyId);
        EmployeeDto GetPolicyEmployee(int policyId);
    }
}
