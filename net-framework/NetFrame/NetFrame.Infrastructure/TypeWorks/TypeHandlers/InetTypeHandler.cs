using System.Data;
using Dapper;
using NpgsqlTypes;

namespace NetFrame.Infrasturcture.TypeWorks.TypeHandlers
{

    /// <summary>
    /// Postgresql Inet veri tipini string veri tipine parse eder.
    /// </summary>
    public class InetTypeHandler : SqlMapper.TypeHandler<string>
    {

        public override void SetValue(IDbDataParameter parameter, string value)
        {
            parameter.Value = value;
        }

        public override string Parse(object value)
        {
            if (value is NpgsqlInet)
            {
                return ((NpgsqlInet)value).Address.ToString();
            }
            return value.ToString();
        }
    }
}
