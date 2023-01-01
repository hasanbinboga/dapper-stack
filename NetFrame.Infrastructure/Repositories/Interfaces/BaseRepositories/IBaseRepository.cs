using NetFrame.Core;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Public repository interface class for Entities with no private repository defined. Provides basic CRUD operations. Also, unlike IRepository, it brings the Audit records of the related Entity.
    /// </summary>
    /// <typeparam name="T"> Entity type</typeparam>
    public interface IBaseRepository<T> : IRepository<T> where T: BaseEntity 
    {
        
    }
}
