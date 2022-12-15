using System;
using System.Collections.Generic;
using NetFrame.Core;
using X.PagedList;

namespace NetFrame.Infrastructure
{
    /// <summary>
    /// Özel repository leri tanımlanmamış veriler için genel repository interface sınıfı. . Temel CRUD işlemlerini sağlar
    /// </summary>
    /// <typeparam name="T"> Entity türü </typeparam>
    public interface IRepository<T>
    {

        /// <summary>
        /// It provides the registration operations of the given single entity to the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        long Add(T entity);

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        List<long> Add(IEnumerable<T> entities);

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity">Updated version of the data requested to be updated in the database </param>
        void Update(T entity);

        /// <summary>
        /// It performs the operations of deleting the given entity FROM the database.
        /// </summary>
        /// <param name="entity">Entity information to be deleted</param>
        void Delete(T entity);

        /// <summary>
        /// It performs the operations of deleting the entity with the given id FROM the database.
        /// </summary>
        /// <param name="id">Entity id information</param>
        void Delete(long id);

        
        /// <summary>
        /// It performs the operations of deleting the entities whose list is given FROM the database.
        /// </summary>
        /// <param name="entities">Entity list to be deleted FROM database</param>
        void Delete(IEnumerable<T> entities);
        /// <summary>
        /// It performs the operations of deleting the entities whose id list is given FROM the database.
        /// </summary>
        /// <param name="idList">Id list of entities to be deleted FROM database</param>
        void Delete(IList<long> idList);

        /// <summary>
        /// It performs the operations of deactivating the entity with the given id in the database.
        /// </summary>
        /// <param name="id">pasife alınacak entity nin id si</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        void Passive(long id, string userName, DateTime? updateTime, string ipAddress);

        /// <summary>
        /// It performs the operations of deactivating the given entity in the database.
        /// </summary>
        /// <param name="entity">pasife alınacak entity</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        void Passive(T entity, string userName, DateTime? updateTime, string ipAddress);

        /// <summary>
        /// It performs the operations of deactivating the listed entities in the database.
        /// </summary>
        /// <param name="entityList">pasife alınacak entity listesi</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        void Passive(IEnumerable<T> entityList, string userName, DateTime? updateTime, string ipAddress);

        /// <summary>
        /// It performs deactivation and deletion of entities whose id list is given in the database.
        /// </summary>
        /// <param name="idList">Entity Ids to be deactivated</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        void Passive(List<long> idList, string userName, DateTime? updateTime, string ipAddress);

        /// <summary>
        /// It performs the operation of bringing the registered instance of the related entity class to the database according to the given id.
        /// </summary>
        /// <param name="id">Entity id value</param>
        /// <returns>Related entity object</returns>
        T GetById(long id);


        /// <summary>
        /// It performs the operation of fetching all the values registered to the database of the requested database entity object.
        /// </summary>
        /// <param name="order">Fields to be sorted are specified here</param>
        /// <returns>Related Entity list registered in database</returns>
        IEnumerable<T> GetAll(string order = "");

        /// <summary>
        /// It performs the operation to fetch all the values of the requested database entity object on the specified page registered to the database.
        
        ///  </summary>
        /// <param name="page">Requested page information</param>
        /// <param name="order">Fields to be sorted are specified here</param>
        /// <returns>Related Entity list registered in database</returns>
        IPagedList<T> GetAll(Page page, string order = "");

        /// <summary>
        /// It returns records that match the criteria and given parameters in the WHERE statement.
        
        
        
        /// </summary>
        /// <param name="criteria">Where clause</param>
        /// <param name="parameters">Parameters in the criteria text. Must be the same as the Parameter names in the Criteria Text.</param>
        /// <param name="order">Fields to be sorted are specified here</param>
        /// <returns>Related Entity list registered in database</returns>
        IEnumerable<T> GetMany(string criteria, object parameters, string order);

        /// <summary>
        /// It returns records that match the criteria and given parameters in the WHERE statement.
        
        
        
        /// </summary>
        /// <param name="page">Talep edilen veri sayfasına ait bilgiler</param>
        /// <param name="criteria">Where clause</param>
        /// <param name="parameters">Parameters in the criteria text. Must be the same as the Parameter names in the Criteria Text.</param>
        /// <param name="order">Fields to be sorted are specified here</param>
        /// <returns>Related Entity list registered in database</returns>
        IPagedList<T> GetMany(Page page, string criteria, object parameters, string order);

      
       


        /// <summary>
        /// Returns the total number of active records in the database.
        /// </summary>
        /// <returns></returns>
        int Count();


        /// <summary>
        /// Returns the total number of active records based on the specified query criteria
        /// </summary>
        /// <param name="criteria">
        /// It returns records that match the criteria and given parameters in the WHERE statement.
        
        
        
        /// </param>
        /// <param name="parameters">Parameters in the criteria text. Must be the same as the Parameter names in the Criteria Text.</param>
        /// <returns></returns>
        int Count(string criteria, object parameters);
    }
}
