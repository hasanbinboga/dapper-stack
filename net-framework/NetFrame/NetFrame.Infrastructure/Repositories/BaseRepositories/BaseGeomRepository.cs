using System.Globalization;
using Dapper;
using Newtonsoft.Json;
using NetFrame.Common.Utils;
using NetFrame.Core;
using NetFrame.Core.Entities;
using PagedList;

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
        /// Genel repository sınıfı oluşturma (contructor)
        /// </summary>
        /// <param name="unitOfWork">reposiyory de kullanılan entity nin içinde bulunduğu context instance bilgisi</param> 
        public BaseGeomRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        /// <summary>
        /// Verilen tek entity nin veri tabanına kayıt işlemlerini sağlar.
        /// </summary>
        /// <param name="entity">Entity bilgisi</param>
        public virtual long Add(T entity)
        {
            //Dapper' ın yaklaşımı nedeniyle burada tanımlanması mümkün değildir.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Listesi verilen entity leri veri tabanına kayıt işlemlerini sağlar
        /// </summary>
        /// <param name="entities">Kaydedilmesi istenilen entity listesi</param>
        public virtual List<long> Add(IEnumerable<T> entities)
        {
            //Dapper' ın yaklaşımı nedeniyle burada tanımlanması mümkün değildir.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Belirtilen entiy nin veritanında güncellenmesini sağlar
        /// </summary>
        /// <param name="entity">Veritabanında güncellenmesi istenen verinin güncellenmiş hali </param>
        public virtual void Update(T entity)
        {
            //Dapper' ın yaklaşımı nedeniyle burada tanımlanması mümkün değildir.
            throw new NotImplementedException();
        }
        /// <summary>
        /// Verilen entity yi veritabanından silme işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="entity">Silinmek istenen Entity bilgisi</param>
        public virtual void Delete(T entity)
        {
            Delete(entity.Id);
        }

        /// <summary>
        /// Verilen id ye sahip entity yi veritabanından silme islemlerini gerçekleştirir.
        /// </summary>
        /// <param name="id">Entity id bilgisi</param>
        public virtual void Delete(long id)
        {
            UnitOfWork.Connection.Execute(
              $"delete from {DataAnnotationHelper.GetTableName<T>()} where Id = @Id",
              param: new { Id = id },
              transaction: UnitOfWork.Transaction);
        }


        /// <summary>
        /// Listesi verilen entity leri veri tabanından silme işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="entities">veritabanından silinmek istenen entity listesi</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            var ids = entities.Cast<Entity>().Select(e => e.Id).ToArray();
            Delete(ids);
        }


        /// <summary>
        /// Id listesi verilen entity leri veri tabanından silme işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="idList">veritabanından silinmek istenen entity lerin id listesi</param>
        public virtual void Delete(IList<long> idList)
        {
            UnitOfWork.Connection.Execute(
              $"delete from {DataAnnotationHelper.GetTableName<T>()} where  id = ANY(@Ids)",
              param: new { Ids = idList },
              transaction: UnitOfWork.Transaction);
        }

        

        public void Passive(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        } 

        /// <summary>
        /// Verilen id ye sahip entity yi veritabanında pasife alma islemlerini gerçekleştirir.
        /// </summary>
        /// <param name="id">Pasife alınmak istenen Entity Idsi</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        public void Passive(long id, string userName, DateTime? updateTime, string ipAddress)
        {
            UnitOfWork.Connection.Execute(
               $"UPDATE {DataAnnotationHelper.GetTableName<T>()} SET isdeleted=@Deleted, updateusername=@UserName, updatetime=@UpdateTime, updateipaddress=@IpAddress::INET where Id = @Id",
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
        public void Passive(T entity, string userName, DateTime? updateTime, string ipAddress)
        {
            Passive(entity.Id, userName, updateTime, ipAddress);
        }

        /// <summary>
        /// Verilen entity listesini veritabanından pasife alma işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="entityList">Pasife alınmak istenen Entity listesi</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        public virtual void Passive(IEnumerable<T> entityList, string userName, DateTime? updateTime, string ipAddress)
        {
            var ids = entityList.Cast<Entity>().Select(e => e.Id).ToList();
            Passive(ids, userName, updateTime, ipAddress);
        }

        /// <summary>
        /// Verilen entity id listesini veritabanından pasife alma işlemlerini gerçekleştirir.
        /// </summary>
        /// <param name="idList">Pasife alınmak istenen Entity id listesi</param>
        /// <param name="userName">pasife alan kullanıcı adı</param>
        /// <param name="updateTime">pasife alınma tarihi</param>
        /// <param name="ipAddress">pasife alan kullanıcının ip adresi</param>
        public virtual void Passive(List<long> idList, string userName, DateTime? updateTime, string ipAddress)
        {
            UnitOfWork.Connection.Execute(
                 $"UPDATE {DataAnnotationHelper.GetTableName<T>()} SET isdeleted=@Deleted, updateusername=@UserName, updatetime=@UpdateTime, updateipaddress=@IpAddress::INET WHERE id = ANY(@Ids)",
                 param: new { Deleted = true, Ids = idList, UserName = userName, UpdateTime = updateTime, IpAddress = ipAddress },
                 transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// Verilen id ye göre ilgili entity sınıfının veritabanına kayıtlı örneğini  getirme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="id">Entity id değeri</param>
        /// <returns>İlgili entity nesnesi</returns>
        public virtual T GetById(long id)
        {
            return UnitOfWork.Connection.Query<T>(
                 $"select *, st_astext(geom) geomwkt from {DataAnnotationHelper.GetTableName<T>()} where isdeleted=false AND Id = @Id",
                 param: new { Id = id },
                 transaction: UnitOfWork.Transaction)
                 .FirstOrDefault();
        }

        /// <summary>
        /// İstekte bulunulan veritabanı entity nesnesinin veritabanına kayıtlı tüm değerlerini getirme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="order">Sıralanması istenen fieldlar burada belirtilir
        /// Örnek: 
        /// var order = "id ASC, updateusername DESC";
        /// </param>
        /// <returns>veri tabanına kayıtlı ilgili Entity listesi</returns>
        public virtual IEnumerable<T> GetAll(string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return UnitOfWork.Connection.Query<T>(
               $"select *, st_astext(geom) geomwkt from {DataAnnotationHelper.GetTableName<T>()} WHERE isdeleted=false {order}")
               .ToList();
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
        public virtual IPagedList<T> GetAll(Page page, string order = "")
        {
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return UnitOfWork.Connection.Query<T>(
               $"select *, st_astext(geom) geomwkt from {DataAnnotationHelper.GetTableName<T>()} WHERE isdeleted=false {order} limit {page.PageSize} offset {page.Skip}",
               transaction: UnitOfWork.Transaction).ToPagedList(1, page.PageSize);
        }

        /// <summary>
        /// Where ifadesi içerisinde bulunan kriterler ve verilen parametreler ile eşleşen
        /// kayıtları getirir.
        /// Örnek: where="ad=@Ad AND soyad=@SoyAd";
        ///  var parameters = new {Ad="Ahmet", Soyad="Yılmaz"};
        /// </summary>
        /// <param name="criteria">SQL ifadesinin sonuna eklenecek kriter metni</param>
        /// <param name="parameters">Kriter metninde yer alan parametreler. Kriter Metnindeki Parametre adlarıyla aynı olmalıdır.</param>
        /// <param name="order">Sıralanması istenen fieldlar burada belirtilir
        /// Örnek: 
        /// var order = "id ASC, updateusername DESC";
        /// </param>
        /// <returns>veri tabanına kayıtlı ilgili Entity listesi</returns>
        public virtual IEnumerable<T> GetMany(string criteria, object parameters, string order = "")
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;
            return UnitOfWork.Connection.Query<T>(
                $"select *, st_astext(geom) geomwkt from {DataAnnotationHelper.GetTableName<T>()} where isdeleted=false {criteria} {order}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// Where ifadesi içerisinde bulunan kriterler ve verilen parametreler ile eşleşen
        /// kayıtları getirir.
        /// Örnek: where="ad=@Ad AND soyad=@SoyAd";
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
        public IPagedList<T> GetMany(Page page, string criteria, object parameters, string order = "")
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            order = string.IsNullOrEmpty(order) ? string.Empty : " ORDER BY " + order;

            return UnitOfWork.Connection.Query<T>(
               $"select *, st_astext(geom) geomwkt from {DataAnnotationHelper.GetTableName<T>()} where isdeleted=false {criteria}  {order} limit {page.PageSize} offset {page.Skip}",
               param: parameters,
               transaction: UnitOfWork.Transaction).ToPagedList(1, page.PageSize);
        }

        /// <summary>
        /// Veri tabanındaki toplam aktif kayıt sayısını verir.
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return UnitOfWork.Connection.ExecuteScalar<int>(
                $"select count(*) from {DataAnnotationHelper.GetTableName<T>()} WHERE isdeleted=false",
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// Belirtilen sorgu kriterlerine göre toplam aktif kayıt sayısını verir
        /// </summary>
        /// <param name="criteria">
        /// Where ifadesi içerisinde bulunan kriterler ve verilen parametreler ile eşleşen
        /// kayıtları getirir.
        /// Örnek: where="ad=@Ad AND soyad=@SoyAd";
        ///  var parameters = new {Ad="Ahmet", Soyad="Yılmaz"};
        /// </param>
        /// <param name="parameters">Kriter metninde yer alan parametreler. Kriter Metnindeki Parametre adlarıyla aynı olmalıdır.</param>
        /// <returns></returns>
        public virtual int Count(string criteria, object parameters)
        {
            criteria = string.IsNullOrEmpty(criteria) ? string.Empty : "AND " + criteria;
            return UnitOfWork.Connection.ExecuteScalar<int>(
                $"select count(*) from {DataAnnotationHelper.GetTableName<T>()} where isdeleted=false {criteria}",
                param: parameters,
                transaction: UnitOfWork.Transaction);
        }

        /// <summary>
        /// İlgili nesnenin belirtilen id değerine sahip insance ının veri geçmişini getirme işlemini gerçekleştirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AuditChange> GetAudit(long id)
        {
            List<AuditChange> rslt = new List<AuditChange>();
            AuditRepository repository = UnitOfWork.Repositories[typeof(AuditEntity)];

            var auditTrail = repository.GetMany("keyfieldid= @Id AND datamodel=@DataModel", new { Id = id, DataModel = DataAnnotationHelper.GetTableName<T>() }, "createtime DESC");

            // we are looking for audit-history of the record selected.

            foreach (var record in auditTrail)
            {
                AuditChange change = new AuditChange
                {
                    DateTimeStamp = record.CreateTime.ToString(CultureInfo.InvariantCulture),
                    AuditActionType = record.ActionType,
                    AuditActionTypeName = Enum.GetName(typeof(AuditActionType), record.ActionType)
                };
                if (record != null && !string.IsNullOrEmpty(record.Changes))
                {
                    List<AuditDelta> delta = JsonConvert.DeserializeObject<List<AuditDelta>>(record?.Changes);
                    change.Changes.AddRange(delta);
                }

                rslt.Add(change);
            }
            return rslt;
        }

       
    }
}
