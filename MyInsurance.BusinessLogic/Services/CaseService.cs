using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Dto;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    public class CaseService : ICaseService, IDisposable
    {
        private readonly InsuranceDBEntities _dbContext;

        public CaseService()
        {
            _dbContext = new InsuranceDBEntities();
        }

        public List<CaseDto> GetAllCases(int customerId)
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => cas.CustomerId == customerId)
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

        public List<CaseDto> GetOpenedCases()
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => cas.IsEnded == false)
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

        public List<CaseDto> GetClosedCases()
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => cas.IsEnded == true)
                    .Select(cas => new CaseDto()
                    {
                        Id = cas.Id,
                        EmployeeId = cas.EmployeeId,
                        Description = cas.Description,
                        Decision = cas.Decision,
                        IsEnded = cas.IsEnded,
                        CustomerId = cas.CustomerId
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER

            }
            return new List<CaseDto>();
        }

        public List<MessageDto> GetCaseMessages(int caseId)
        {
            try
            {
                Case cas = _dbContext.Cases.First(Case => Case.Id == caseId);
                return cas.Messages
                    .Select(mess => new MessageDto()
                    {
                        Id = mess.Id,
                        CaseId = mess.CaseId,
                        Text = mess.Text,
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER

            }
            return new List<MessageDto>();
        }

        public CustomerDto GetCaseCustomer(int caseId)
        {
            try
            {
                Customer customer = _dbContext.Cases.First(cas => cas.Id == caseId).Customer;
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
                //LOGGER
            }
            return null;
        }

        public EmployeeDto GetCaseEmployee(int caseId)
        {
            try
            {
                Employee employee = _dbContext.Cases.First(cas => cas.Id == caseId).Employee;
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
                    BirthDate = new DateTime(employee.BirthDate.Year, employee.BirthDate.Month, employee.BirthDate.Year),
                    EmailAddress = employee.EmailAddress,
                    Login = employee.Login,
                    Password = employee.Password,
                    IsAdmin = employee.IsAdmin,
                    IsBoss = employee.IsBoss,
                    PhoneNumber = employee.PhoneNumber,
                };
            }

            catch (Exception ex)
            {
                //LOGGER
            }
            return null;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Add(CaseDto casee)
        {
            throw new NotImplementedException();
        }
    }
}
