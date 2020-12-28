﻿using MyInsurance.BusinessLogic.Data;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface ICustomerService
    {
        void Add(string username, string password, string email, string firstName, string lastName, string street, int houseNumber, string city, string zipCode, string companyName, string phoneNumber, string nipNumber = "00000000000", decimal discount = 0);
        Customer GetCustomer(int customerId);
        Customer GetCustomer(string username);
        List<Policy> GetCustomerPolicies(int customerId);
        List<Case> GetCustomerCases(int customerId);
        bool CheckIfExists(string username);
        bool CheckIfExists(int customerId);
    }
}
