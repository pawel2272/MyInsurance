﻿using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    public class PolicyService : IPolicyService, IDisposable
    {
        private readonly InsuranceDBEntities _dbContext;

        public PolicyService()
        {
            _dbContext = new InsuranceDBEntities();
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


        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Customer GetPolicyCustomer(int policyId)
        {
            return GetPolicy(policyId).Customer;
        }

        public Employee GetPolicyEmployee(int policyId)
        {
            return GetPolicy(policyId).Employee;
        }
    }
}
