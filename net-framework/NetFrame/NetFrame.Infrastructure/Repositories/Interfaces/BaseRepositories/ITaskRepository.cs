
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface class where Task data is managed
    /// </summary>
    public interface ITaskRepository : IBaseRepository<TaskEntity>
    {
        Task<TaskEntity> GetTaskDetails(long id);
    }

   
}
