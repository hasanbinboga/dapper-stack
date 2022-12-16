using FluentValidation;
using NetFrame.Common.Exception;
using NetFrame.Core;


namespace NetFrame.Infrastructure.DataAcces
{
    public abstract class EntityDataAccess<T> where T : Entity
    {
        public UnitOfWork UnitOfWork { get; }

        public AbstractValidator<T> Validator { get; }

        protected EntityDataAccess(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected EntityDataAccess(UnitOfWork unitOfWork, AbstractValidator<T> validator)
        {
            UnitOfWork = unitOfWork;

            Validator = validator;
        }



        public async Task<T> GetEntityById(long id)
        {
            return await UnitOfWork.Repository<T>().GetById(id);
        }

        public async Task Update(T value)
        {
            if (Validator != null)
            {
                var validation = Validator.Validate(value);
                if (validation.IsValid)
                {
                    await UnitOfWork.Repository<T>().Update(value);
                }
                else
                {
                    throw new ValidationCoreException(validation.Errors.ToString());
                }
            }
            else
            {
                await UnitOfWork.Repository<T>().Update(value);

            }
        }

        public async Task<long> Add(T value)
        {
            if (Validator != null)
            {
                var validation = Validator.Validate(value);
                if (validation.IsValid)
                {
                    return await UnitOfWork.Repository<T>().Add(value);
                }
                else
                {
                    throw new ValidationCoreException(validation.Errors.ToString());
                }
            }
            else
            {
                return await UnitOfWork.Repository<T>().Add(value);
            }
        }

        public async Task Passive(long id, string userName, string ipAddress)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(ipAddress))
            {
                throw new ValidationCoreException("userName and ipAdress couldn't be null or empty.");
            }

            await UnitOfWork.Repository<T>().Passive(id, userName, DateTime.Now, ipAddress);
        }
        public async Task Passive(long[] idArray, string userName, string ipAddress)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(ipAddress))
            {
                throw new ValidationCoreException("userName and ipAdress couldn't be null or empty.");
            }

            await UnitOfWork.Repository<T>().Passive(idArray.ToList(), userName, DateTime.Now, ipAddress);
        }
    }
}
