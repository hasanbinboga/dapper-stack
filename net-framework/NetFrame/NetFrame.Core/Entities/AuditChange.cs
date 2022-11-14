namespace NetFrame.Core.Entities
{
    /// <summary>
    /// The class that keeps the metadata of the changed entity and the changes as old and new when the data history is pulled from the database.
    /// </summary>
    public class AuditChange
    {
        /// <summary>
        /// Shows when data has changed
        /// </summary>
        public string DateTimeStamp { get; set; }
        /// <summary>
        /// Indicates the type of change made to the data
        /// </summary>
        public AuditActionType AuditActionType { get; set; }
        /// <summary>
        /// Shows which entity was modified
        /// </summary>
        public string AuditActionTypeName { get; set; }
        /// <summary>
        /// Shows the old and new values on the fields of the changed entity
        /// </summary>
        public List<AuditDelta> Changes { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public AuditChange()
        {
            Changes = new List<AuditDelta>();
        }
    }


}
