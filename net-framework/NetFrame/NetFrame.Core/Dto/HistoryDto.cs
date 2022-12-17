using Newtonsoft.Json;

namespace NetFrame.Core.Dtos
{
    public class HistoryDto
    {
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "createTime")]
        public DateTime CreateTime { get; set; }
        [JsonProperty(PropertyName = "transaction")]
        public string Transaction { get; set; } = string.Empty;

        public static HistoryDto[] GetHistory(dynamic[] histories)
        {
            var result = new List<HistoryDto>();
            foreach (var history in histories)
            {
                result.Add(new HistoryDto { CreateTime = history.createtime, UserName = history.username, Transaction = history.transaction });
            }
            return result.ToArray();
        }
    }
}
