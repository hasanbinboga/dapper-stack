using Dapper;
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Mesaj verilerinin yönetildiği repository interface sınıfı implementasyonu
    /// </summary>
    public class MessageRepository : BaseRepository<MessageEntity>, IMessageRepository
    {
        /// <summary>
        /// Mesaj repository sınıfının oluşturucu fonksiyonu
        /// </summary>
        /// <param name="unitOfWork">MessageEntity verisinin bulunduğu context örneği (instance)</param>
        public MessageRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public override List<long> Add(IEnumerable<MessageEntity> entities)
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
        public override long Add(MessageEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (entity.CreateUserName == null)
                throw new ArgumentNullException("entity.CreatedUserName");

            try
            {
                entity.Id = UnitOfWork.Connection.ExecuteScalar<long>(
                    "INSERT INTO messages(id, title, body, receiverusername, receiveruserfullname, sendtime, senderusername, senderuserfullname, readstatus, readtime, createtime, createusername, createipaddress) values(DEFAULT, @Title, @Body, @ReceiverUserName, @ReceiverUserFullname, @SendTime, @SenderUserName, @SenderUserFullname, @ReadStatus, @ReadTime, @CreateTime, @CreateUserName, @CreateIpAddress::inet) RETURNING id;",
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
        public override void Update(MessageEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            try
            {
                UnitOfWork.Connection.Execute(
                    "update messages set title = @Title, body = @Body, receiverusername = @ReceiverUserName, receiveruserfullname = @ReceiverUserFullname, sendtime = @SendTime, senderusername = @SenderUserName, senderuserfullname = @SenderUserFullname, readstatus = @ReadStatus, readtime = @ReadTime, updatetime=@UpdateTime, updateusername=@UpdateUserName,  updateipaddress=@UpdateIpAddress::inet  where Id = @Id",
                    param: entity,
                    transaction: UnitOfWork.Transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
