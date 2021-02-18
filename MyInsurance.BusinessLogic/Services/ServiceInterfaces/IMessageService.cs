using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IMessageService
    {
        InsuranceDBEntities DBContext { get; }
        void Add(int caseId, string messageText, bool isFromAgent, int employeeId, int customerId);
        Message GetMessage(int messageId);
        Case GetMessageCase(int messageId);
        bool RemoveMessage(int messageId);
        bool RemoveMessage(Message message);
        List<Message> GetCaseMessages(int caseId);
    }
}
