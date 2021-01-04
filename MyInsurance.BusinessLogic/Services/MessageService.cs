using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    /// <summary>
    /// serwis obsługujący tabelę Message
    /// </summary>
    public class MessageService : IMessageService
    {
        /// <summary>
        /// połączenie z bazą danych
        /// </summary>
        private readonly InsuranceDBEntities _dbContext;
        private readonly CryptoService crypto;

        /// <summary>
        /// Konstruktor inicjalizujący połączenie z bazą
        /// </summary>
        public MessageService()
        {
            _dbContext = new InsuranceDBEntities();
            crypto = new CryptoService(CryptoConstants.MESSAGE_KEY);
        }

        public void Add(int caseId, string messageText, bool isFromAgent)
        {
            Message message = new Message()
            {
                CaseId = caseId,
                Text = crypto.Encrypt(messageText),
                Case = _dbContext.Cases.FirstOrDefault(c => c.Id == caseId),
                IsFromAgent = isFromAgent,
                SendingDate = DateTime.Now
            };
            _dbContext.Messages.Add(message);
            _dbContext.SaveChangesAsync();
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
