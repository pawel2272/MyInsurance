using MyInsurance.BusinessLogic.Services.Dto;
using System;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IEmployeeService
    {
        void Add(string username, string password, string email, string firstName, string lastName, DateTime birthDate, bool isBoos, bool isAdmin, decimal salary);
        EmployeeDto GetEmployee(int customerId);
        EmployeeDto GetEmployee(string username);
        List<CaseDto> GetEmployeeCases(int employeeId);
        List<PolicyDto> GetEmployeePolicies(int employeeId);
        bool CheckIfExists(string username);
        bool CheckIfExists(int employeeId);
    }
}
