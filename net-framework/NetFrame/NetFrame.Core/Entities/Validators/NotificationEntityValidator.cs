using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    public class NotificationEntityValidator : BaseEntityValidator<NotificationEntity>
    {
        public NotificationEntityValidator()
        {
            RuleFor(s => s.Title)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                         .WithMessage("Title cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.Body)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                         .WithMessage("Body cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.ReceiverUserName)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                         .WithMessage("ReceiverUserName cannot be empty and 255 must be less than one character.");
            RuleFor(s => s.ReceiverUserFullname)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                         .WithMessage("ReceiverUserFullname cannot be empty and 255 must be less than one character.");
            RuleFor(s => s.SendTime).NotNull();
            RuleFor(s => s.SenderUserName)
                        .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                        .WithMessage("SenderUserName cannot be empty and 255 must be less than one character.");

            RuleFor(s => s.SenderUserFullname)
                      .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                      .WithMessage("SenderUserFullname cannot be empty and 255 must be less than one character.");
        }
    }
}
