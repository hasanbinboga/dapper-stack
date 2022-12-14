using FluentValidation;
using NetFrame.Core.Entities.Validators;
using System.ComponentModel.DataAnnotations.Schema;


namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Task entity class.
    /// </summary>
    [Table("tasks")]
    public class TaskEntity : BaseEntity
    {
        /// <summary>
        /// Title of task
        /// </summary> 
        [Column("title")]
        public string Title { get; set; }

        /// <summary>
        /// Description about task
        /// </summary> 
        [Column("taskdescription")]
        public string TaskDescription { get; set; }
        /// <summary>
        /// id of assigned user
        /// </summary>
        [Column("assigneduserid")]
        public long AssignerUserId { get; set; }
        /// <summary>
        /// Id of assignee user
        /// </summary>
        [Column("assigneeuserid")]
        public long AssigneeUserId { get; set; }
        /// <summary>
        /// Task status
        /// </summary>
        [Column("taskstatus")]
        public TaskStatus TaskStatus { get; set; }
        
        /// <summary>
        /// Task actions
        /// </summary>
        public List<TaskActionEntity> TaskActions { get; set; }

    }


    public class TaskEntityValidator : BaseEntityValidator<TaskEntity>
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
