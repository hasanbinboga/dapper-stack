using FluentValidation;
using NetFrame.Core.Entities.Validators;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Exception entity class.
    /// </summary>
    [Table("exceptions")]
    public class ExceptionEntity : BaseEntity
    {
        /// <summary>
        /// Exception module name max 2000 chars
        /// </summary>
        [Column("modulename")]
        public string ModuleName { get; set; }
        /// <summary>
        /// Class name of that throw exception. max 2000 chars
        /// </summary>
        [Column("classname")]
        public string ClassName { get; set; }
        /// <summary>
        /// Exception code info
        /// </summary>
        [Column("exceptioncode")]
        public long ExceptionCode { get; set; }
        /// <summary>
        /// Exception description max 2000 chars
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Exception stack trace info max 8000 chars
        /// </summary>
        [Column("stacktrace")]
        public string Stacktrace { get; set; }
        /// <summary>
        /// Exception type max 2000 chars
        /// </summary> 
        [Column("exceptiontype")]
        public string ExceptionType { get; set; }
    }

    public class ExceptionEntityValidator : BaseEntityValidator<ExceptionEntity>
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
