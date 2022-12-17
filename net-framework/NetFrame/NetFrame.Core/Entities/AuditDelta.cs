namespace NetFrame.Core.Entities
{
    /// <summary>
    /// The class that holds the old and new values of the Entity on a field basis in the data history
    /// </summary>
    public class AuditDelta
    {
        /// <summary>
        /// changed entity field name
        /// </summary>
        public string FieldName { get; set; } = string.Empty;
        /// <summary>
        /// Value of the Entity field before modification
        /// </summary>
        public string OldValue { get; set; } = string.Empty;
        /// <summary>
        /// Post-change value of the Entity field
        /// </summary>
        public string NewValue { get; set; } = string.Empty;
    }
}
