using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Services.Exceptions
{
    public class EntityDoesNotExistsException : Exception
    {
        public EntityDoesNotExistsException() : base() { }
        public EntityDoesNotExistsException(string message) : base(message) { }
        public EntityDoesNotExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
