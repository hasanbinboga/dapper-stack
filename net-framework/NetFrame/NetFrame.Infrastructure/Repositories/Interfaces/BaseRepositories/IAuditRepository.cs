using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// AuditRepository interface: Provides repository functions related to data history on an Entity basis
    /// </summary>
    public interface IAuditRepository : IRepository<AuditEntity>
    {
        /// <summary>
        /// Gets the data history of the given entity FROM the database
        /// </summary>
        /// <param name="id">id value of related data in database</param>
        /// <param name="entityType">Entity type</param>
        /// <returns>If there is a data history related to the requested data, it returns a list of changes made. Otherwise, an empty list is returned.</returns>
        Task<List<AuditChange>> GetAudit(long id, Type entityType);
    }
}
