using FluentValidation;
using NetFrame.Core.Entities.Validators;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Task actions entity class.
    /// </summary>
    [Table("taskactions")]
    public class TaskActionEntity : BaseEntity
    {
        /// <summary>
        /// Task referece Id
        /// </summary>
        [Column("taskref")]
        public long TaskRef { get; set; }

        /// <summary>
        /// Action time
        /// </summary>
        [Column("actiontime")]
        public DateTime ActionTime { get; set; }

        /// <summary>
        /// Action description max 2000 chars
        /// </summary>
        [Column("actiondescription")] 
        public string ActionDescription { get; set; }

        /// <summary>
        /// Previous status
        /// </summary>
        [Column("previousstatus")]
        public TaskStatus PreviousStatus { get; set; }

        /// <summary>
        /// New Status  
        /// </summary>
        [Column("newstatus")]
        public TaskStatus NewStatus { get; set; }
    }

    public class TaskActionEntityValidator : BaseEntityValidator<TaskActionEntity>
    {
        public TaskActionEntityValidator()
        {
            RuleFor(s => s.TaskRef).NotNull();
            RuleFor(s => s.ActionTime).NotNull();
            RuleFor(s => s.ActionDescription).Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                      .WithMessage("ActionDescription cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.PreviousStatus).NotNull().IsInEnum();
            RuleFor(s => s.NewStatus).NotNull().IsInEnum();
        }
    }
}
