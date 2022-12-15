using Dapper;
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    public class CityRepository : BaseGeomRepository<CityEntity>, ICityRepository
    {
        public CityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        } 

        public new List<AuditChange> GetAudit(long id)
        {
            return null;
        }

        public OptionEntity[] GetList()
        {
            return UnitOfWork.Connection.Query<OptionEntity>(
              "SELECT ad as label, id as value FROM city WHERE NOT isdeleted ORDER BY id",
              transaction: UnitOfWork.Transaction).ToArray();
        }

        public OptionEntity[] GetListByRegionRef(short regionRef)
        {
            return UnitOfWork.Connection.Query<OptionEntity>(
               "SELECT ad as label, id as value FROM city WHERE NOT isdeleted AND regionref = @RegionRef ORDER BY id",
               param: new { RegionRef = regionRef },
               transaction: UnitOfWork.Transaction).ToArray();
        }
    }
}
