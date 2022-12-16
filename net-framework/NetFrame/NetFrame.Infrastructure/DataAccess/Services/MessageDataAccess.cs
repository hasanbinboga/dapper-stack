using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class MessageDataAccess : EntityDataAccess<MessageEntity>
    {
        public MessageDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new MessageEntityValidator())
        {
        }
    }
}
