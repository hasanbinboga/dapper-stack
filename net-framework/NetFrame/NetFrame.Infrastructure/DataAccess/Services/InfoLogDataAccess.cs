using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class InfoLogDataAccess : EntityDataAccess<InfoLogEntity>, IInfoLogDataAccess
    {
        public InfoLogDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new InfoLogEntityValidator())
        {
        }
    }
}
