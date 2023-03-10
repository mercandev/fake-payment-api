using System;
using Newtonsoft.Json;
using System.Net;

namespace Fba.Api.SharedObject
{
    [Serializable]
    public class ReturnState<T>
    {
        public ReturnState(object result)
        {
            Status = !string.IsNullOrWhiteSpace(ErrorMessage)
                ? HttpStatusCode.NotAcceptable : HttpStatusCode.OK;

            Data = (T?)result;
        }

        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

