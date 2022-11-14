using Newtonsoft.Json;

namespace NetFrame.Core.Dtos
{ 
    public class BaseListDto:BaseDto
    {


        [JsonProperty(PropertyName = "medyaAdet")]
        public int MedyaAdet { get; set; }

        [JsonProperty(PropertyName = "resimRef")]
        public long ResimRef { get; set; }

        [JsonProperty(PropertyName = "history")]
        public HistoryDto[] History { get; set; }
    }
}
