using Newtonsoft.Json;
using NetFrame.Core.Entities;
using NetFrame.Common.Exception;
using Dapper;

// ReSharper disable NotResolvedInText

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// AuditRepository interface implementation provides data history related repository functions on an Entity basis
    /// </summary>
    public class AuditRepository : Repository<AuditEntity>, IAuditRepository
    {
        /// <summary>
        /// Constructor function of data history repository class
        /// </summary>
        /// <param name="unitOfWork">Context instance with audit data</param>
        public AuditRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public override async Task<List<long>> Add(IEnumerable<AuditEntity> entities)
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
        /// <param name="entity">Entity</param>
        public override async Task<long> Add(AuditEntity entity)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.CreateUserName == null)
                throw new ArgumentNullException("entity.CreateUserName");

            try
            {
                entity.Id = await UnitOfWork.Connection.ExecuteScalarAsync<long>(
                    "insert into audit(id, keyfieldid, datamodel, valuebefore, valueafter, changes, actiontype, createtime, createipaddress, createusername, transactionid, applicationname) values(DEFAULT, @KeyFieldId, @DataModel, @ValueBefore::jsonb, @ValueAfter::jsonb, @Changes::jsonb, @ActionType, @CreateTime, @CreateIpAddress::inet, @CreateUserName, @TransactionId, @ApplicationName) returning id;",
                    param: entity,
                    transaction: UnitOfWork.Transaction);

                return entity.Id;
            }
            catch (Exception ex)
            {

                throw new DataAccessException("Audit kaydı eklenemedi", new BaseException("", ex));
            }
        }

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity">Updated version of the data requested to be updated in the database </param>
        public override async Task Update(AuditEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await UnitOfWork.Connection.ExecuteAsync(
                    "UPDATE audit SET keyfieldid = @KeyFieldId, datamodel = @DataModel, valuebefore = @ValueBefore::jsonb, valueafter = @ValueAfter::jsonb, changes = @Changes::jsonb, actiontype= @ActionType, transactionid = @TransactionId, applicationname = @ApplicationName where Id = @Id",
                    param: entity,
                    transaction: UnitOfWork.Transaction);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Audit kaydı güncellenemedi", new BaseException("",ex));
            }
        }


        /// <summary>
        /// Gets the data history of the given entity from the database
        /// </summary>
        /// <param name="id">id value of related data in database</param>
        /// <param name="entityType">Entity type</param>
        /// <returns>If there is a data history related to the requested data, it returns a list of changes made. Otherwise, an empty list is returned. </returns>
        public async Task<List<AuditChange>> GetAudit(long id, Type entityType)
        {
            List<AuditChange> rslt = new List<AuditChange>();
            IRepository<AuditEntity> repository = new Repository<AuditEntity>(UnitOfWork);

            var auditTrail = await repository.GetMany("keyfieldid= @Id AND datamodel=@DataModel", new { Id = id, DataModel = entityType.FullName }, "createdate");

            // we are looking for audit-history of the record selected.

            foreach (var record in auditTrail)
            {
                var change = new AuditChange
                {
                    DateTimeStamp = record.CreateTime.ToString(),
                    AuditActionType = record.ActionType,
                    AuditActionTypeName = Enum.GetName(typeof(AuditActionType), record.ActionType)
                };
                var delta = JsonConvert.DeserializeObject<List<AuditDelta>>(record.Changes);
                change.Changes.AddRange(delta);
                rslt.Add(change);
            }
            return rslt;

        }
         
    }
}
