using MyInsurance.BusinessLogic.Services.Dto;
using System.Collections.Generic;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IMessageService
    {
        void Add(MessageDto message);
        CaseDto GetMessageCase(int messageId);
    }
}
