using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class TaskDataAccess : EntityDataAccess<TaskEntity>, ITaskDataAccess
    {
        public TaskDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new TaskEntityValidator())
        {
        }
    }
}
