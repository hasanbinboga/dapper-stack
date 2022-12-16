using NetFrame.Core.Entities;


namespace NetFrame.Infrastructure.DataAcces
{
    public class InfoLogDataAccess : EntityDataAccess<InfoLogEntity>
    {
        public InfoLogDataAccess(UnitOfWork unitOfWork) : base(unitOfWork, new InfoLogEntityValidator())
        {
        }
    }
}
