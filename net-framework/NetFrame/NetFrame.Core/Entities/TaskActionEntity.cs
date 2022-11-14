using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Görev yapılma bilgilerini içeren sınıf.
    /// </summary>
    [Table("taskactions")]
    public class TaskActionEntity : BaseEntity
    {
        /// <summary>
        /// Görev Referans Id si
        /// </summary>
        [Column("taskref")]
        public long TaskRef { get; set; }

        /// <summary>
        /// Görev yapılma tarih bilgisi
        /// </summary>
        [Column("actiontime")]
        public DateTime ActionTime { get; set; }

        /// <summary>
        /// Görev açıklama bilgisi max 2000 karakter
        /// </summary>
        [Column("actiondescription")] 
        public string ActionDescription { get; set; }

        /// <summary>
        /// Görev önceki durum bilgisi
        /// </summary>
        [Column("previousstatus")]
        public TaskStatus PreviousStatus { get; set; }

        /// <summary>
        /// Görev yeni durum bilgisi    
        /// </summary>
        [Column("newstatus")]
        public TaskStatus NewStatus { get; set; }
    }
}
