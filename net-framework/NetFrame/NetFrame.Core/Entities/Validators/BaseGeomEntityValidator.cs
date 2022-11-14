using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    public class BaseGeomEntityValidator<T> : AbstractValidator<T> where T : BaseGeomEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseGeomEntityValidator()
        {
            RuleFor(i => i.CreateTime).NotEmpty().NotNull().WithMessage("CreateTime cannot be empty.");
            RuleFor(i => i.CreateUserName)
                .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                .WithMessage("CreateUserName 255 must be less than one character.");
            RuleFor(i => i.CreateIpAddress).NotEmpty().NotNull().WithMessage("CreateIpAddress cannot be empty.");
        }
    }
}
