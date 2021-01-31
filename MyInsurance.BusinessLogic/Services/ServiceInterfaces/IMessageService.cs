using MyInsurance.BusinessLogic.Data;
using System;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IMessageService : IDisposable
    {
        void Add(int caseId, string messageText, bool isFromAgent);
        Message GetMessage(int messageId);
        Case GetMessageCase(int messageId);
        bool RemoveMessage(int messageId);
        bool RemoveMessage(Message message);
    }
}
