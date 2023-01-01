using Newtonsoft.Json;

namespace NetFrame.Core.Dtos
{
    public class OptionDto
    {
        [JsonProperty(PropertyName = "label")] 
        public string Label { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "value")] 
        public string Value { get; set; } = string.Empty; 
    }
}
