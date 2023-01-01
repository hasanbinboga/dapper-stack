using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class CityDataAccess : EntityDataAccess<CityEntity>, ICityDataAccess
    {
        public CityDataAccess(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
