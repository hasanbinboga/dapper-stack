using NetFrame.Core;
using NetFrame.Core.Entities.Validators;


namespace NetFrame.Infrastructure.DataAcces
{
    public interface IBaseEntityDataAccess<T> where T : BaseEntity 
    {
        public UnitOfWork UnitOfWork { get; }

        public BaseEntityValidator<T> Validator { get; }
        public BaseEntityValidator<T> UpdateValidator { get; }

        Task<T> GetEntityById(long id);

        Task Update(T value);

        Task<long> Add(T value);

        Task Passive(long id, string userName, string ipAddress);
        Task Passive(long[] idArray, string userName, string ipAddress);
    }
}
