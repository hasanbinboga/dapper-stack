using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Log bilgilerini içeren sınıf
    /// </summary>
    [Table("errorlog")]
    public class ErrorLogEntity: LogEntity
    {
        /// <summary>
        /// Log istisna bilgisi
        /// </summary>
        [Column("exception")]
        public string Exception { get; set; }
    }
}
