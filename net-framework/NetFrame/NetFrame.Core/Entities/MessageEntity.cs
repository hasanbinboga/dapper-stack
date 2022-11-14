using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Mesaj bilgilerini içeren sınıf.
    /// </summary>
    [Table("messages")]
    public class MessageEntity : NotificationEntity
    {
        /// <summary>
        /// Mesaj okunma tarih bilgisi
        /// </summary>
        [Column("readtime")]
        public DateTime? ReadTime { get; set; }
    }
}
