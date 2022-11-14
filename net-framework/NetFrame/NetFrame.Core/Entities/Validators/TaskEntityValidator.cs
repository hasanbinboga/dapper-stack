using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    public class TaskEntityValidator: BaseEntityValidator<TaskEntity>
    {
        public TaskEntityValidator()
        {
            RuleFor(s => s.Title)
                        .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                        .WithMessage("Title cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.TaskDescription)
                      .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                      .WithMessage("TaskDescription cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.AssignerUserId).NotNull();

            RuleFor(s => s.TaskStatus).IsInEnum();
        }
    }
}
