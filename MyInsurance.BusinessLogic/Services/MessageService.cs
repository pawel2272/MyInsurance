using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    public class MessageService : IMessageService, IDisposable
    {
        private readonly InsuranceDBEntities _dbContext;
        private readonly CryptoService crypto;

        public MessageService()
        {
            _dbContext = new InsuranceDBEntities();
            crypto = new CryptoService(CryptoConstants.ENCRYPTION_KEYS["message"]);
        }

        public void Add(int caseId, string messageText)
        {
            Message message = new Message()
            {
                CaseId = caseId,
                Text = crypto.Encrypt(messageText),
                Case = _dbContext.Cases.FirstOrDefault(c => c.Id == caseId),
                SendingDate = DateTime.Now
            };
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            crypto.Dispose();
        }

        public Message GetMessage(int messageId)
        {
            return _dbContext.Messages.FirstOrDefault(m => m.Id == messageId);
        }

        public Case GetMessageCase(int messageId)
        {
            return GetMessage(messageId).Case;
        }
    }
}
