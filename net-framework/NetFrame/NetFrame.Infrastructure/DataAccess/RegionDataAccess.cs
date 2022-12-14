using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class RegionDataAccess : EntityDataAccess<CityEntity>
    {
        public RegionDataAccess(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
