using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Interfaces;
using MyInsurance.BusinessLogic.Services.Dto;
using MyInsurance.BusinessLogic.Services.Exceptions;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService, IDisposable, IPerson
    {
        private readonly InsuranceDBEntities _dbContext;

        public EmployeeService()
        {
            _dbContext = new InsuranceDBEntities();
        }

        public void Add(string username, string password, string email, string firstName, string lastName, DateTime birthDate, bool isBoos, bool isAdmin, decimal salary)
        {
            if (!this.CheckIfExists(username))
            {
                string passEncrypted;
                using (CryptoService crypto = new CryptoService())
                {
                    passEncrypted = crypto.Encrypt(password);
                }
                Employee employee = new Employee()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Salary = salary,
                    BirthDate = birthDate,
                    EmailAddress = email,
                    Login = username,
                    Password = passEncrypted,
                    IsAdmin = isAdmin,
                    IsBoss = isBoos
                };
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
            }
            else
                throw new EntityAlreadyExistsException("User: " + username + " already exists!");
        }

        public bool CheckIfExists(string username)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(e => e.Login == username);
            if (employee == null)
                return false;
            else
                return true;
        }

        public bool CheckIfExists(int employeeId)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null)
                return false;
            else
                return true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public EmployeeDto GetEmployee(int customerId)
        {
            try
            {
                Employee employee = _dbContext.Employees.First(e => e.Id == customerId);
                return new EmployeeDto()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Street = employee.Street,
                    HouseNumber = employee.HouseNumber,
                    City = employee.City,
                    ZipCode = employee.ZipCode,
                    Salary = employee.Salary,
                    BirthDate = new DateTime(employee.BirthDate.Year, employee.BirthDate.Month, employee.BirthDate.Day),
                    EmailAddress = employee.EmailAddress,
                    Login = employee.Login,
                    Password = employee.Password,
                    IsAdmin = employee.IsAdmin,
                    IsBoss = employee.IsBoss,
                    PhoneNumber = employee.PhoneNumber
                };
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public EmployeeDto GetEmployee(string username)
        {
            try
            {
                Employee employee = _dbContext.Employees.First(e => e.Login == username);
                return new EmployeeDto()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Street = employee.Street,
                    HouseNumber = employee.HouseNumber,
                    City = employee.City,
                    ZipCode = employee.ZipCode,
                    Salary = employee.Salary,
                    BirthDate = new DateTime(employee.BirthDate.Year, employee.BirthDate.Month, employee.BirthDate.Day),
                    EmailAddress = employee.EmailAddress,
                    Login = employee.Login,
                    Password = employee.Password,
                    IsAdmin = employee.IsAdmin,
                    IsBoss = employee.IsBoss,
                    PhoneNumber = employee.PhoneNumber
                };
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public List<CaseDto> GetEmployeeCases(int employeeId)
        {
            try
            {
                Employee employee = _dbContext.Employees.First(emp => emp.Id == employeeId);
                return employee.Cases
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

        public List<PolicyDto> GetEmployeePolicies(int employeeId)
        {
            try
            {
                Employee employee = _dbContext.Employees.First(emp => emp.Id == employeeId);
                return employee.Policies
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

        public ILoginable GetPerson(string username)
        {
            return GetEmployee(username);
        }
    }
}
