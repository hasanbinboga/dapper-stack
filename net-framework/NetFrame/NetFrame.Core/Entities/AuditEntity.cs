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
        public string DataModel { get; set; }
        /// <summary>
        /// The value of the audit received data before the change is json
        /// </summary>
        [Column("valuebefore")]
        public string ValueBefore { get; set; }
        /// <summary>
        /// The value of audit received data after change is json
        /// </summary>
        [Column("valueafter")]
        public string ValueAfter { get; set; }
        /// <summary>
        /// field-based change information as json
        /// </summary>
        [Column("changes")]
        public string Changes { get; set; }
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
        ///  Transaction Id Information where the change was made
        /// </summary>
        [Column("transactionid")]
        public long TransactionId { get; set; }

        /// <summary>
        ///  Information from which application the change was made
        /// </summary>
        [Column("applicationname")]
        public string ApplicationName { get; set; }

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
        public string CreateIpAddress { get; set; }

        /// <summary>
        /// Create UserName
        /// </summary>
        [StringLength(255, ErrorMessage = "")]
        [Column("createusername")]
        public string CreateUserName { get; set; }
    }
}
