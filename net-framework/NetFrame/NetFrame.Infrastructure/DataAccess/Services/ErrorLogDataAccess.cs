using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class ErrorLogDataAccess : EntityDataAccess<ErrorLogEntity>, IErrorLogDataAccess
    {
        public ErrorLogDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new ErrorLogEntityValidator())
        {
        }
    }
}
