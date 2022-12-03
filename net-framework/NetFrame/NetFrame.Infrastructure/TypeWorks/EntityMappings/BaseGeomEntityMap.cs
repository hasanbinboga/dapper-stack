using Dapper.FluentMap.Mapping;
using NetFrame.Core;

namespace NetFrame.Infrasturcture.TypeWorks.EntityMappings
{
    public class BaseGeomEntityMap : EntityMap<BaseGeomEntity>
    {
        public BaseGeomEntityMap()
        {
            Map(s => s.Geometry).Ignore();
        }
    }
}
