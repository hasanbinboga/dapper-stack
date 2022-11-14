using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    public class ErrorLogEntityValidator:LogEntityValidator<ErrorLogEntity>
    {
        public ErrorLogEntityValidator()
        {
            RuleFor(s => s.Exception)
                          .Must(s => !string.IsNullOrEmpty(s) && s.Length < 8000)
                          .WithMessage("Exception cannot be empty and 8000 must be less than one character.");
             
        }
    }
}
