using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    public interface ICityRepository: IBaseGeomRepository<CityEntity>
    {
        OptionEntity[] GetList();
        OptionEntity[] GetListByRegionRef(short regionRef);
    }
}
