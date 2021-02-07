using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface ICaseService
    {
        InsuranceDBEntities DBContext { get; }
        void Add(Case casee);
        void Add(int employeeId, string description, string decision, int customerId, bool isEnded = false);
        Case GetCase(int caseId);
        List<Case> GetAllCases(int customerId);
        List<Case> GetAllCases(int agentId, string agentFirstName);
        List<Case> GetOpenedCases();
        List<Case> GetClosedCases();
        List<Message> GetCaseMessages(int caseId);

        Customer GetCaseCustomer(int caseId);
        Employee GetCaseEmployee(int caseId);
        bool RemoveCase(int caseId);
        bool RemoveCase(Case casee);
    }
}
