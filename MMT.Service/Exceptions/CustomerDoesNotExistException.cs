using System;
using System.Collections.Generic;
using System.Text;

namespace MMT.Service.Exceptions
{
    public class CustomerDoesNotExistException : Exception
    {
        public CustomerDoesNotExistException() : base() { }

        public CustomerDoesNotExistException(string message) : base(message) { }

        public CustomerDoesNotExistException(string message, Exception inner) : base(message, inner) { }
    }
}
