﻿using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundException : CRUDException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages()
        {
           return [Message];
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
