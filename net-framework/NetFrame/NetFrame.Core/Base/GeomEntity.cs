using System.ComponentModel.DataAnnotations.Schema;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace NetFrame.Core
{
    public class GeomEntity : Entity, IGeomEntity
    {
        private string _geomWkt;

        /// <summary>
        /// Wkt information of spatial information The geometry field selected FROM the database should be wkt. (SELECT st_astext(geom) FROM ..)
        /// </summary>
        [Column("geomwkt")]
        public string GeomWkt
        {
            get { return _geomWkt; }
            set
            {
                if (_geomWkt != value)
                {
                    _geomWkt = value;
                    if (!string.IsNullOrEmpty(value))
                    {
                        var pm = new PrecisionModel(PrecisionModels.Floating);
                        var factory = new GeometryFactory(pm, 4326);
                        var reader = new WKTReader(factory);
                        _geometry = (Geometry)reader.Read(value);
                    }
                    else
                    {
                        _geometry = null;
                    }
                }

            }

        }

        private Geometry _geometry;


        /// <summary>
        /// Geometry object of spatial information. It was created to be used in operations with geometry in the application layer.
        /// </summary>
        [Column("geometry")]
        public Geometry Geometry
        {
            get { return _geometry; }
            set
            {
                if (!_geometry.EqualsExact(value))
                {
                    _geomWkt = value.ToString();
                    _geometry = value;
                }

            }
        }
    }
}
