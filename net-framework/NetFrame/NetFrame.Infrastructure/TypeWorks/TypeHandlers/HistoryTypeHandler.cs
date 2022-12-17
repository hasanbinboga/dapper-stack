using System.Collections.Generic;
using System.Data;
using Dapper;
using Newtonsoft.Json;

namespace NetFrame.Infrasturcture.TypeWorks.TypeHandlers
{
    /// <summary>
    /// Postgresql jsonb veri tipindeki History değerlerini dynamic dizisine parse eder.
    /// </summary>
    public class HistoryTypeHandler : SqlMapper.TypeHandler<dynamic[]>
    {
        public override void SetValue(IDbDataParameter parameter, dynamic[] value)
        {
            parameter.Value = value;
        }

        public override dynamic[] Parse(object value)
        {
            if (value is string && string.IsNullOrEmpty(value.ToString()) == false)
            {
                var histories = JsonConvert.DeserializeObject<List<dynamic>>(value.ToString()!)!.ToArray();
                return histories;
            }
            return null!;
        }
    }
}
