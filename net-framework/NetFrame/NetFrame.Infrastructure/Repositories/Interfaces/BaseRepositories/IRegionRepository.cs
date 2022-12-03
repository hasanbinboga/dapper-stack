using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    public interface IRegionRepository: IBaseGeomRepository<RegionEntity>
    {

        OptionEntity[] GetList();

    }
}
