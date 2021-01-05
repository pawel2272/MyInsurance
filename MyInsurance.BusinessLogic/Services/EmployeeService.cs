using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Interfaces;
using MyInsurance.BusinessLogic.Services.Exceptions;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    /// <summary>
    /// serwis obsługujący tabelę Employee
    /// </summary>
    public class EmployeeService : IEmployeeService, IPerson
    {
        /// <summary>
        /// połączenie z bazą danych
        /// </summary>
        private readonly InsuranceDBEntities _dbContext;

        /// <summary>
        /// Konstruktor inicjalizujący połączenie z bazą
        /// </summary>
        public EmployeeService()
        {
            _dbContext = new InsuranceDBEntities();
        }

        public void Add(string username, string password, string email, string firstName, string lastName, DateTime birthDate, bool isBoos, bool isAdmin, decimal salary)
        {
            if (!this.CheckIfExists(username))
            {
                string passEncrypted;
                using (CryptoService crypto = new CryptoService(CryptoConstants.USER_KEY))
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

        public void Add(Employee employee)
        {
            using (CryptoService crypto = new CryptoService(CryptoConstants.USER_KEY))
            {
                employee.Password = crypto.Encrypt(employee.Password);
            }
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
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
            if (GetEmployee(employeeId) == null)
                return false;
            else
                return true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);
        }

        public Employee GetEmployee(string username)
        {
            return _dbContext.Employees.FirstOrDefault(e => e.Login == username);
        }

        public List<Case> GetEmployeeCases(int employeeId)
        {
            try
            {
                Employee employee = _dbContext.Employees.First(emp => emp.Id == employeeId);
                return employee.Cases
                               .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER
            }
            return new List<Case>();
        }

        public List<Policy> GetEmployeePolicies(int employeeId)
        {
            try
            {
                Employee employee = _dbContext.Employees.First(emp => emp.Id == employeeId);
                return employee.Policies
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
            return GetEmployee(username);
        }
    }
}
