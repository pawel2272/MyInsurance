﻿using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IEmployeeService
    {
        InsuranceDBEntities DBContext { get; }
        void Add(string username, string password, string email, string firstName, string lastName, DateTime birthDate, bool isBoos, bool isAdmin, decimal salary);
        void Add(Employee employee);
        Employee GetEmployee(int employeeId);
        Employee GetEmployee(string username);
        List<Employee> GetAllEmployees();
        List<Case> GetEmployeeCases(int employeeId);
        List<Policy> GetEmployeePolicies(int employeeId);
        bool CheckIfExists(string username);
        bool CheckIfExists(int employeeId);
        bool RemoveEmployee(int employeeId);
        bool RemoveEmployee(Employee employee);
    }
}
