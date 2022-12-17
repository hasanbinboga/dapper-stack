using Dapper;
using NetFrame.Common.Utils;
using NetFrame.Core;
using NetFrame.Core.Entities;
using Newtonsoft.Json;
using X.PagedList;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// LogEntity verilerinin yönetildiği repository interface sınıfı implementasyonu
    /// </summary>
    public class LogRepository<T> : ILogRepository<T> where T : LogEntity
    {
        protected readonly IUnitOfWork UnitOfWork;


        /// <summary>
        /// Genel repository sınıfı oluşturma (contructor)
        /// </summary>
        /// <param name="unitOfWork">Context instance information of the entity used in the repository</param> 
        public LogRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        /// <summary>
        /// It provides the registration operations of the given single entity to the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<long> Add(T entity)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            return await Task.Run(long () => { throw new NotImplementedException(); });
        }


        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public virtual async Task<List<long>> Add(IEnumerable<T> entities)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            return await Task.Run(List<long> () => { throw new NotImplementedException(); });
        }

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity">Updated version of the data requested to be updated in the database </param>
        public virtual async Task Update(T entity)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            await Task.Run(() => { throw new NotImplementedException(); });
        }


        /// <summary>
        /// It throws an error because the log records cannot be deleted.
        /// </summary>
        /// <param name="entity">silinecek log kaydı</param>
        public virtual async Task Delete(T entity)
        {
            //No improvements were made as the log records should not be deleted.
            await Task.Run(() => { throw new NotImplementedException(); });
        }

        /// <summary>
        /// It throws an error because the log records cannot be deleted.
        /// </summary>
        /// <param name="id">silinecek log kaydı id si</param>
        public virtual async Task Delete(long id)
        {
            //No improvements were made as the log records should not be deleted.
            await Task.Run(() => { throw new NotImplementedException(); });
        }

        public virtual async Task Delete(IEnumerable<T> entities)
        {
            //No improvements were made as the log records should not be deleted.
            await Task.Run(() => { throw new NotImplementedException(); });
        }

        /// <summary>
        /// It throws an error because the log records cannot be deleted.
        /// </summary>
        /// <param name="idList">silinecek log kayıtlarının id leri</param>
        public virtual async Task Delete(IList<long> idList)
        {
            //No improvements were made as the log records should not be deleted.
            await Task.Run(() => { throw new NotImplementedException(); });
        }

        /// <summary>
        /// It throws an error because log records cannot be disabled.
        /// </summary>
        /// <param name="id">pasife alınacak log kaydı idsi</param>
        public virtual async Task Passive(long id, string userName, DateTime? updateTime, string ipAddress)
        {
            //No improvements were made as the log records should not be deleted.
            await Task.Run(() => { throw new NotImplementedException(); });
        }

        /// <summary>
        /// It throws an error because log records cannot be disabled.
        /// </summary>
        /// <param name="entity">pasife alınacak log kaydı </param>
        public virtual async Task Passive(T entity, string userName, DateTime? updateTime, string ipAddress)
        {
            //No improvements were made as the log records should not be deleted.
            await Task.Run(() => { throw new NotImplementedException(); });
        }

        /// <summary>
        /// It throws an error because log records cannot be disabled.
        /// </summary>
        /// <param name="entityList">pasife alınacak log kayıtları </param>
        public virtual async Task Passive(IEnumerable<T> entityList, string userName, DateTime? updateTime, string ipAddress)
        {
            //No improvements were made as the log records should not be deleted.
            await Task.Run(() => { throw new NotImplementedException(); });
        }

        /// <summary>
        /// It throws an error because log records cannot be disabled.
        /// </summary>
        /// <param name="idList">pasife alınacak log kayıtlarının id leri </param>
        public virtual async Task Passive(List<long> idList, string userName, DateTime? updateTime, string ipAddress)
        {
            //No improvements were made as the log records should not be deleted.
            await Task.Run(() => { throw new NotImplementedException(); });
        }

        public virtual async Task<T> GetById(long id)
        {
            return await UnitOfWork.Connection.QueryFirstOrDefaultAsync<T>(
                $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} WHERE id = @Id",
                param: new { Id = id },
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// It performs the operation of fetching all the values registered to the database of the requested database entity object.
        /// </summary>
        /// <param name="order">Fields to be sorted are specified here </param>
        /// <returns>Related Entity list registered in database</returns>
        public virtual async Task<IEnumerable<T>> GetAll(string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return await (await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} {order}"))
               .ToListAsync();
        }


        public virtual async Task<IPagedList<T>> GetAll(Page page, string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return await (await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()}  {order} LIMIT {page.PageSize} OFFSET {page.Skip}",
               transaction: UnitOfWork.Transaction)).ToPagedListAsync(1, page.PageSize);
        }


        public virtual async Task<IEnumerable<T>> GetMany(string criteria, object parameters, string order)
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return await UnitOfWork.Connection.QueryAsync<T>(
                $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} {criteria} {order}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
        }


        public virtual async Task<IPagedList<T>> GetMany(Page page, string criteria, object parameters, string order)
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;

            return await(await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} {criteria}  {order} LIMIT {page.PageSize} OFFSET {page.Skip}",
               param: parameters,
               transaction: UnitOfWork.Transaction)).ToPagedListAsync(1, page.PageSize);
        }


        public virtual async Task<int> Count()
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>(
               $"SELECT count(*) FROM {DataAnnotationHelper.GetTableName<T>()}",
               transaction: UnitOfWork.Transaction);
        }


        public virtual async Task<int> Count(string criteria, object parameters)
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>(
                $"SELECT count(*) FROM {DataAnnotationHelper.GetTableName<T>()} {criteria}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
        }

        public virtual async Task<List<AuditChange>> GetAudit(long id)
        {
            List<AuditChange> rslt = new List<AuditChange>();
            AuditRepository repository = UnitOfWork.Repositories[typeof(AuditEntity)];

            var auditTrail = await repository.GetMany("keyfieldid= @Id AND datamodel=@DataModel", new { Id = id, DataModel = DataAnnotationHelper.GetTableName<T>() }, "createtime DESC");

            // we are looking for audit-history of the record selected.

            foreach (var record in auditTrail)
            {
                AuditChange change = new AuditChange();
                change.DateTimeStamp = record.CreateTime.ToString();
                change.AuditActionType = (AuditActionType)record.ActionType;
                change.AuditActionTypeName = Enum.GetName(typeof(AuditActionType), record.ActionType)!;
                if (!string.IsNullOrEmpty(record.Changes))
                {
                    List<AuditDelta> delta = JsonConvert.DeserializeObject<List<AuditDelta>>(record.Changes)!;
                    change.Changes.AddRange(delta!);
                }

                rslt.Add(change);
            }
            return rslt;
        }
    }
}
