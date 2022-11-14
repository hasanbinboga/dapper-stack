using System.ComponentModel.DataAnnotations.Schema;


namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Görev bilgilerini içeren sınıf.
    /// </summary>
    [Table("tasks")]
    public class TaskEntity : BaseEntity
    {
        /// <summary>
        /// Görev başlık bilgisi
        /// </summary> 
        [Column("title")]
        public string Title { get; set; }

        /// <summary>
        /// Görev açıklama bilgisi
        /// </summary> 
        [Column("taskdescription")]
        public string TaskDescription { get; set; }
        /// <summary>
        /// Görev atayan kullanıcı id bilgisi
        /// </summary>
        [Column("assigneduserid")]
        public long AssignerUserId { get; set; }
        /// <summary>
        /// Görev atanan kullanıcı id bilgisi
        /// </summary>
        [Column("assigneeuserid")]
        public long AssigneeUserId { get; set; }
        /// <summary>
        /// Görev durum bilgisi
        /// </summary>
        [Column("taskstatus")]
        public TaskStatus TaskStatus { get; set; }
        
        /// <summary>
        /// Görevin olayları
        /// </summary>
        public List<TaskActionEntity> TaskActions { get; set; }

    }
}
