using MyInsurance.BusinessLogic.Services.Dto;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IMessageService
    {
        void Add(MessageDto message);
        CaseDto GetMessageCase(int messageId);
    }
}
