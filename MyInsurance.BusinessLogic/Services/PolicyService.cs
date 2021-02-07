using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Base;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    /// <summary>
    /// serwis obsługujący tabelę Policy
    /// </summary>
    public class PolicyService : CommonDbService, IPolicyService
    {
        /// <summary>
        /// Konstruktor inicjalizujący połączenie z bazą
        /// </summary>
        public PolicyService() : base()
        {
        }

        public PolicyService(InsuranceDBEntities dbContext) : base(dbContext)
        {
        }

        public Policy GetPolicy(int policyId)
        {
            return _dbContext.Policies.FirstOrDefault(p => p.Id == policyId);
        }

        private Customer GetCustomer(int customerId)
        {
            using (CustomerService customerService = new CustomerService())
            {
                return customerService.GetCustomer(customerId);
            }
        }

        private Employee GetEmployee(int employeeId)
        {
            using (EmployeeService employeeService = new EmployeeService())
            {
                return employeeService.GetEmployee(employeeId);
            }
        }

        public void Add(int customerId, int employeeId, decimal amount, string type, string name, DateTime dateOfEnding)
        {
            Policy newPolicy = new Policy()
            {
                CustomerId = customerId,
                EmployeeId = employeeId,
                Amount = amount,
                Type = type,
                Name = name,
                DateOfEnding = dateOfEnding,
                Customer = GetCustomer(customerId),
                Employee = GetEmployee(employeeId)
            };

            _dbContext.Policies.Add(newPolicy);
            _dbContext.SaveChanges();
        }

        public Customer GetPolicyCustomer(int policyId)
        {
            return GetPolicy(policyId).Customer;
        }

        public Employee GetPolicyEmployee(int policyId)
        {
            return GetPolicy(policyId).Employee;
        }

        public bool RemovePolicy(int policyId)
        {
            try
            {
                return this.RemovePolicy(this._dbContext.Policies.First(p => p.Id == policyId));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemovePolicy(Policy policy)
        {
            try
            {
                this._dbContext.Policies.Remove(policy);
            }
            catch (Exception e)
            {
                return false;
            }
            this._dbContext.SaveChanges();
            return true;
        }

        public List<Policy> GetAllPolicies(int agentId)
        {
            try
            {
                return this._dbContext.Employees.First(e => e.Id == agentId).Policies.ToList();
            }
            catch (Exception e)
            {
                
            }
            return new List<Policy>();
        }

        public void Add(Policy policy)
        {
            _dbContext.Policies.Add(policy);
            _dbContext.SaveChanges();
        }
    }
}
