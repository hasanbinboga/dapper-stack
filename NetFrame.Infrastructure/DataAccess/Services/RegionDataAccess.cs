using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class RegionDataAccess : EntityDataAccess<RegionEntity>, IRegionDataAccess
    {
        public RegionDataAccess(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
