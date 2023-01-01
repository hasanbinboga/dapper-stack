using Nest;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Infrastructure
{
    [Serializable]
    public class ServiceResult
    {

        [JsonProperty(PropertyName = "resultType")]
        public ResultType ResultType { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        public ServiceResult(string message = "", ResultType state = ResultType.Success)
        {
            ResultType = state;
            Message = message;
        }
    }
    [Serializable]
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public ServiceResult(T result, string message = "", ResultType state = ResultType.Success)
            : base(message, state)
        {
            Data = result;
        }
    }
}
