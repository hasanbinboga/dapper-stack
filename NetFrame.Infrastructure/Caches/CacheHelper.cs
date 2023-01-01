using Microsoft.Extensions.Caching.Memory;
using NetFrame.Core.Dtos;
using NetFrame.Core.Entities;
using NetFrame.Infrastructure.Repositories;
using StackExchange.Redis;

namespace NetFrame.Infrastructure.Caches
{
    public class CacheHelper : ICacheHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public CacheHelper(IUnitOfWork UnitOfWork, IMemoryCache cache)
        {
            _unitOfWork = UnitOfWork;
            Cache = cache;
        }

        private readonly static Lazy<ConnectionMultiplexer> redisConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions conf = new()
            {
                AbortOnConnectFail = false,
                ConnectTimeout = 100,
            };

            conf.EndPoints.Add("RedisUrl", 6379);

            return ConnectionMultiplexer.Connect(conf.ToString());
        });

        public static ConnectionMultiplexer Connection => redisConnection.Value;

        public IMemoryCache Cache { get; }


        public Task<List<OptionDto>> Regions => GetOrCrateRegions();
        public Task<List<OptionDto>> Cities => GetOrCrateCities();

        private async Task<List<OptionDto>> GetOrCrateRegions()
        {
            var expirationDate = DateTimeOffset.UtcNow.AddYears(1);

            if (!Cache.TryGetValue("Regions", out List<OptionDto>?cities))
            {
                var cityEntities = await ((IRegionRepository)_unitOfWork.Repository<CityEntity>()).GetAll();
                    
                var cityList = cityEntities.Select(a => new OptionDto { Value = a.Id.ToString(), Label = a.Name }).OrderBy(p => p.Label).ToList();

                Cache.Set("Regions", cityList, expirationDate);
                Cache.TryGetValue("Regions", out cities!);
            }
            return cities!;
        }

        private async Task<List<OptionDto>> GetOrCrateCities()
        {
            var expirationDate = DateTimeOffset.UtcNow.AddYears(1);

            if (!Cache.TryGetValue("Cities", out List<OptionDto>? cities))
            {
                var cityEntities = await ((ICityRepository)_unitOfWork.Repository<CityEntity>()).GetAll();

                var cityList = cityEntities.Select(a => new OptionDto { Value = a.Id.ToString(), Label = a.Name }).OrderBy(p => p.Label).ToList();

                Cache.Set("Cities", cityList, expirationDate);
                Cache.TryGetValue("Cities", out cities!);
            }
            return cities!;
        } 
       
        public async Task SetCommonCacheItems()
        {
            await GetOrCrateRegions();
            await GetOrCrateCities();
        }

        public async Task RemoveCommonCacheItems()
        {

            await Task.Run(() => {
                Cache.Remove("Regions");
                Cache.Remove("Cities");
            });
            
        }
    }
}
