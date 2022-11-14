using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    public class LogEntityValidator<T>  : AbstractValidator<T> where T : LogEntity 
    {
        public LogEntityValidator()
        {
            RuleFor(s => s.ServiceUri)
                .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                .WithMessage("ServiceUri cannot be empty and 2000 must be less than one character.");

            RuleFor(s => s.Controller)
                            .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                            .WithMessage("Controller cannot be empty and 2000 must be less than one character.");

            RuleFor(s => s.Method)
                            .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                            .WithMessage("Method cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.RequestUri)
                            .Must(s => !string.IsNullOrEmpty(s) && s.Length < 8000)
                            .WithMessage("RequestUri cannot be empty and 8000 must be less than one character.");


            RuleFor(s => s.UserName)
                            .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                            .WithMessage("UserName cannot be empty and 255 must be less than one character.");

            RuleFor(s => s.UserIpAddress).NotNull();

            RuleFor(s => s.CreateTime).NotNull();

        }

    }
}
