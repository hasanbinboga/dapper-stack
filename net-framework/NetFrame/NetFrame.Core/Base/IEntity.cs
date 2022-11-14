namespace NetFrame.Core
{
    /// <summary>
    /// Entity interface class
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// entity unique id information
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// Whether the entity is deleted or not
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
