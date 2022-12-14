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



        public T GetEntityById(long id)
        {
            return UnitOfWork.Repository<T>().GetById(id);
        }

        public void Update(T value)
        {
            if (Validator != null)
            {
                var validation = Validator.Validate(value);
                if (validation.IsValid)
                {
                    UnitOfWork.Repository<T>().Update(value);
                }
                else
                {
                    throw new ValidationCoreException(validation.Errors.ToString());
                }
            }
            else
            {
                UnitOfWork.Repository<T>().Update(value);

            }
        }

        public long Add(T value)
        {
            if (Validator != null)
            {
                var validation = Validator.Validate(value);
                if (validation.IsValid)
                {
                    return UnitOfWork.Repository<T>().Add(value);
                }
                else
                {
                    throw new ValidationCoreException(validation.Errors.ToString());
                }
            }
            else
            {
                return UnitOfWork.Repository<T>().Add(value);
            }
        }

        public void Passive(long id, string userName, string ipAddress)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(ipAddress))
            {
                throw new ValidationCoreException("userName and ipAdress couldn't be null or empty.");
            }

            UnitOfWork.Repository<T>().Passive(id, userName, DateTime.Now, ipAddress);
        }
        public void Passive(long[] idArray, string userName, string ipAddress)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(ipAddress))
            {
                throw new ValidationCoreException("userName and ipAdress couldn't be null or empty.");
            }

            UnitOfWork.Repository<T>().Passive(idArray.ToList(), userName, DateTime.Now, ipAddress);
        }
    }
}
