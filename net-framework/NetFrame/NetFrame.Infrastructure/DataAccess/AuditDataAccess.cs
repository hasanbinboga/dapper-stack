using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class AuditDataAccess : EntityDataAccess<AuditEntity>
    {
        public AuditDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new AuditEntityValidator())
        {
        }
    }
}
