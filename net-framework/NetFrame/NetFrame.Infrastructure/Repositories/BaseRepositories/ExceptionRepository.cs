using Dapper;
using NetFrame.Core.Entities;

namespace NetFrame.Infrastructure.Repositories
{
    /// <summary>
    /// Exception (Hata) verilerinin yönetildiği repository sınıfı
    /// </summary>
    public class ExceptionRepository : BaseRepository<ExceptionEntity>, IExceptionRepository
    {
        /// <summary>
        /// Hata repository sınıfının oluşturucu fonksiyonu
        /// </summary>
        /// <param name="unitOfWork">Exception verisinin bulunduğu context örneği (instance)</param>
        public ExceptionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Provides the registration of the listed entities to the database
        /// </summary>
        /// <param name="entities">Entity list to be saved</param>
        public override async Task<List<long>> Add(IEnumerable<ExceptionEntity> entities)
        {
            var rslt = new List<long>();
            foreach (var item in entities)
            {
                rslt.Add(await Add(item));
            }
            return rslt;
        }

        /// <summary>
        /// It provides the registration operations of the given single entity to the database.
        /// </summary>
        /// <param name="entity">Entity</param>
        public override async Task<long> Add(ExceptionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (entity.CreateUserName == null)
                throw new ArgumentNullException("entity.CreateUserName");

            try
            {
                entity.Id = await UnitOfWork.Connection.ExecuteScalarAsync<long>(
                    "INSERT INTO exceptions(id, modulename, classname, exceptioncode, description, stacktrace, exceptiontype, createtime, createusername, createipaddress) VALUES (DEFAULT,@ModuleName,@ClassName,@ExceptionCode,@Description,@Stacktrace,@ExceptionType,@CreateTime,@CreateUserName,@CreateIpAddress::inet) RETURNING id;",
                    param: entity,
                    transaction: UnitOfWork.Transaction);
                return entity.Id;
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }
        }

        /// <summary>
        /// Allows the specified entity to be updated in the database.
        /// </summary>
        /// <param name="entity">Updated version of the data requested to be updated in the database </param>
        public override async Task Update(ExceptionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await UnitOfWork.Connection.ExecuteAsync(
                    "UPDATE exceptions SET modulename=@ModuleName, classname=@ClassName, exceptioncode=@ExceptionCode, description=@Description, stacktrace=@Stacktrace, exceptiontype=@ExceptionType, updatetime=@UpdateTime, updateusername=@UpdateUserName,  updateipaddress=@UpdateIpAddress::inet  WHERE id = @Id",
                    param: entity,
                    transaction: UnitOfWork.Transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
