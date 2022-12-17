using Newtonsoft.Json;

namespace NetFrame.Core.Dtos
{
    public class OrderArg
    {
        [JsonProperty(PropertyName = "propertyName")]
        public string PropertyName { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "descending")]
        public bool Descending { get; set; }
    }
}
