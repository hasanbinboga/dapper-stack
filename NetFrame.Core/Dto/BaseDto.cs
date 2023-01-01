using Newtonsoft.Json;

namespace NetFrame.Core.Dtos
{
    public class BaseDto
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
    }
}
