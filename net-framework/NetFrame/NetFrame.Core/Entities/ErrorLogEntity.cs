using FluentValidation;
using NetFrame.Core.Entities.Validators;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Log info class
    /// </summary>
    [Table("errorlog")]
    public class ErrorLogEntity: LogEntity
    {
        /// <summary>
        /// Log exception info
        /// </summary>
        [Column("exception")]
        public string Exception { get; set; } = string.Empty;
    }


    public class ErrorLogEntityValidator : LogEntityValidator<ErrorLogEntity>
    {
        public ErrorLogEntityValidator()
        {
            RuleFor(s => s.Exception)
                          .Must(s => !string.IsNullOrEmpty(s) && s.Length < 8000)
                          .WithMessage("Exception cannot be empty and 8000 must be less than one character.");

        }
    }
}
