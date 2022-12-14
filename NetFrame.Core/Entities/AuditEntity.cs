using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Audit Entity
    /// </summary>
    [Table("audit")]
    public class AuditEntity : Entity
    {
        /// <summary>
        /// id of the modified data
        /// </summary>
        [Column("keyfieldid")]
        public long KeyFieldId { get; set; }
        /// <summary>
        ///The type information of which data is kept, along with the namespace of that data
        /// </summary> 
        [Column("datamodel")]
        public string DataModel { get; set; } = string.Empty;
        /// <summary>
        /// The value of the audit received data before the change is json
        /// </summary>
        [Column("valuebefore")]
        public string ValueBefore { get; set; } = string.Empty;
        /// <summary>
        /// The value of audit received data after change is json
        /// </summary>
        [Column("valueafter")]
        public string ValueAfter { get; set; } = string.Empty;
        /// <summary>
        /// field-based change information as json
        /// </summary>
        [Column("changes")]
        public string Changes { get; set; } = string.Empty;
        /// <summary>
        /// the type of change made.
        /// public enum AuditActionType
        ///    {
        ///        Create = 1,
        ///        Update,
        ///        Delete
        ///    }
        /// </summary>
        [Column("actiontype")]
        public AuditActionType ActionType { get; set; }

        /// <summary>
        ///  Transaction Id Information WHERE the change was made
        /// </summary>
        [Column("transactionid")]
        public long TransactionId { get; set; }

        /// <summary>
        ///  Information FROM which application the change was made
        /// </summary>
        [Column("applicationname")]
        public string ApplicationName { get; set; } = string.Empty;

        /// <summary>
        /// Create Time
        /// </summary>
        [Column("createtime")]
        public DateTime CreateTime { get; set; } 

        /// <summary>
        ///  Create Ip Address
        /// </summary>

        [StringLength(100, ErrorMessage = "")]
        [Column("createipaddress")]
        public string CreateIpAddress { get; set; } = string.Empty;

        /// <summary>
        /// Create UserName
        /// </summary>
        [StringLength(255, ErrorMessage = "")]
        [Column("createusername")]
        public string CreateUserName { get; set; } = string.Empty;
    }


    public class AuditEntityValidator : AbstractValidator<AuditEntity>
    {
        public AuditEntityValidator()
        {
            RuleFor(i => i.CreateTime).NotEmpty().NotNull().WithMessage("CreateTimecannot be empty.");
            RuleFor(i => i.CreateUserName)
                .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                .WithMessage("CreateUserName 255 must be less than one character.");
            RuleFor(i => i.CreateIpAddress).NotEmpty().NotNull().WithMessage("CreateIpAddresscannot be empty.");
            RuleFor(i => i.ActionType).NotNull().IsInEnum();
            RuleFor(i => i.KeyFieldId).NotNull();
            RuleFor(i => i.DataModel)
                .Must(s => string.IsNullOrEmpty(s) || s.Length < 255)
                .WithMessage("DataModel 255 must be less than one character.");
        }
    }
}
