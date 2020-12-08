using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Dto;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    public class CustomerService : ICustomerService, IDisposable
    {
        private readonly InsuranceDBEntities _dbContext;

        public CustomerService()
        {
            _dbContext = new InsuranceDBEntities();
        }

        public void Add(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public CustomerDto GetCustomer(int customerId)
        {
            try
            {
                Customer customer = _dbContext.Customers.First(cust => cust.Id == customerId);
                return new CustomerDto()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Street = customer.Street,
                    HouseNumber = customer.HouseNumber,
                    City = customer.City,
                    ZipCode = customer.ZipCode,
                    CompanyName = customer.CompanyName,
                    PhoneNumber = customer.PhoneNumber,
                    NIPNumber = customer.NIPNumber,
                    Login = customer.Login,
                    Password = customer.Password,
                    EmailAddress = customer.EmailAddress,
                    Discount = customer.Discount
                };
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public List<CaseDto> GetCustomerCases(int customerId)
        {
            try
            {
                Customer customer = _dbContext.Customers.First(cust => cust.Id == customerId);
                return customer.Cases
                    .Select(cas => new CaseDto()
                    {
                        Id = cas.Id,
                        EmployeeId = cas.EmployeeId,
                        Description = cas.Description,
                        Decision = cas.Decision,
                        IsEnded = cas.IsEnded,
                        CustomerId = cas.CustomerId,
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER

            }
            return new List<CaseDto>();
        }

        public List<PolicyDto> GetCustomerPolicies(int customerId)
        {
            try
            {
                Customer customer = _dbContext.Customers.First(cust => cust.Id == customerId);
                return customer.Policies
                    .Select(pol => new PolicyDto()
                    {
                        Id = pol.Id,
                        CustomerId = pol.CustomerId,
                        EmployeeId = pol.EmployeeId,
                        Amount = pol.Amount,
                        Type = pol.Type,
                        Name = pol.Name,
                        DateOfEnding = new DateTime(pol.DateOfEnding.Year, pol.DateOfEnding.Month, pol.DateOfEnding.Year)
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER

            }
            return new List<PolicyDto>();
        }
    }
}
