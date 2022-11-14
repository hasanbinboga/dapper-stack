using System.ComponentModel;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Shows the type of change made to the data history table
    /// </summary>
    public enum AuditActionType
    {
        /// <summary>
        /// Unknown transaction type
        /// </summary>
        [Description("Unknown")]
        Unknown = 0,
        /// <summary>
        /// Indicates that the Entity value has just been added
        /// </summary>
        [Description("Create")]
        Create = 1,
        /// <summary>
        /// Indicates that the Entity value has been updated
        /// </summary>
        [Description("Update")]
        Update = 2,
        /// <summary>
        /// Indicates that the Entity value has been deleted
        /// </summary>
        [Description("Delete")]
        Delete = 3
    }
}
