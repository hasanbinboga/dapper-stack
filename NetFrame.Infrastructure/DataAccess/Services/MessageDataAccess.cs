using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class MessageDataAccess : EntityDataAccess<MessageEntity>, IMessageDataAccess
    {
        public MessageDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new MessageEntityValidator())
        {
        }
    }
}
