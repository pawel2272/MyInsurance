using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface ICaseService : IDisposable
    {
        void Add(int employeeId, string description, string decision, int customerId, bool isEnded = false);
        Case GetCase(int caseId);
        List<Case> GetAllCases(int customerId);
        List<Case> GetOpenedCases();
        List<Case> GetClosedCases();
        List<Message> GetCaseMessages(int caseId);

        Customer GetCaseCustomer(int caseId);
        Employee GetCaseEmployee(int caseId);
    }
}
