using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MMT.Web.Exceptions
{
    public class ErrorDetail
    {
        public ErrorDetail(int statusCode, string message)
        {
            Message = message;
            StatusCode = statusCode;
        }
        public int StatusCode { get; }

        public string Message { get; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
