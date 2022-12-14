using Dapper;
using NetFrame.Common.Exception;
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Görev (TaskEntity) verilerinin yönetildiği repository interface sınıfı implementasyonu
    /// </summary>
    public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
    {
        /// <summary>
        /// Görev repository sınıfının oluşturucu fonksiyonu
        /// </summary>
        /// <param name="unitOfWork">TaskEntity verisinin bulunduğu context örneği (instance)</param>
        public TaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public override List<long> Add(IEnumerable<TaskEntity> entities)
        {
            var rslt = new List<long>();
            foreach (var item in entities)
            {
                rslt.Add(Add(item));
            }
            return rslt;
        }

        /// <summary>
        /// It provides the registration operations of the given single entity to the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        public override long Add(TaskEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (entity.CreateUserName == null)
                throw new ArgumentNullException("entity.CreatedUserName");

            try
            {
                entity.Id = UnitOfWork.Connection.ExecuteScalar<long>(
                    "INSERT INTO tasks(id, title, taskdescription, assigneduserid, assigneeuserid, taskstatus, createtime, createusername, createipaddress) values(DEFAULT, @Title, @TaskDescription, @AssignerUserId, @AssigneeUserId, @TaskStatus, @CreateTime, @CreateUserName, @CreateIpAddress::inet) RETURNING id;",
                    param: entity,
                    transaction: UnitOfWork.Transaction);

                if (entity.TaskActions.Count > 0)
                {
                    TaskActionRepository actionsRepo = UnitOfWork.Repositories[typeof(TaskActionEntity)];
                    foreach (var taskAction in entity.TaskActions)
                    {
                        taskAction.TaskRef = entity.Id;
                        actionsRepo.Add(taskAction);
                    }
                }


                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("insert error:", ex);

            }
        }

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity">Updated version of the data requested to be updated in the database </param>
        public override void Update(TaskEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            try
            {
                UnitOfWork.Connection.Execute(
                    "update tasks set title = @Title, taskdescription = @TaskDescription, assigneduserid = @AssignerUserId, assigneeuserid = @AssigneeUserId, taskstatus = @TaskStatus, updatetime=@UpdateTime, updateusername=@UpdateUserName,  updateipaddress=@UpdateIpAddress::inet  where Id = @Id",
                    param: entity,
                    transaction: UnitOfWork.Transaction);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Update error:", ex);
            }
        }

        public TaskEntity GetTaskDetails(long id)
        {
            var result = GetById(id);
            TaskActionRepository actionsRepo = UnitOfWork.Repositories[typeof(TaskActionEntity)];

            result.TaskActions = actionsRepo.GetAllByTaskId(id);

            return result;
        }
    }
}
