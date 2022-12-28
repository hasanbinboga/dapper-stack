using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class NotificationDataAccess : EntityDataAccess<NotificationEntity>, INotificationDataAccess
    {
        public NotificationDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new NotificationEntityValidator())
        {
        }
    }
}
