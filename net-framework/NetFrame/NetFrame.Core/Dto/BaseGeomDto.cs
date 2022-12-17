using Newtonsoft.Json;

namespace NetFrame.Core.Dtos
{
    public class BaseGeomDto
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "geom")]
        public string Geom { get; set; } = string.Empty;



        [JsonProperty(PropertyName = "history")]
        public HistoryDto[] History { get; set; } = new HistoryDto[0];
    }
}
