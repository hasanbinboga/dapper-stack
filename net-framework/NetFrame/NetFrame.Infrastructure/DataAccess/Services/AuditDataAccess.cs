using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.DataAcces
{
    public class AuditDataAccess :  EntityDataAccess<AuditEntity>, IAuditDataAccess
    {
        public AuditDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new AuditEntityValidator())
        {
        } 
    }
}
