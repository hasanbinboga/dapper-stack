using Dapper;
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Hatırlatma (NotificationEntity) verilerinin yönetildiği repository interface sınıfı implementasyonu
    /// </summary>
    public class NotificationRepository : BaseRepository<NotificationEntity>, INotificationRepository
    {
        /// <summary>
        /// Hatırlatma (notification) repository sınıfının oluşturucu fonksiyonu
        /// </summary>
        /// <param name="unitOfWork">NotificationEntity verisinin bulunduğu context örneği (instance)</param>
        public NotificationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public override async Task<List<long>> Add(IEnumerable<NotificationEntity> entities)
        {
            var rslt = new List<long>();
            foreach (var item in entities)
            {
                rslt.Add(await Add(item));
            }
            return rslt;
        }

        /// <summary>
        /// It provides the registration operations of the given single entity to the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        public override async Task<long> Add(NotificationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (entity.CreateUserName == null)
                throw new ArgumentNullException("entity.CreatedUserName");


            entity.Id = await UnitOfWork.Connection.ExecuteScalarAsync<long>(
                "INSERT INTO notifications(id, title, body, receiverusername, receiveruserfullname, sendtime, senderusername, senderuserfullname, readstatus, createtime, createusername, createipaddress) values(DEFAULT, @Title, @Body, @ReceiverUserName, @ReceiverUserFullname, @SendTime, @SenderUserName, @SenderUserFullname, @ReadStatus,  @CreateTime, @CreateUserName, @CreateIpAddress::inet) RETURNING id;",
                param: entity,
                transaction: UnitOfWork.Transaction);

            return entity.Id;

        }

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity">Updated version of the data requested to be updated in the database </param>
        public override async Task Update(NotificationEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");


            await UnitOfWork.Connection.ExecuteAsync(
                "UPDATE notifications SET title = @Title, body = @Body, receiverusername = @ReceiverUserName, receiveruserfullname = @ReceiverUserFullname, sendtime = @SendTime, senderusername = @SenderUserName, senderuserfullname = @SenderUserFullname, readstatus = @ReadStatus,  updatetime=@UpdateTime, updateusername=@UpdateUserName,  updateipaddress=@UpdateIpAddress::inet  WHERE id = @Id",
                param: entity,
                transaction: UnitOfWork.Transaction);

        }
    }
}
