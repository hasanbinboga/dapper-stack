using Dapper;
using NetFrame.Common.Utils;
using NetFrame.Core;
using NetFrame.Core.Entities;
using Newtonsoft.Json;
using X.PagedList;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Public repository interface class implementation for data with no private repository defined. Provides basic CRUD operations
    /// </summary>
    /// <typeparam name="T"> Entity türü </typeparam>
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly IUnitOfWork UnitOfWork;


        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="unitOfWork">Context instance information of the entity used in the repository</param> 
        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }



        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public virtual async Task<List<long>> Add(IEnumerable<T> entities)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            throw new NotImplementedException();
        }

        /// <summary>
        /// It provides the registration operations of the given single entity to the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<long> Add(T entity)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity"Updated version of the data requested to be updated in the database </param>
        public virtual async Task Update(T entity)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            throw new NotImplementedException();
        }

        /// <summary>
        /// It performs the operations of deleting the entities whose list is given FROM the database.
        /// </summary>
        /// <param name="entities">Entity list to be deleted FROM database</param>
        public virtual async Task Delete(IEnumerable<T> entities)
        {
            var ids = entities.Cast<Entity>().Select(e => e.Id).ToArray();
            await Delete(ids);
        }

        /// <summary>
        /// It performs the operations of deleting the entities whose id list is given FROM the database.
        /// </summary>
        /// <param name="idList">Id list of entities to be deleted FROM database</param>
        public virtual async Task Delete(IList<long> idList)
        {
            await UnitOfWork.Connection.ExecuteAsync(
                $"delete FROM {DataAnnotationHelper.GetTableName<T>()} WHERE id = ANY(@Ids)",
                param: new { Ids = idList },
                transaction: UnitOfWork.Transaction);
        }



        /// <summary>
        /// It performs the operations of deleting the entity with the given id FROM the database.
        /// </summary>
        /// <param name="id">Entity id information</param>
        public virtual async Task Delete(long id)
        {
            await UnitOfWork.Connection.ExecuteAsync(
                $"delete FROM {DataAnnotationHelper.GetTableName<T>()} WHERE id = @Id",
                param: new { Id = id },
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// It performs the operations of deleting the given entity FROM the database.
        /// </summary>
        /// <param name="entity">Entity information to be deleted</param>
        public virtual async Task Delete(T entity)
        {
            await Delete(entity.Id);
        }

        /// <summary>
        /// It performs the operations of deactivating the entity with the given id in the database.
        /// </summary>
        /// <param name="id">Pasife alınmak istenen Entity Idsi</param>
        /// <param name="userName">user name</param>
        /// <param name="updateTime">passive time</param>
        /// <param name="ipAddress">user ip address</param>
        public virtual async Task Passive(long id, string userName, DateTime? updateTime, string ipAddress)
        {
            await UnitOfWork.Connection.ExecuteAsync(
                $"UPDATE {DataAnnotationHelper.GetTableName<T>()} SET isdeleted=@Deleted, updateusername=@UserName, updatetime=@UpdateTime, updateipaddress=@IpAddress::INET WHERE id = @Id",
                param: new { Deleted = true, Id = id, UserName = userName, UpdateTime = updateTime, IpAddress = ipAddress },
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// It performs the operations of deactivating the given entity FROM the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="userName">user name</param>
        /// <param name="updateTime">passive time</param>
        /// <param name="ipAddress">user ip address</param>
        public virtual async Task Passive(T entity, string userName, DateTime? updateTime, string ipAddress)
        {
            await Passive(entity.Id, userName, updateTime, ipAddress);
        }

        /// <summary>
        /// It performs the operations of deactivating the given entity list FROM the database.
        /// </summary>
        /// <param name="entityList">Entity list</param>
        /// <param name="userName">user name</param>
        /// <param name="updateTime">passive time</param>
        /// <param name="ipAddress">user ip address</param>
        public virtual async Task Passive(IEnumerable<T> entityList, string userName, DateTime? updateTime, string ipAddress)
        {
            var ids = entityList.Cast<Entity>().Select(e => e.Id).ToList();
            await Passive(ids, userName, updateTime, ipAddress);
        }

        /// <summary>
        /// It performs the operations of deactivating the given entity id list FROM the database.
        /// </summary>
        /// <param name="idList">Entity id listesi</param>
        /// <param name="userName">user name</param>
        /// <param name="updateTime">passive time</param>
        /// <param name="ipAddress">user ip address</param>
        public virtual async Task Passive(List<long> idList, string userName, DateTime? updateTime, string ipAddress)
        {
            await UnitOfWork.Connection.ExecuteAsync(
                     $"UPDATE {DataAnnotationHelper.GetTableName<T>()} SET isdeleted=@Deleted, updateusername=@UserName, updatetime=@UpdateTime, updateipaddress=@IpAddress::INET WHERE id = ANY(@Ids)",
                     param: new { Deleted = true, Ids = idList, UserName = userName, UpdateTime = updateTime, IpAddress = ipAddress },
                     transaction: UnitOfWork.Transaction);
        }


        /// <summary>
        /// It performs the operation of fetching all the values registered to the database of the requested database entity object.
        /// </summary>
        /// <param name="order">Fields to be sorted are specified here
        /// </param>
        /// <returns>Related Entity list registered in database</returns>
        public virtual async Task<IEnumerable<T>> GetAll(string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;

            var res = await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {order}");

            return await res.ToListAsync();
        }

        /// <summary>
        /// It performs the operation to fetch all the values of the requested database 
        /// entity object on the specified page registered to the database.
        ///  </summary>
        /// <param name="page">Page info</param>
        /// <param name="order">Fields to be sorted are specified here</param>
        /// <returns>Related Entity list registered in database</returns>
        public async Task<IPagedList<T>> GetAll(Page page, string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            var res = await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {order} LIMIT {page.PageSize} OFFSET {page.Skip}",
               transaction: UnitOfWork.Transaction);

            return await res.ToPagedListAsync(1, page.PageSize);
        }


        /// <summary>
        /// It performs the operation of bringing the registered instance of the related 
        /// entity class to the database according to the given id.
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>entity object</returns>
        public virtual async Task<T> GetById(long id)
        {
            var res = await UnitOfWork.Connection.QueryFirstOrDefaultAsync<T>(
                 $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted AND id = @Id",
                 param: new { Id = id },
                 transaction: UnitOfWork.Transaction);
            return res;
        }


        /// <summary>
        /// Returns the total number of active records in the database.
        /// </summary>
        /// <returns></returns>
        public virtual async  Task<int> Count()
        {
            var res = await UnitOfWork.Connection.ExecuteScalarAsync<int>(
                $"SELECT count(*) FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted",
                transaction: UnitOfWork.Transaction);

            return res;
        }


        /// <summary>
        /// Returns the total number of active records based on the specified query criteria
        /// </summary>
        /// <param name="criteria"> It returns records that match the criteria and given parameters in the WHERE statement. </param>
        /// <param name="parameters">Parameters in the criteria text. Must be the same as the Parameter names in the criteria.</param>
        /// <returns></returns>
        public virtual async Task<int> Count(string criteria, object parameters)
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            var res = await UnitOfWork.Connection.ExecuteScalarAsync<int>(
                $"SELECT count(*) FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {criteria}",
                param: parameters,
                transaction: UnitOfWork.Transaction);

            return res;
        }


        /// <summary> It returns records that match the criteria and given parameters in the WHERE statement. </summary>
        /// <param name="criteria">Where clause</param>
        /// <param name="parameters">Parameters in the criteria statement. Must be the same as the Parameter names in the Criteria statement.</param>
        /// <param name="order">Fields to be sorted are specified here </param>
        /// <returns>Related Entity list registered in database</returns>
        public virtual async Task<IEnumerable<T>> GetMany(string criteria, object parameters, string order = "")
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return await UnitOfWork.Connection.QueryAsync<T>(
                $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {criteria} {order}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
        }

        /// <summary> It returns records that match the criteria and given parameters in the WHERE statement. </summary>
        /// <param name="page">Pagination info</param>
        /// <param name="criteria">Where clause</param>
        /// <param name="parameters">Parameters in the criteria statement. Must be the same as the Parameter names in the Criteria statement.</param>
        /// <param name="order">Fields to be sorted are specified here </param>
        /// <returns>Related Entity list registered in database</returns>
        public async Task<IPagedList<T>> GetMany(Page page, string criteria, object parameters, string order = "")
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;

            var res = await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT * FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {criteria}  {order} LIMIT {page.PageSize} OFFSET {page.Skip}",
               param: parameters,
               transaction: UnitOfWork.Transaction);

            return await res.ToPagedListAsync(1, page.PageSize);
        }

        public async Task<List<AuditChange>> GetAudit(long id)
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
                change.AuditActionTypeName = Enum.GetName(typeof(AuditActionType), record.ActionType);
                if (!string.IsNullOrEmpty(record.Changes))
                {
                    List<AuditDelta> delta = JsonConvert.DeserializeObject<List<AuditDelta>>(record.Changes);
                    change.Changes.AddRange(delta);
                }

                rslt.Add(change);
            }
            return rslt;
        }
    }
}
