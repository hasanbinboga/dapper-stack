using Dapper;
using NetFrame.Common.Exception;
using NetFrame.Core.Entities;
using NetFrame.Infrastructure.Repositories.Interfaces.LogRepositories;

namespace NetFrame.Infrastructure.Repositories
{
    public class ErrorLogRepository : LogRepository<ErrorLogEntity>, IErrorLogRepository
    {
        /// <summary>
        /// Info Log repository sınıfının oluşturucu fonksiyonu
        /// </summary>
        /// <param name="unitOfWork">InfoLog verisinin bulunduğu context örneği (instance)</param>
        public ErrorLogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public override List<long> Add(IEnumerable<ErrorLogEntity> entities)
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
        public override long Add(ErrorLogEntity entity)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.UserName == null)
                throw new ArgumentNullException("entity.UserName");

            try
            {
                entity.Id = UnitOfWork.Connection.ExecuteScalar<long>(
                    "INSERT INTO errorlog(id, serviceuri, controller, method, requesturi, requestjsonobject, requestxmlobject, statuscode, responsejsonobject, responsexmlobject, exception, username, useripaddress) VALUES (DEFAULT, @ServiceUri, @Controller, @Method, @RequestUri, @RequestJsonObject::jsonb, @RequestXmlObject::xml, @StatusCode, @ResponseJsonObject::jsonb, @ResponseXmlObject::xml, @Exception, @UserName, @UserIpAddress::inet) RETURNING id;",
                    param: entity,
                    transaction: UnitOfWork.Transaction);

                return entity.Id;
            }
            catch (Exception ex)
            {

                throw new DataAccessException("ErrorLog kaydı eklenemedi", new BaseException("", ex));
            }
        }
    }
}
