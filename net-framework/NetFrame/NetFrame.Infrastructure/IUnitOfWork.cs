
using NetFrame.Core;
using NetFrame.Infrastructure.Repositories;
using System.Data;
using System.Data.Common;

namespace NetFrame.Infrastructure
{
    /// <summary>
    /// It offers to perform all transactions to be made with the database through a single channel and 
    /// to keep them in memory. In this way, it ensures that the transactions are performed in batches and 
    /// that they can be retrieved in case of error.
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Database connection
        /// </summary>
        DbConnection Connection { get; }
        /// <summary>
        /// Database transaction
        /// </summary>
        DbTransaction Transaction { get; }
        /// <summary>
        /// Database isolation level
        /// </summary>
        IsolationLevel IsolationLevel { get; }

        /// <summary>
        /// The method used to save changes made within the transaction in the database.
        /// </summary>
        /// <returns></returns>
        Task<bool> Commit();
        /// <summary>
        /// A method that undoes the changes made in that transaction in the database in case of any error.
        /// </summary>
        Task Rollback();


        /// <summary>
        /// Manages entity-specific repository register operations on UnitOfWork.
        /// </summary>
        /// <typeparam name="T"> Entity Class. Etc: Message </typeparam>
        /// <param name="repository">The type information of the repository defined as Custom. Ex: MessageRepository </param>
        void RegisterRepository<T>(Type repository) where T : class, new();


        /// <summary>
        /// All datasets associated with the application
        /// </summary>
        Dictionary<Type, dynamic> Repositories { get; set; }


        IRepository<TEntity> Repository<TEntity>() where TEntity : Entity;

    }
}
