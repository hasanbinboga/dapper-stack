using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class CityDataAccess : EntityDataAccess<CityEntity>
    {
        public CityDataAccess(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
