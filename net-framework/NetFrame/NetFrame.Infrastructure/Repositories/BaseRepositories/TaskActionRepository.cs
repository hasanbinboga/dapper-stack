using Dapper;
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    public class TaskActionRepository : BaseRepository<TaskActionEntity>, ITaskActionRepository
    {
        /// <summary>
        /// Görev hareketleri sınıfının oluşturucu metodu
        /// </summary>
        /// <param name="unitOfWork">TaskActionAction verisi ile ilgili işlemlerin tannımları</param>
        public TaskActionRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            
        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public override List<long> Add(IEnumerable<TaskActionEntity> entities)
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
        public override long Add(TaskActionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (entity.CreateUserName == null)
                throw new ArgumentNullException("entity.CreatedUserName");

            try
            {
                entity.Id = UnitOfWork.Connection.ExecuteScalar<long>(
                    "INSERT INTO taskactions(id, taskref, actiontime, actiondescription, previousstatus, newstatus, createtime, createusername, createipaddress) values(DEFAULT, @TaskRef, @ActionTime, @ActionDescription, @PreviousStatus, @NewStatus, @CreateTime, @CreateUserName, @CreateIpAddress::inet) RETURNING id;",
                    param: entity,
                    transaction: UnitOfWork.Transaction);

                return entity.Id;
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }
        }

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity">Updated version of the data requested to be updated in the database </param>
        public override void Update(TaskActionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            try
            {
                UnitOfWork.Connection.Execute(
                    "update taskactions set taskref = @TaskRef, actiontime = @ActionTime, actiondescription = @ActionDescription, previousstatus = @PreviousStatus, newstatus = @NewStatus, updatetime=@UpdateTime, updateusername=@UpdateUserName,  updateipaddress=@UpdateIpAddress::inet  where Id = @Id",
                    param: entity,
                    transaction: UnitOfWork.Transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TaskActionEntity> GetAllByTaskId(long taskId)
        {
            var criteria = "taskref = @TaskId";
            var order = "id";
            return GetMany(criteria, new {TaskId = taskId}, order).ToList();
        }
    }
}
