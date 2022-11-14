using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    public class AuditEntityValidator : AbstractValidator<AuditEntity>
    {
        public AuditEntityValidator()
        {
            RuleFor(i => i.CreateTime).NotEmpty().NotNull().WithMessage("CreateTimecannot be empty.");
            RuleFor(i => i.CreateUserName)
                .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                .WithMessage("CreateUserName 255 must be less than one character.");
            RuleFor(i => i.CreateIpAddress).NotEmpty().NotNull().WithMessage("CreateIpAddresscannot be empty.");
            RuleFor(i => i.ActionType).NotNull().IsInEnum();
            RuleFor(i => i.KeyFieldId).NotNull();
            RuleFor(i => i.DataModel)
                .Must(s => string.IsNullOrEmpty(s) || s.Length < 255)
                .WithMessage("DataModel 255 must be less than one character.");
        }
    }
}
