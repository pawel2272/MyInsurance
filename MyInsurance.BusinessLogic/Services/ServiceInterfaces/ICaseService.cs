using MyInsurance.BusinessLogic.Services.Dto;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface ICaseService
    {
        void Add(CaseDto casee);
        List<CaseDto> GetAllCases(int customerId);
        List<CaseDto> GetOpenedCases();
        List<CaseDto> GetClosedCases();
        List<MessageDto> GetCaseMessages(int caseId);

        CustomerDto GetCaseCustomer(int caseId);
        EmployeeDto GetCaseEmployee(int caseId);
    }
}
