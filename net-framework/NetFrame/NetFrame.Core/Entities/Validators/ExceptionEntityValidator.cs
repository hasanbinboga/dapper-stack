using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    public class ExceptionEntityValidator:BaseEntityValidator<ExceptionEntity>
    {
        public ExceptionEntityValidator()
        {
            RuleFor(s => s.ModuleName)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                         .WithMessage("ModuleName cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.ClassName)
                        .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                        .WithMessage("ClassName cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.Description)
                        .Must(s => string.IsNullOrEmpty(s) || s.Length < 2000)
                        .WithMessage("Description  2000 must be less than one character.");

            RuleFor(s => s.Stacktrace)
                          .Must(s => !string.IsNullOrEmpty(s) && s.Length < 8000)
                          .WithMessage("Stacktrace cannot be empty and 8000 must be less than one character.");

            RuleFor(s => s.ExceptionType)
                        .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                        .WithMessage("ExceptionType cannot be empty and 2000 must be less than one character.");
        }
    }
}
