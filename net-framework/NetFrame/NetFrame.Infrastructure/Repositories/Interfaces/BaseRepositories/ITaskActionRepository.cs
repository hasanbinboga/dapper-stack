using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface class WHERE Task data is managed
    /// </summary>
    public interface ITaskActionRepository : IBaseRepository<TaskActionEntity>
    {
        Task<List<TaskActionEntity>> GetAllByTaskId(long taskId);

    }
}
