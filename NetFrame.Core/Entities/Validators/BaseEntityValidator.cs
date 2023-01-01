using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    /// <summary>
    /// Entity'nin uygunluğunu kontrol eden doğrulama sınıfı
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntityValidator<T> : AbstractValidator<T> where T : BaseEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseEntityValidator()
        {
            RuleFor(i => i.CreateTime).NotEmpty().NotNull().WithMessage("CreateTime cannot be empty.");
            RuleFor(i => i.CreateUserName)
                .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                .WithMessage("CreateUserName 255 must be less than one character.");
            RuleFor(i => i.CreateIpAddress).NotEmpty().NotNull().WithMessage("CreateIpAddress cannot be empty.");
        }
    }
}
