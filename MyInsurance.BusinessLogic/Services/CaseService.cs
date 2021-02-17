using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Base;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    /// <summary>
    /// serwis obsługujący tabelę Case
    /// </summary>
    public class CaseService : CommonDbService, ICaseService
    {
        private readonly CryptoService crypto;
        /// <summary>
        /// Konstruktor inicjalizujący połączenie z bazą
        /// </summary>
        public CaseService() : base()
        {
            crypto = new CryptoService(CryptoConstants.MESSAGE_KEY);
        }

        public CaseService(InsuranceDBEntities dbContext) : base(dbContext)
        {
            crypto = new CryptoService(CryptoConstants.MESSAGE_KEY);
        }

        /// <summary>
        /// zwraca wszystkie zgłoszenia danego klienta lub pustą listę
        /// </summary>
        /// <param name="customerId">id klienta</param>
        /// <returns>lista zgłoszeń klienta</returns>
        public List<Case> GetAllCases(int customerId)
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => cas.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER
            }
            return new List<Case>();
        }

        /// <summary>
        /// zwraca wszystkie otwarte zgłoszenia danego klienta lub pustą listę
        /// </summary>
        /// <returns>lista zgłoszeń klienta</returns>
        public List<Case> GetOpenedCases()
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => !cas.IsEnded)
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER
            }
            return new List<Case>();
        }

        /// <summary>
        /// zwraca wszystkie zamknięte zgłoszenia danego klienta lub pustą listę
        /// </summary>
        /// <returns>lista zgłoszeń klienta</returns>
        public List<Case> GetClosedCases()
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => cas.IsEnded)
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER
            }
            return new List<Case>();
        }

        /// <summary>
        /// zwraca wszystkie wiadomości dla danego zgłoszenia lub pustą listę
        /// </summary>
        /// <param name="caseId">id zgłoszenia</param>
        /// <returns>lista wiadomości</returns>
        public List<Message> GetCaseMessages(int caseId)
        {
            try
            {
                Console.WriteLine("Id sprawy: " + caseId);
                Case casee = this.GetCase(caseId);
                if (casee != null)
                    return this.GetCase(caseId).Messages.Select(m => new Message()
                    {
                        CaseId = m.CaseId,
                        Text = this.crypto.Decrypt(m.Text),
                        Case = _dbContext.Cases.FirstOrDefault(c => c.Id == caseId),
                        IsFromAgent = m.IsFromAgent,
                        SendingDate = m.SendingDate,
                        EmployeeId = m.EmployeeId,
                        CustomerId = m.CustomerId,
                        Employee = m.Employee,
                        Customer = m.Customer
                    }).ToList();
            }
            catch (Exception ex)
            {
                //LOGGER

            }
            return new List<Message>();
        }

        /// <summary>
        /// zwraca klienta dla danego zgłoszenia lub null
        /// </summary>
        /// <param name="caseId">id zgłoszenia</param>
        /// <returns>klient dla danego zgłoszenia</returns>
        public Customer GetCaseCustomer(int caseId)
        {
            try
            {
                Customer customer = GetCase(caseId).Customer;
                return customer;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// zwraca pracownika zajmującego się daną sprawą
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        public Employee GetCaseEmployee(int caseId)
        {
            try
            {
                Employee employee = GetCase(caseId).Employee;
                return employee;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// zwraca klienta o podanym id lub null
        /// </summary>
        /// <param name="customerId">id kliena</param>
        /// <returns>klient</returns>
        private Customer GetCustomer(int customerId)
        {
            using (CustomerService customerService = new CustomerService())
            {
                return customerService.GetCustomer(customerId);
            }
        }

        /// <summary>
        /// zwraca pracownika o podanym id lub null
        /// </summary>
        /// <param name="employeeId">id pracownika</param>
        /// <returns>pracownik</returns>
        private Employee GetEmployee(int employeeId)
        {
            using (EmployeeService employeeService = new EmployeeService())
            {
                return employeeService.GetEmployee(employeeId);
            }
        }

        /// <summary>
        /// dodaje nowe zgłoszenie
        /// </summary>
        /// <param name="employeeId">id pracownika</param>
        /// <param name="description">opis</param>
        /// <param name="decision">decyzja</param>
        /// <param name="customerId">id klienta</param>
        /// <param name="isEnded">status - czy jest zakończone</param>
        public void Add(int employeeId, string description, string decision, int customerId, bool isEnded = false)
        {
            Case newCase = new Case()
            {
                EmployeeId = employeeId,
                Description = description,
                Decision = decision,
                IsEnded = isEnded,
                CustomerId = customerId,
                Customer = GetCustomer(customerId),
                Employee = GetEmployee(employeeId)
            };

            _dbContext.Cases.Add(newCase);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// zwraca zgłoszenie o podanym id
        /// </summary>
        /// <param name="caseId">id zgłoszenia</param>
        /// <returns>zgłoszenie</returns>
        public Case GetCase(int caseId)
        {
                return _dbContext.Cases.FirstOrDefault(cas => cas.Id == caseId);
        }

        public List<Case> GetAllCases(int agentId, string agentFirstName)
        {
            try
            {
                return _dbContext.Cases
                    .Where(cas => cas.EmployeeId == agentId && cas.Employee.FirstName.Equals(agentFirstName))
                    .ToList();
            }
            catch (Exception ex)
            {
                //LOGGER
            }
            return new List<Case>();
        }

        public bool RemoveCase(int caseId)
        {
            try
            {
                return this.RemoveCase(this._dbContext.Cases.First(c => c.Id == caseId));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveCase(Case casee)
        {
            try
            {
                this._dbContext.Cases.Remove(casee);
            }
            catch (Exception e)
            {
                return false;
            }
            this._dbContext.SaveChanges();
            return true;
        }

        public void Add(Case casee)
        {
            _dbContext.Cases.Add(casee);
            _dbContext.SaveChanges();
        }
    }
}
