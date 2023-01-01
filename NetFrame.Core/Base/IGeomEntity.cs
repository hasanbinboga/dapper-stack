using NetTopologySuite.Geometries;

namespace NetFrame.Core
{
    public interface IGeomEntity:IEntity
    {
         string GeomWkt { get; set; }

         Geometry Geometry { get; set; }
    }
}
