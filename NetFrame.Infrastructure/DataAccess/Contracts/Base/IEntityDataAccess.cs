using FluentValidation;
using NetFrame.Core;


namespace NetFrame.Infrastructure.DataAcces
{
    public interface IEntityDataAccess<T> where T : Entity
    {
        public UnitOfWork UnitOfWork { get; }

        public AbstractValidator<T> Validator { get; }
        
        Task<T> GetEntityById(long id);

        Task Update(T value);

        Task<long> Add(T value);

        Task Passive(long id, string userName, string ipAddress);

        Task Passive(long[] idArray, string userName, string ipAddress);
    }
}
