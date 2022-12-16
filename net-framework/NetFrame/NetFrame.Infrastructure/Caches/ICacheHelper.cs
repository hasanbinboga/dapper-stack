using NetFrame.Core.Dtos;

namespace NetFrame.Infrastructure.Caches
{
    public interface ICacheHelper
    {
        Task SetCommonCacheItems();
        Task RemoveCommonCacheItems();
       
        Task<List<OptionDto>> Regions { get; }
        Task<List<OptionDto>> Cities { get; }

    }
}
