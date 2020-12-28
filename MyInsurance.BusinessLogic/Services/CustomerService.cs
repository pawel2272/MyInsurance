﻿using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Interfaces;
using MyInsurance.BusinessLogic.Services.Exceptions;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    public class CustomerService : ICustomerService, IDisposable, IPerson
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
                using (CryptoService crypto = new CryptoService(CryptoConstants.ENCRYPTION_KEYS["customer"]))
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
            if (GetCustomer(customerId) == null)
                return false;
            else
                return true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Customer GetCustomer(int customerId)
        {
            return _dbContext.Customers.FirstOrDefault(cust => cust.Id == customerId);
        }

        public Customer GetCustomer(string username)
        {
            return _dbContext.Customers.FirstOrDefault(cust => cust.Login == username);
        }

        public List<Case> GetCustomerCases(int customerId)
        {
            try
            {
                Customer customer = _dbContext.Customers.First(cust => cust.Id == customerId);
                return customer.Cases
                               .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER

            }
            return new List<Case>();
        }

        public List<Policy> GetCustomerPolicies(int customerId)
        {
            try
            {
                Customer customer = _dbContext.Customers.First(cust => cust.Id == customerId);
                return customer.Policies
                               .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER

            }
            return new List<Policy>();
        }

        public ILoginable GetPerson(string username)
        {
            return GetCustomer(username);
        }
    }
}
