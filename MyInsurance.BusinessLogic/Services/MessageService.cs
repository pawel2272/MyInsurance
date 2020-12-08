using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Dto;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;

namespace MyInsurance.BusinessLogic.Services
{
    public class MessageService : IMessageService, IDisposable
    {
        private readonly InsuranceDBEntities _dbContext;

        public MessageService()
        {
            _dbContext = new InsuranceDBEntities();
        }

        public void Add(MessageDto message)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public CaseDto GetMessageCase(int messageId)
        {
            throw new NotImplementedException();
        }
    }
}
