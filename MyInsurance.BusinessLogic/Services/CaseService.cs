using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    public class CaseService : ICaseService, IDisposable
    {
        private readonly InsuranceDBEntities _dbContext;

        public CaseService()
        {
            _dbContext = new InsuranceDBEntities();
        }

        public List<Case> GetAllCases(int customerId)
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => cas.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER
            }
            return new List<Case>();
        }

        public List<Case> GetOpenedCases()
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => !cas.IsEnded)
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER
            }
            return new List<Case>();
        }

        public List<Case> GetClosedCases()
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => cas.IsEnded)
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER
            }
            return new List<Case>();
        }

        public List<Message> GetCaseMessages(int caseId)
        {
            try
            {
                return GetCase(caseId).Messages.ToList();
            }
            catch (Exception ex)
            {
                //LOGGER

            }
            return new List<Message>();
        }

        public Customer GetCaseCustomer(int caseId)
        {
            Customer customer = GetCase(caseId).Customer;
            return customer;
        }

        public Employee GetCaseEmployee(int caseId)
        {
            Employee employee = GetCase(caseId).Employee;
            return employee;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
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

        public void Add(int employeeId, string description, string decision, int customerId, bool isEnded = false)
        {
            Case newCase = new Case()
            {
                EmployeeId = employeeId,
                Description = description,
                Decision = decision,
                IsEnded = isEnded,
                CustomerId = customerId,
                Customer = GetCustomer(customerId),
                Employee = GetEmployee(employeeId)
            };

            _dbContext.Cases.Add(newCase);
            _dbContext.SaveChanges();
        }

        public Case GetCase(int caseId)
        {
                return _dbContext.Cases.FirstOrDefault(cas => cas.Id == caseId);
        }
    }
}
