using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public abstract class CRUDException : SystemException
    {
        public CRUDException(string message) : base(message)
        { 
        }

        public abstract HttpStatusCode GetStatusCode();
        public abstract IList<string> GetErrorMessages();
    }
}
