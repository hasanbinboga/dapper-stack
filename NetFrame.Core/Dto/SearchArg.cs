using Newtonsoft.Json;

namespace NetFrame.Core.Dtos
{
    public class SearchArg
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "metin")]
        public string Metin { get; set; } = string.Empty;
    }
}