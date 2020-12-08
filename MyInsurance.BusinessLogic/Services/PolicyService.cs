using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Dto;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;

namespace MyInsurance.BusinessLogic.Services
{
    public class PolicyService : IPolicyService, IDisposable
    {
        private readonly InsuranceDBEntities _dbContext;

        public PolicyService()
        {
            _dbContext = new InsuranceDBEntities();
        }

        public void Add(PolicyDto policy)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public CustomerDto GetPolicyCustomer(int policyId)
        {
            throw new NotImplementedException();
        }

        public EmployeeDto GetPolicyEmployee(int policyId)
        {
            throw new NotImplementedException();
        }
    }
}
