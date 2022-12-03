﻿using Dapper;
using NetFrame.Common.Utils;
using NetFrame.Core;
using NetFrame.Core.Entities;
using Newtonsoft.Json;
using PagedList;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Public repository interface class implementation for data with no private repository defined. Provides basic CRUD operations
    /// </summary>
    /// <typeparam name="T"> Entity türü </typeparam>
    public class Repository<T> : IRepository<T> where T :  Entity
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
        public virtual List<long> Add(IEnumerable<T> entities)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            throw new NotImplementedException();
        }

        /// <summary>
        /// It provides the registration operations of the given single entity to the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual long Add(T entity)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity"Updated version of the data requested to be updated in the database </param>
        public virtual void Update(T entity)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            throw new NotImplementedException();
        }

        /// <summary>
        /// It performs the operations of deleting the entities whose list is given from the database.
        /// </summary>
        /// <param name="entities">Entity list to be deleted from database</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            var ids = entities.Cast<Entity>().Select(e => e.Id).ToArray();
            Delete(ids);
        }

        /// <summary>
        /// It performs the operations of deleting the entities whose id list is given from the database.
        /// </summary>
        /// <param name="idList">Id list of entities to be deleted from database</param>
        public virtual void Delete(IList<long> idList)
        {
            UnitOfWork.Connection.Execute(
                $"delete from {DataAnnotationHelper.GetTableName<T>()} where id = ANY(@Ids)",
                param: new { Ids = idList },
                transaction: UnitOfWork.Transaction);
        }



        /// <summary>
        /// It performs the operations of deleting the entity with the given id from the database.
        /// </summary>
        /// <param name="id">Entity id information</param>
        public virtual void Delete(long id)
        {
            UnitOfWork.Connection.Execute(
                $"delete from {DataAnnotationHelper.GetTableName<T>()} where Id = @Id",
                param: new { Id = id },
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// It performs the operations of deleting the given entity from the database.
        /// </summary>
        /// <param name="entity">Entity information to be deleted</param>
        public virtual void Delete(T entity)
        {
            Delete(entity.Id);
        }

        /// <summary>
        /// It performs the operations of deactivating the entity with the given id in the database.
        /// </summary>
        /// <param name="id">Pasife alınmak istenen Entity Idsi</param>
        /// <param name="userName">user name</param>
        /// <param name="updateTime">passive time</param>
        /// <param name="ipAddress">user ip address</param>
        public virtual void Passive(long id, string userName, DateTime? updateTime, string ipAddress)
        {
            UnitOfWork.Connection.Execute(
                $"UPDATE {DataAnnotationHelper.GetTableName<T>()} SET isdeleted=@Deleted, updateusername=@UserName, updatetime=@UpdateTime, updateipaddress=@IpAddress::INET where id = @Id",
                param: new { Deleted = true, Id = id,  UserName = userName, UpdateTime = updateTime, IpAddress = ipAddress },
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// It performs the operations of deactivating the given entity from the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="userName">user name</param>
        /// <param name="updateTime">passive time</param>
        /// <param name="ipAddress">user ip address</param>
        public virtual void Passive(T entity, string userName, DateTime? updateTime, string ipAddress)
        {
            Passive(entity.Id, userName, updateTime, ipAddress);
        }

        /// <summary>
        /// It performs the operations of deactivating the given entity list from the database.
        /// </summary>
        /// <param name="entityList">Entity list</param>
        /// <param name="userName">user name</param>
        /// <param name="updateTime">passive time</param>
        /// <param name="ipAddress">user ip address</param>
        public virtual void Passive(IEnumerable<T> entityList, string userName, DateTime? updateTime, string ipAddress)
        {
            var ids = entityList.Cast<Entity>().Select(e => e.Id).ToList();
            Passive(ids, userName, updateTime, ipAddress);
        }

        /// <summary>
        /// It performs the operations of deactivating the given entity id list from the database.
        /// </summary>
        /// <param name="idList">Entity id listesi</param>
        /// <param name="userName">user name</param>
        /// <param name="updateTime">passive time</param>
        /// <param name="ipAddress">user ip address</param>
        public void Passive(List<long> idList, string userName, DateTime? updateTime, string ipAddress)
        {
            UnitOfWork.Connection.Execute(
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
        public virtual IEnumerable<T> GetAll(string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return UnitOfWork.Connection.Query<T>(
               $"select * from {DataAnnotationHelper.GetTableName<T>()} WHERE isdeleted=false {order}")
               .ToList();
        }

        /// <summary>
        /// It performs the operation to fetch all the values of the requested database 
        /// entity object on the specified page registered to the database.
        ///  </summary>
        /// <param name="page">Page info</param>
        /// <param name="order">Fields to be sorted are specified here</param>
        /// <returns>Related Entity list registered in database</returns>
        public IPagedList<T> GetAll(Page page, string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return UnitOfWork.Connection.Query<T>(
               $"select * from {DataAnnotationHelper.GetTableName<T>()} WHERE isdeleted=false {order} limit {page.PageSize} offset {page.Skip}",
               transaction: UnitOfWork.Transaction).ToPagedList(1, page.PageSize);
        }


        /// <summary>
        /// It performs the operation of bringing the registered instance of the related 
        /// entity class to the database according to the given id.
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>entity object</returns>
        public virtual T GetById(long id)
        {
            return UnitOfWork.Connection.Query<T>(
                 $"select * from {DataAnnotationHelper.GetTableName<T>()} where isdeleted=false AND Id = @Id",
                 param: new { Id = id },
                 transaction: UnitOfWork.Transaction)
                 .FirstOrDefault();
        }


        /// <summary>
        /// Returns the total number of active records in the database.
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return UnitOfWork.Connection.ExecuteScalar<int>(
                $"select count(*) from {DataAnnotationHelper.GetTableName<T>()} WHERE isdeleted=false",
                transaction: UnitOfWork.Transaction);
        }


        /// <summary>
        /// Returns the total number of active records based on the specified query criteria
        /// </summary>
        /// <param name="criteria"> It returns records that match the criteria and given parameters in the where statement. </param>
        /// <param name="parameters">Parameters in the criteria text. Must be the same as the Parameter names in the criteria.</param>
        /// <returns></returns>
        public virtual int Count(string criteria, object parameters)
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            return UnitOfWork.Connection.ExecuteScalar<int>(
                $"select count(*) from {DataAnnotationHelper.GetTableName<T>()} where isdeleted=false {criteria}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
        }


        /// <summary> It returns records that match the criteria and given parameters in the where statement. </summary>
        /// <param name="criteria">Where clause</param>
        /// <param name="parameters">Parameters in the criteria statement. Must be the same as the Parameter names in the Criteria statement.</param>
        /// <param name="order">Fields to be sorted are specified here </param>
        /// <returns>Related Entity list registered in database</returns>
        public virtual IEnumerable<T> GetMany(string criteria, object parameters, string order = "")
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return UnitOfWork.Connection.Query<T>(
                $"select * from {DataAnnotationHelper.GetTableName<T>()} where isdeleted=false {criteria} {order}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
        }

        /// <summary> It returns records that match the criteria and given parameters in the where statement. </summary>
        /// <param name="page">Pagination info</param>
        /// <param name="criteria">Where clause</param>
        /// <param name="parameters">Parameters in the criteria statement. Must be the same as the Parameter names in the Criteria statement.</param>
        /// <param name="order">Fields to be sorted are specified here </param>
        /// <returns>Related Entity list registered in database</returns>
        public IPagedList<T> GetMany(Page page, string criteria, object parameters, string order = "")
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;

            return UnitOfWork.Connection.Query<T>(
               $"select * from {DataAnnotationHelper.GetTableName<T>()} where isdeleted=false {criteria}  {order} limit {page.PageSize} offset {page.Skip}",
               param: parameters,
               transaction: UnitOfWork.Transaction).ToPagedList(1, page.PageSize);
        }

        public List<AuditChange> GetAudit(long id)
        {
            List<AuditChange> rslt = new List<AuditChange>();
            AuditRepository repository = UnitOfWork.Repositories[typeof(AuditEntity)];

            var auditTrail = repository.GetMany("keyfieldid= @Id AND datamodel=@DataModel", new { Id = id, DataModel = DataAnnotationHelper.GetTableName<T>() }, "createtime DESC");

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