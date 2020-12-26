using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Dto;
using MyInsurance.BusinessLogic.Services.Exceptions;
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

        public void Add(string username, string password, string email, string firstName, string lastName, string street, int houseNumber, string city, string zipCode, string companyName, string phoneNumber, string nipNumber = "00000000000", decimal discount = 0)
        {
            if (!this.CheckIfExists(username))
            {
                string passEncrypted;
                using (CryptoService crypto = new CryptoService())
                {
                    passEncrypted = crypto.Encrypt(password);
                }
                Customer customer = new Customer()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Street = street,
                    HouseNumber = houseNumber,
                    City = city,
                    ZipCode = zipCode,
                    CompanyName = companyName,
                    PhoneNumber = phoneNumber,
                    NIPNumber = nipNumber,
                    Login = username,
                    Password = passEncrypted,
                    EmailAddress = email,
                    Discount = discount
                };
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
            }
            else
                throw new EntityAlreadyExistsException("User: " + username + "already exists!");
        }

        public bool CheckIfExists(string username)
        {
            Customer customer = _dbContext.Customers.FirstOrDefault(c => c.Login == username);
            if (customer == null)
                return false;
            else
                return true;
        }

        public bool CheckIfExists(int customerId)
        {
            Customer customer = _dbContext.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
                return false;
            else
                return true;
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
