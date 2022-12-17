using System.Globalization;
using Dapper;
using Newtonsoft.Json;
using NetFrame.Common.Utils;
using NetFrame.Core;
using NetFrame.Core.Entities;
using X.PagedList;


namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Özel repository leri tanımlanmamış ve geometry fieldı içeren veriler için genel repository interface sınıfı implementasyonu. 
    /// Temel CRUD işlemlerini sağlar
    /// </summary>
    /// <typeparam name="T"> Geometry si olan Entity türü </typeparam>
    public class BaseGeomRepository<T> : IBaseGeomRepository<T> where T : BaseGeomEntity
    {
        protected readonly IUnitOfWork UnitOfWork;


        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="unitOfWork">Context instance information of the entity used in the repository</param> 
        public BaseGeomRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        /// <summary>
        /// It provides the registration operations of the given single entity to the database.
        /// </summary>
        /// <param name="entity">Entity info</param>
        public virtual async Task<long> Add(T entity)
        {
            //Due to Dapper's approach it is not possible to describe it here.

            return await Task.Run(long () => { throw new NotImplementedException(); });
        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Kaydedilmesi istenilen entity listesi</param>
        public virtual async Task<List<long>> Add(IEnumerable<T> entities)
        {
            //Due to Dapper's approach it is not possible to describe it here.

            return await Task.Run(List<long> () => { throw new NotImplementedException(); });

        }

        /// <summary>
        /// Belirtilen entiy nin veritanında güncellenmesini sağlar
        /// </summary>
        /// <param name="entity">Veritabanında güncellenmesi istenen verinin güncellenmiş hali </param>
        public virtual async Task Update(T entity)
        {
            //Due to Dapper's approach it is not possible to describe it here.
            await Task.Run(() => { throw new NotImplementedException(); });
        }
        /// <summary>
        /// Verilen entity yi veritabanından silme işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="entity">Silinmek istenen Entity bilgisi</param>
        public virtual async Task Delete(T entity)
        {
            await Delete(entity.Id);
        }

        /// <summary>
        /// Verilen id ye sahip entity yi veritabanından silme islemlerini gerçekleştirir.
        /// </summary>
        /// <param name="id">Entity id bilgisi</param>
        public virtual async Task Delete(long id)
        {
            await UnitOfWork.Connection.ExecuteAsync(
              $"delete FROM {DataAnnotationHelper.GetTableName<T>()} WHERE id = @Id",
              param: new { Id = id },
              transaction: UnitOfWork.Transaction);
        }


        /// <summary>
        /// Listesi verilen entity leri veri tabanından silme işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="entities">veritabanından silinmek istenen entity listesi</param>
        public virtual async Task Delete(IEnumerable<T> entities)
        {
            var ids = entities.Cast<Entity>().Select(e => e.Id).ToArray();
            await Delete(ids);
        }


        /// <summary>
        /// Id listesi verilen entity leri veri tabanından silme işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="idList">veritabanından silinmek istenen entity lerin id listesi</param>
        public virtual async Task Delete(IList<long> idList)
        {
            await UnitOfWork.Connection.ExecuteAsync(
              $"delete FROM {DataAnnotationHelper.GetTableName<T>()} WHERE  id = ANY(@Ids)",
              param: new { Ids = idList },
              transaction: UnitOfWork.Transaction);
        }



        /// <summary>
        /// Verilen id ye sahip entity yi veritabanında pasife alma islemlerini gerçekleştirir.
        /// </summary>
        /// <param name="id">Pasife alınmak istenen Entity Idsi</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        public async Task Passive(long id, string userName, DateTime? updateTime, string ipAddress)
        {
            await UnitOfWork.Connection.ExecuteAsync(
                $"UPDATE {DataAnnotationHelper.GetTableName<T>()} SET isdeleted=@Deleted, updateusername=@UserName, updatetime=@UpdateTime, updateipaddress=@IpAddress::INET WHERE id = @Id",
                param: new { Deleted = true, Id = id, UserName = userName, UpdateTime = updateTime, IpAddress = ipAddress },
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// Verilen entity yi veritabanından pasife alma işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="entity">Pasife alınmak istenen Entity</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        public async Task Passive(T entity, string userName, DateTime? updateTime, string ipAddress)
        {
            await Passive(entity.Id, userName, updateTime, ipAddress);
        }

        /// <summary>
        /// Verilen entity listesini veritabanından pasife alma işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="entityList">Pasife alınmak istenen Entity listesi</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        public virtual async Task Passive(IEnumerable<T> entityList, string userName, DateTime? updateTime, string ipAddress)
        {
            var ids = entityList.Cast<Entity>().Select(e => e.Id).ToList();
            await Passive(ids, userName, updateTime, ipAddress);
        }

        /// <summary>
        /// Verilen entity id listesini veritabanından pasife alma işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="idList">Pasife alınmak istenen Entity id listesi</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        public virtual async Task Passive(List<long> idList, string userName, DateTime? updateTime, string ipAddress)
        {
            await UnitOfWork.Connection.ExecuteAsync(
                  $"UPDATE {DataAnnotationHelper.GetTableName<T>()} SET isdeleted=@Deleted, updateusername=@UserName, updatetime=@UpdateTime, updateipaddress=@IpAddress::INET WHERE id = ANY(@Ids)",
                  param: new { Deleted = true, Ids = idList, UserName = userName, UpdateTime = updateTime, IpAddress = ipAddress },
                  transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// Verilen id ye göre ilgili entity sınıfının veritabanına kayıtlı örneğini  getirme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="id">Entity id değeri</param>
        /// <returns>İlgili entity nesnesi</returns>
        public virtual async Task<T> GetById(long id)
        {
            var res = await UnitOfWork.Connection.QueryFirstOrDefaultAsync<T>(
                 $"SELECT *, st_astext(geom) geomwkt FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted AND id = @Id",
                 param: new { Id = id },
                 transaction: UnitOfWork.Transaction);
            return res;
        }

        /// <summary>
        /// İstekte bulunulan veritabanı entity nesnesinin veritabanına kayıtlı tüm değerlerini getirme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="order">Sıralanması istenen fieldlar burada belirtilir
        /// Örnek: 
        /// var order = "id ASC, updateusername DESC";
        /// </param>
        /// <returns>veri tabanına kayıtlı ilgili Entity listesi</returns>
        public virtual async Task<IEnumerable<T>> GetAll(string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            var res = await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT *, st_astext(geom) geomwkt FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {order}");


            return await res.ToListAsync();
        }

        /// <summary>
        /// İstekte bulunulan veritabanı entity nesnesinin veritabanına kayıtlı belirtilen sayfadaki  
        /// tüm değerlerini getirme işlemini gerçekleştirir.
        ///  </summary>
        /// <param name="page">Talepte bulunulan sayfa bilgisi</param>
        /// <param name="order">Sıralanması istenen fieldlar burada belirtilir
        /// Örnek: 
        /// var order = "id ASC, updateusername DESC";
        /// </param>
        /// <returns>veri tabanına kayıtlı ilgili Entity listesi</returns>
        public virtual async Task<IPagedList<T>> GetAll(Page page, string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            var res = await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT *, st_astext(geom) geomwkt FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {order} LIMIT {page.PageSize} OFFSET {page.Skip}",
               transaction: UnitOfWork.Transaction);
            return await res.ToPagedListAsync(1, page.PageSize);
        }

        /// <summary>
        /// Where ifadesi içerisinde bulunan kriterler ve verilen parametreler ile eşleşen
        /// kayıtları getirir.
        /// Örnek: WHERE="ad=@Ad AND soyad=@SoyAd";
        ///  var parameters = new {Ad="Ahmet", Soyad="Yılmaz"};
        /// </summary>
        /// <param name="criteria">SQL ifadesinin sonuna eklenecek kriter metni</param>
        /// <param name="parameters">Kriter metninde yer alan parametreler. Kriter Metnindeki Parametre adlarıyla aynı olmalıdır.</param>
        /// <param name="order">Sıralanması istenen fieldlar burada belirtilir
        /// Örnek: 
        /// var order = "id ASC, updateusername DESC";
        /// </param>
        /// <returns>veri tabanına kayıtlı ilgili Entity listesi</returns>
        public virtual async Task<IEnumerable<T>> GetMany(string criteria, object parameters, string order = "")
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            var res = await UnitOfWork.Connection.QueryAsync<T>(
                $"SELECT *, st_astext(geom) geomwkt FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {criteria} {order}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
            return await res.ToListAsync();
        }

        /// <summary>
        /// Where ifadesi içerisinde bulunan kriterler ve verilen parametreler ile eşleşen
        /// kayıtları getirir.
        /// Örnek: WHERE="ad=@Ad AND soyad=@SoyAd";
        ///  var parameters = new {Ad="Ahmet", Soyad="Yılmaz"};
        /// </summary>
        /// <param name="page">Talep edilen veri sayfasına ait bilgiler</param>
        /// <param name="criteria">SQL ifadesinin sonuna eklenecek kriter metni</param>
        /// <param name="parameters">Kriter metninde yer alan parametreler. Kriter Metnindeki Parametre adlarıyla aynı olmalıdır.</param>
        /// <param name="order">Sıralanması istenen fieldlar burada belirtilir
        /// Örnek: 
        /// var order = "id ASC, updateusername DESC";
        /// </param>
        /// <returns>veri tabanına kayıtlı ilgili Entity listesi</returns>
        public virtual async Task<IPagedList<T>> GetMany(Page page, string criteria, object parameters, string order = "")
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;

            var res = await UnitOfWork.Connection.QueryAsync<T>(
               $"SELECT *, st_astext(geom) geomwkt FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {criteria}  {order} LIMIT {page.PageSize} OFFSET {page.Skip}",
               param: parameters,
               transaction: UnitOfWork.Transaction);
            return await res.ToPagedListAsync(1, page.PageSize);
        }

        /// <summary>
        /// Veri tabanındaki toplam aktif kayıt sayısını verir.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> Count()
        {
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>(
                $"SELECT count(*) FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted",
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// Belirtilen sorgu kriterlerine göre toplam aktif kayıt sayısını verir
        /// </summary>
        /// <param name="criteria">
        /// Where ifadesi içerisinde bulunan kriterler ve verilen parametreler ile eşleşen
        /// kayıtları getirir.
        /// Örnek: WHERE="ad=@Ad AND soyad=@SoyAd";
        ///  var parameters = new {Ad="Ahmet", Soyad="Yılmaz"};
        /// </param>
        /// <param name="parameters">Kriter metninde yer alan parametreler. Kriter Metnindeki Parametre adlarıyla aynı olmalıdır.</param>
        /// <returns></returns>
        public virtual async Task<int> Count(string criteria, object parameters)
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            return await UnitOfWork.Connection.ExecuteScalarAsync<int>(
                $"SELECT count(*) FROM {DataAnnotationHelper.GetTableName<T>()} WHERE NOT isdeleted {criteria}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// İlgili nesnenin belirtilen id değerine sahip insance ının veri geçmişini getirme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<AuditChange>> GetAudit(long id)
        {
            List<AuditChange> rslt = new List<AuditChange>();
            AuditRepository repository = UnitOfWork.Repositories[typeof(AuditEntity)];

            var auditTrail = await repository.GetMany("keyfieldid= @Id AND datamodel=@DataModel", new { Id = id, DataModel = DataAnnotationHelper.GetTableName<T>() }, "createtime DESC");

            // we are looking for audit-history of the record selected.

            foreach (var record in auditTrail)
            {
                AuditChange change = new AuditChange
                {
                    DateTimeStamp = record.CreateTime.ToString(CultureInfo.InvariantCulture),
                    AuditActionType = record.ActionType,
                    AuditActionTypeName = Enum.GetName(typeof(AuditActionType), record.ActionType)!
                };
                if (record != null && !string.IsNullOrEmpty(record.Changes))
                {
                    List<AuditDelta> delta = JsonConvert.DeserializeObject<List<AuditDelta>>(record?.Changes!)!;
                    change.Changes.AddRange(delta!);
                }

                rslt.Add(change);
            }
            return rslt;
        }


    }
}
