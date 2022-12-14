using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class TaskDataAccess : EntityDataAccess<TaskActionEntity>
    {
        public TaskDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new TaskActionEntityValidator())
        {
        }
    }
}
