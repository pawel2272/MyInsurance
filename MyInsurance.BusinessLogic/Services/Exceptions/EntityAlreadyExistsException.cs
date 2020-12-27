using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Services.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException() : base() { }
        public EntityAlreadyExistsException(string message) : base(message) { }
        public EntityAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
