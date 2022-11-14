using Newtonsoft.Json;

namespace NetFrame.Core.Dtos
{
    public class OrderArg
    {
        [JsonProperty(PropertyName = "propertyName")]
        public string PropertyName { get; set; }

        [JsonProperty(PropertyName = "descending")]
        public bool Descending { get; set; }
    }
}
