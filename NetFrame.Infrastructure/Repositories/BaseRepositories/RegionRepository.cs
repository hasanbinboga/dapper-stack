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
           return await Task.Run(() => { return 0; });
        }

        public override async Task<List<long>> Add(IEnumerable<RegionEntity> entities)
        {
            return await Task.Run(() => { return new List<long>(); });
        }

        public override async Task Update(RegionEntity entity)
        {
            await Task.Run(() => {   });
        } 
       
        
        public new List<AuditChange> GetAudit(long id)
        {
            return null!;
        }

        public OptionEntity[] GetList()
        {
            return UnitOfWork.Connection.Query<OptionEntity>(
             "SELECT ad as label, id as value FROM region WHERE NOT isdeleted ORDER BY id",
             transaction: UnitOfWork.Transaction).ToArray();
        }
    }
}
