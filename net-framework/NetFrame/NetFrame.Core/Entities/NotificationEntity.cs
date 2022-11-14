using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Bildirim bilgilerini içeren sınıf.
    /// </summary>
    [Table("notifications")]
    public class NotificationEntity : BaseEntity
    {
        /// <summary>
        /// Bildirim başlık bilgisi max 2000 karakter 
        /// </summary> 
        [Column("title")]
        public string Title { get; set; }
        /// <summary>
        /// Bildirim içerik bilgisi max 2000 karakter 
        /// </summary> 
        [Column("body")]
        public string Body { get; set; }
        /// <summary>
        /// Bildirim alan kullanıcı bilgisi max 255 karakter 
        /// </summary>
        [Column("receiverusername")]
        public string ReceiverUserName { get; set; }

        /// <summary>
        /// Bildirim alan kullanıcı bilgisi max 255 karakter 
        /// </summary>
        [Column("receiveruserfullname")]
        public string ReceiverUserFullname { get; set; }

        /// <summary>
        /// Bildirim gönderim tarihi
        /// </summary>
        [Column("sendtime")]
        public DateTime SendTime { get; set; }
        /// <summary>
        /// Bildirim gönderen kullanıcı bilgisi max 255 karakter 
        /// </summary>
        [Column("senderusername")]
        public string SenderUserName { get; set; }

        /// <summary>
        /// Bildirim gönderen kullanıcı bilgisi max 255 karakter 
        /// </summary>
        [Column("senderuserfullname")]
        public string SenderUserFullname { get; set; }

        /// <summary>
        /// Bildirim okunma durum bilgisi
        /// </summary>
        [Column("readstatus")]
        public bool ReadStatus { get; set; }
    }
}
