using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Log verilerinin yönetildiği base repository interfacesi
    /// </summary>
    public interface ILogRepository<T> : IRepository<T> where T : LogEntity
    {

    }
}
