using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MMT.Service.Exceptions
{
    public class RequestException : Exception
    {
        public HttpStatusCode statusCode;

        public RequestException()
        {
        }

        public RequestException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.statusCode = statusCode;
        }
    }
}
