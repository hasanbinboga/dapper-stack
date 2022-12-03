using System;
using System.Linq;
using FluentValidation;
using NetFrame.Common.Exception;
using NetFrame.Core;
using NetFrame.Core.Entities.Validators;

namespace NetFrame.Infrastructure.DataAcces
{
    public abstract class BaseGeomDataAccess<T> where T : BaseGeomEntity
    {
        public UnitOfWork UnitOfWork { get; }

        public BaseGeomEntityValidator<T> Validator { get; }
        public BaseGeomEntityValidator<T> UpdateValidator { get; }

        protected BaseGeomDataAccess(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            Validator = new BaseGeomEntityValidator<T>();
            UpdateValidator = new BaseGeomEntityValidator<T>();


            UpdateValidator.RuleFor(s => s.UpdateUserName)
                        .Must(s => !string.IsNullOrEmpty(s) && s.Length <= 255)
                        .WithMessage("UpdateUserName boş olamaz ve 255 karakterden az olmalıdır.");
            UpdateValidator.RuleFor(s => s.UpdateIpAddress)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length <= 25)
                         .WithMessage("UpdateIpAddress boş olamaz ve 25 karakterden az olmalıdır.");
            UpdateValidator.RuleFor(i => i.UpdateTime).NotEmpty().NotNull().WithMessage("UpdateTime boş geçilemez.");
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
               return  UnitOfWork.Repository<T>().Add(value);
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
                throw new ValidationCoreException("userName ve ipAdress boş olamaz.");
            }

            UnitOfWork.Repository<T>().Passive(id, userName, DateTime.Now, ipAddress);
        }
        public void Passive(long[] idArray, string userName, string ipAddress)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(ipAddress))
            {
                throw new ValidationCoreException("userName ve ipAdress boş olamaz.");
            }

            UnitOfWork.Repository<T>().Passive(idArray.ToList(), userName, DateTime.Now, ipAddress);
        }
    }
}
