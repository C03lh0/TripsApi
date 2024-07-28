using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public class ErrorOnValidationException : CRUDException
    {
        private readonly IList<string> _messageErrors;

        public ErrorOnValidationException(IList<string> messageErrors) : base(string.Empty)
        {
            _messageErrors = messageErrors;
        }

        public override IList<string> GetErrorMessages()
        {
            return _messageErrors;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}
