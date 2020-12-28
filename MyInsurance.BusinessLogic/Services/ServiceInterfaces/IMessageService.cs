using MyInsurance.BusinessLogic.Data;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IMessageService
    {
        void Add(int caseId, string messageText);
        Message GetMessage(int messageId);
        Case GetMessageCase(int messageId);
    }
}
