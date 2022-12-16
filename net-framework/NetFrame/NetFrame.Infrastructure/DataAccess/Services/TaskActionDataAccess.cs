using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class TaskActionDataAccess : EntityDataAccess<TaskActionEntity>
    {
        public TaskActionDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new TaskActionEntityValidator())
        {
        }
    }
}
