namespace NetFrame.Core.Entities
{
    public class CityEntity: BaseGeomEntity
    {
        public short RegionRef { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
