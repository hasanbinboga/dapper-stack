using NetFrame.Core;
using NetFrame.Core.Entities.Validators;

namespace NetFrame.Infrastructure.DataAcces
{
    public interface IBaseGeomDataAccess<T> where T : BaseGeomEntity
    {
        public UnitOfWork UnitOfWork { get; }

        public BaseGeomEntityValidator<T> Validator { get; }
        public BaseGeomEntityValidator<T> UpdateValidator { get; }

        Task<T> GetEntityById(long id);
        Task Update(T value);
        Task Add(T value);

        Task Passive(long id, string userName, string ipAddress);

        Task Passive(long[] idArray, string userName, string ipAddress);
    }
}
