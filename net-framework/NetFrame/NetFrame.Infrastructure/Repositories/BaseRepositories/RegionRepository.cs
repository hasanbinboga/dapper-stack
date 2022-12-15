using Dapper;
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    public class RegionRepository: BaseGeomRepository<RegionEntity>, IRegionRepository
    {
        public RegionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<long>Add(RegionEntity entity)
        {
            return 0;
        }

        public override async Task<List<long>> Add(IEnumerable<RegionEntity> entities)
        {
            return null;
        }

        public override async Task Update(RegionEntity entity)
        {
        } 
       
        
        public new List<AuditChange> GetAudit(long id)
        {
            return null;
        }

        public OptionEntity[] GetList()
        {
            return UnitOfWork.Connection.Query<OptionEntity>(
             "select ad as label, id as value from region where NOT isdeleted ORDER BY id",
             transaction: UnitOfWork.Transaction).ToArray();
        }
    }
}
