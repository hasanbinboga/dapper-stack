using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core
{
    /// <summary>
    /// It holds the main Entity information.
    /// </summary>
    public class Entity : IEntity
    {
        /// <summary>
        /// Data primary key information
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Data activity information
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
