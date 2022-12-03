using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using NetFrame.Common.Exception;
using NetFrame.Core;
using NetFrame.Infrastructure.Repositories;

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

        private IDbConnection _connection;

        /// <summary>
        /// database connection
        /// </summary>
        public IDbConnection Connection
        {
            get { return _connection; }
            private set { _connection = value; }
        }

        private IDbTransaction _transaction;

        /// <summary>
        /// database transaction
        /// </summary>
        public IDbTransaction Transaction
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
        public UnitOfWork(IDbConnection connection, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
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
                throw new DataAccessException("Veri tabanı bağlantısında hata oluştu.",
                    new BaseException("Veri tabanı bağlantısında hata oluştu.", ex));
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
        public bool Commit()
        {
            try
            {
                _transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new DataAccessException("Veri tabanında işlemler kaydedilirken hata oluştu", new BaseException("", ex));
                //return false;
                //throw;
            }
            finally
            {
                Reset();
            }
        }



        /// <summary>
        /// A method that undoes the changes made in that transaction in the database in case of any error.
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();

            Reset();
        }

        /// <summary>
        ///Creates a new process by terminating the current process.
        /// </summary>
        public void Reset()
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
        }


        /// <summary>
        /// free ram
        /// </summary>
        public virtual void Dispose()
        {
            RealDispose(true);
            GC.SuppressFinalize(this);
        }

        void RealDispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            RealDispose(false);
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


    }
}
