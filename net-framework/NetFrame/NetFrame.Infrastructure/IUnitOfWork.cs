using System;
using System.Collections.Generic;
using System.Data;

namespace NetFrame.Infrastructure
{
    /// <summary>
    /// Veritabanı ile yapılacak olan tüm işlemleri, tek bir kanal aracılığı ile 
    /// gerçekleştirme ve hafızada tutma işlemlerini sunmaktadır. 
    /// Bu sayede işlemlerin toplu halde gerçekleştirilmesi ve hata durumunda geri alınabilmesi sağlamaktadır.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Veri tabanı bağlantısı
        /// </summary>
        IDbConnection Connection { get; }
        /// <summary>
        /// Veri tabanı işlemi 
        /// </summary>
        IDbTransaction Transaction { get; }
        /// <summary>
        /// Veri tabanı işlemi izolasyon seviyesi
        /// </summary>
        IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// Veri tabanında işlem içerisinde yapılan değişiklikleri kaydetmek için kullanılan metot.
        /// </summary>
        /// <returns></returns>
        bool Commit();
        /// <summary>
        /// Herhangi bir hata durumunda veri tabanında o işlem içerisinde yapılan değişiklikleri geri alan metot.
        /// </summary>
        void Rollback();


        /// <summary>
        /// UnitOfWork üzerinde entity e özel repository register işlemlerini yönetir.
        /// </summary>
        /// <typeparam name="T"> entity türü. ör: Message </typeparam>
        /// <param name="repository">Custom olarak tanımlanmış  repository nin tür bilgisi. Ugadan Ör: MessageRepository </param>
        void RegisterRepository<T>(Type repository) where T : class, new();


        /// <summary>
        /// Uygulama ile ilişkili tüm veri kümeleri
        /// </summary>
        Dictionary<Type, dynamic> Repositories { get; set; }

    }
}
