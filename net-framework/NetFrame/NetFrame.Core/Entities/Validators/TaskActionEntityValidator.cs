using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    public class TaskActionEntityValidator: BaseEntityValidator<TaskActionEntity>
    {
        public TaskActionEntityValidator()
        {
            RuleFor(s => s.TaskRef).NotNull();
            RuleFor(s => s.ActionTime).NotNull();
            RuleFor(s=>s.ActionDescription).Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                      .WithMessage("ActionDescription cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.PreviousStatus).NotNull().IsInEnum();
            RuleFor(s => s.NewStatus).NotNull().IsInEnum();
        }
    }
}
