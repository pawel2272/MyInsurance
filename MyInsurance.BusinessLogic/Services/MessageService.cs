using MyInsurance.BusinessLogic.Constants;
using MyInsurance.BusinessLogic.Data;
using MyInsurance.BusinessLogic.Services.Base;
using MyInsurance.BusinessLogic.Services.ServiceInterfaces;
using System;
using System.Linq;

namespace MyInsurance.BusinessLogic.Services
{
    /// <summary>
    /// serwis obsługujący tabelę Message
    /// </summary>
    public class MessageService : CommonDbService, IMessageService
    {
        private readonly CryptoService crypto;

        /// <summary>
        /// Konstruktor inicjalizujący połączenie z bazą
        /// </summary>
        public MessageService() : base()
        {
            crypto = new CryptoService(CryptoConstants.MESSAGE_KEY);
        }

        public MessageService(InsuranceDBEntities dbContext) : base(dbContext)
        {
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
            _dbContext.SaveChanges();
        }

        public Message GetMessage(int messageId)
        {
            return _dbContext.Messages.FirstOrDefault(m => m.Id == messageId);
        }

        public Case GetMessageCase(int messageId)
        {
            return GetMessage(messageId).Case;
        }

        public bool RemoveMessage(int messageId)
        {
            try
            {
                return this.RemoveMessage(this._dbContext.Messages.First(m => m.Id == messageId));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveMessage(Message message)
        {
            try
            {
                this._dbContext.Messages.Remove(message);
            }
            catch (Exception e)
            {
                return false;
            }
            this._dbContext.SaveChanges();
            return true;
        }
    }
}
