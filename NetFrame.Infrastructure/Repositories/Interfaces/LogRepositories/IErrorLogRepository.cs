using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories.Interfaces.LogRepositories
{
    /// <summary>
    /// The repository interface, WHERE the request information that comes to the application and creates an error is managed
    /// </summary>
    public interface IErrorLogRepository : ILogRepository<ErrorLogEntity>
    {
    }
}
