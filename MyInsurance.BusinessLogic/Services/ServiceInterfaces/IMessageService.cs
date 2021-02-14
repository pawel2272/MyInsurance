using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IMessageService
    {
        InsuranceDBEntities DBContext { get; }
        void Add(int caseId, string messageText, bool isFromAgent);
        Message GetMessage(int messageId);
        Case GetMessageCase(int messageId);
        bool RemoveMessage(int messageId);
        bool RemoveMessage(Message message);
        List<Employee> GetConversationEmployees(int customerId);
        List<Customer> GetConversationCustomers(int employeeId);
        List<Message> GetCustomerConversation(int customerId);
        List<Message> GetEmployeeConversation(int employeeId);
    }
}
