using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IPolicyService : IDisposable
    {
        void Add(int customerId, int employeeId, decimal amount, string type, string name, DateTime dateOfEnding);
        Policy GetPolicy(int policyId);
        Customer GetPolicyCustomer(int policyId);
        Employee GetPolicyEmployee(int policyId);
        bool RemovePolicy(int policyId);
        bool RemovePolicy(Policy policy);
        List<Policy> GetAllPolicies(int agentId);
    }
}
