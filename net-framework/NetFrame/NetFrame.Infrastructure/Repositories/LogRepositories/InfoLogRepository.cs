using Dapper;
using NetFrame.Common.Exception;
using NetFrame.Core.Entities;
using NetFrame.Infrastructure.Repositories.Interfaces.LogRepositories;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// The InfoLog Repository interface implementation provides functions for keeping log records of user operations.
    /// </summary>
    public class InfoLogRepository : LogRepository<InfoLogEntity>, IInfoLogRepository
    {
        /// <summary>
        /// Builder function of Info Log repository class
        /// </summary>
        /// <param name="unitOfWork">Context instance WHERE InfoLog data is located</param>
        public InfoLogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public override async Task<List<long>> Add(IEnumerable<InfoLogEntity> entities)
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
        /// <param name="entity">Entity info</param>
        public override async Task<long> Add(InfoLogEntity entity)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.UserName == null)
                throw new ArgumentNullException("entity.UserName");

            try
            {
                entity.Id = await UnitOfWork.Connection.ExecuteScalarAsync<long>(
                    "INSERT INTO infolog(id, serviceuri, controller, method, requesturi, requestjsonobject, requestxmlobject, statuscode, responsejsonobject, responsexmlobject, username, useripaddress) VALUES (DEFAULT, @ServiceUri, @Controller, @Method, @RequestUri, @RequestJsonObject::jsonb, @RequestXmlObject::xml, @StatusCode, @ResponseJsonObject::jsonb, @ResponseXmlObject::xml, @UserName, @UserIpAddress::inet) RETURNING id;",
                    param: entity,
                    transaction: UnitOfWork.Transaction);

                return entity.Id;
            }
            catch (Exception ex)
            {

                throw new DataAccessException("InfoLog kaydı eklenemedi", new BaseException("", ex));
            }
        }
    }
}
