using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IEmployeeService
    {
        void Add(string username, string password, string email, string firstName, string lastName, DateTime birthDate, bool isBoos, bool isAdmin, decimal salary);
        Employee GetEmployee(int employeeId);
        Employee GetEmployee(string username);
        List<Case> GetEmployeeCases(int employeeId);
        List<Policy> GetEmployeePolicies(int employeeId);
        bool CheckIfExists(string username);
        bool CheckIfExists(int employeeId);
    }
}
