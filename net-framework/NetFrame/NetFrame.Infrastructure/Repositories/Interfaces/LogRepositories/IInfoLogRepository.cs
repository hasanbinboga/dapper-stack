using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories.Interfaces.LogRepositories
{
    /// <summary>
    /// Repository interface where the request information coming to the application is managed
    /// </summary>
    public interface IInfoLogRepository: ILogRepository<InfoLogEntity>
    {
    }
}
