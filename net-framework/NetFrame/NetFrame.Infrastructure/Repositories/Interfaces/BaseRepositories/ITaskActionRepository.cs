using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface class where Task data is managed
    /// </summary>
    public interface ITaskActionRepository : IBaseRepository<TaskActionEntity>
    {
        List<TaskActionEntity> GetAllByTaskId(long taskId);

    }
}
