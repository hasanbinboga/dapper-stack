using FluentValidation;
using NetFrame.Common.Exception;
using NetFrame.Core;
using NetFrame.Core.Entities.Validators;
using System;
using System.Linq;


namespace NetFrame.Infrastructure.DataAcces
{
    public abstract class BaseEntityDataAccess<T> where T : BaseEntity 
    {
        public UnitOfWork UnitOfWork { get; }

        public BaseEntityValidator<T> Validator { get; }
        public BaseEntityValidator<T> UpdateValidator { get; }

        protected BaseEntityDataAccess(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork; 

            Validator = new BaseEntityValidator<T>();
            UpdateValidator = new BaseEntityValidator<T>();


            UpdateValidator.RuleFor(s => s.UpdateUserName)
                        .Must(s => !string.IsNullOrEmpty(s) && s.Length <= 255)
                        .WithMessage("UpdateUserName must not be empty and 255 is max length.");
            UpdateValidator.RuleFor(s => s.UpdateIpAddress)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length <= 25)
                         .WithMessage("UpdateIpAddress must not be empty and 25 is max length.");
            UpdateValidator.RuleFor(i => i.UpdateTime).NotEmpty().NotNull().WithMessage("UpdateTime is required.");
        }

     

        public T GetEntityById(long id)
        {
            return UnitOfWork.Repository<T>().GetById(id);
        } 

        public void Update(T value)
        {
            var validation = UpdateValidator.Validate(value);
            if (validation.IsValid)
            {
                UnitOfWork.Repository<T>().Update(value);
            }
            else
            {
                throw new ValidationCoreException(validation.Errors.ToString());
            }
        }

        public long Add(T value)
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
