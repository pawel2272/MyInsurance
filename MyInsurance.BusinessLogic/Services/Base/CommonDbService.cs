using MyInsurance.BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Services.Base
{
    public abstract class CommonDbService : IDisposable
    {
        /// <summary>
        /// połączenie z bazą danych
        /// </summary>
        protected readonly InsuranceDBEntities _dbContext;
        protected readonly bool needToBeDisposed;
        public InsuranceDBEntities DBContext
        {
            get
            {
                return this._dbContext;
            }
        }

        /// <summary>
        /// Konstruktor inicjalizujący połączenie z bazą
        /// </summary>
        public CommonDbService()
        {
            _dbContext = new InsuranceDBEntities();
            needToBeDisposed = true;
        }

        /// <summary>
        /// Konstruktor inicjalizujący połączenie z bazą
        /// </summary>
        /// <param name="dbContext">Obiekt dbcontext zapewniający połączenie z bazą danych</param>
        public CommonDbService(InsuranceDBEntities dbContext)
        {
            this._dbContext = dbContext;
            needToBeDisposed = false;
        }

        /// <summary>
        /// implementacja interfejsu IDisposable
        /// </summary>
        public void Dispose()
        {
            if (this.needToBeDisposed)
                this._dbContext.Dispose();
        }
    }
}
