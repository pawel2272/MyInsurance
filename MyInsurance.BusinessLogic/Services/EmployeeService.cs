﻿using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Interfaces;
using MyInsurance.BusinessLogic.Services.Base;
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
    public class EmployeeService : CommonDbService, IEmployeeService, IPerson
    {
        /// <summary>
        /// Konstruktor inicjalizujący połączenie z bazą
        /// </summary>
        public EmployeeService() : base()
        {
        }

        public EmployeeService(InsuranceDBEntities dbContext) : base(dbContext)
        {
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

        public List<Employee> GetAllEmployees()
        {
            return _dbContext.Employees.ToList();
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

        public bool RemoveEmployee(int employeeId)
        {
            try
            {
                return this.RemoveEmployee(this._dbContext.Employees.First(e => e.Id == employeeId));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveEmployee(Employee employee)
        {
            try
            {
                this._dbContext.Employees.Remove(employee);
            }
            catch (Exception e)
            {
                return false;
            }
            this._dbContext.SaveChanges();
            return true;
        }
    }
}
