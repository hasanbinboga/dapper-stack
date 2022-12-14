using NetFrame.Common.Exception;
using NetFrame.Core;
using NetFrame.Infrastructure.Repositories;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace NetFrame.Infrastructure
{
    /// <summary>
    /// It offers to perform all transactions to be made with the database through a single channel 
    /// and to keep them in memory. In this way, it ensures that the transactions are performed in 
    /// batches and that they can be retrieved in case of error.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<Type, dynamic> _repositories;

        /// <summary>
        ///All datasets associated with the application
        /// </summary>
        public Dictionary<Type, dynamic> Repositories
        {
            get { return _repositories; }
            set { _repositories = value; }
        }

        private bool _disposed;

        private DbConnection _connection;

        /// <summary>
        /// database connection
        /// </summary>
        public DbConnection Connection
        {
            get { return _connection; }
            private set { _connection = value; }
        }

        private DbTransaction _transaction;

        /// <summary>
        /// database transaction
        /// </summary>
        public DbTransaction Transaction
        {
            get { return _transaction; }
            private set { _transaction = value; }
        }

        private IsolationLevel _isolationLevel;

        /// <summary>
        /// Database transaction isolation level
        /// </summary>
        public IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
            set { _isolationLevel = value; }
        }

        /// <summary>
        /// A connection is created to operate in the database.
        /// </summary>
        /// <param name="connection">Db Connection</param>
        /// <param name="isolationLevel">Data access isolation level</param>
        /// <exception cref="DataAccessException">This exception is thrown in case of an error in data access.</exception>
        public UnitOfWork(DbConnection connection, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _connection = connection;
            _isolationLevel = isolationLevel;
            _repositories = new Dictionary<Type, dynamic>();

            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction(isolationLevel);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An error occurred in the database connection.",
                    new BaseException("An error occurred in the database connection.", ex));
            }
        }

        /// <summary>
        /// With the connection sentence, a connection is created to operate in the database.
        /// </summary>
        /// <param name="connection">Veri tabanı bağlantısı</param>
        /// <param name="isolationLevel">Veri erişim izolasyon seviyesi</param>
        public UnitOfWork(string connectionString, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
            : this(new NpgsqlConnection(connectionString), isolationLevel)
        {

        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity

        {
            //if (ServiceLocator.IsLocationProviderSet)
            //{
            //    return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            //}

            if (_repositories == null)
                _repositories = new Dictionary<Type, dynamic>();

            if (_repositories.ContainsKey(typeof(TEntity)))
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];

            var repositoryType = typeof(Repository<>);
            _repositories.Add(typeof(TEntity), Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), this));

            return _repositories[typeof(TEntity)];
        }

        /// <summary>
        /// The method used to save changes made within the transaction in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Commit()
        {
            try
            {
                await _transaction.DisposeAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw new DataAccessException("Error occurred while saving transactions in database", new BaseException("", ex));
                //return false;
                //throw;
            }
            finally
            {
               await Reset();
            }
        }

      




        /// <summary>
        /// A method that undoes the changes made in that transaction in the database in case of any error.
        /// </summary>
        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
             await Reset();
        }

        /// <summary>
        ///Creates a new process by terminating the current process.
        /// </summary>
        public async Task Reset()
        {
            await _transaction.DisposeAsync();
            _transaction = await _connection.BeginTransactionAsync();
        }


        

        async Task RealDispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        await _transaction.DisposeAsync();
                    }
                    if (_connection != null)
                    {
                        await _connection.DisposeAsync();
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Task.Run(async ()=> await RealDispose(false));
        }

        /// <summary>
        ///Manages entity-specific repository register operations on UnitOfWork.
        /// </summary>
        /// <typeparam name="T"> entity type. ex: Message </typeparam>
        /// <param name="repository">Type information of repository defined as Custom. Ex: MessageRepository </param>
        public virtual void RegisterRepository<T>(Type repository) where T : class, new()
        {
            var repositoryType = typeof(Repository<>);

            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories.Add(typeof(T), Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this));
            }
            else
            {
                _repositories[typeof(T)] = Activator.CreateInstance(repositoryType.MakeGenericType(repositoryType), this);
            }
        }

        
        /// <summary>
        /// free ram
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            await RealDispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
