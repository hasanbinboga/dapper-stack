using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class TaskActionDataAccess : EntityDataAccess<TaskActionEntity>, ITaskActionDataAccess
    {
        public TaskActionDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new TaskActionEntityValidator())
        {
        }
    }
}
