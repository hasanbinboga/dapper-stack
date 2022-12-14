using FluentValidation;
using NetFrame.Core.Entities.Validators;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Notification Entity class.
    /// </summary>
    [Table("notifications")]
    public class NotificationEntity : BaseEntity
    {
        /// <summary>
        /// Notification Title max 2000 chars 
        /// </summary> 
        [Column("title")]
        public string Title { get; set; }
        /// <summary>
        /// Notification message max 2000 chars 
        /// </summary> 
        [Column("body")]
        public string Body { get; set; }
        /// <summary>
        /// Receiver user name max 255 chars 
        /// </summary>
        [Column("receiverusername")]
        public string ReceiverUserName { get; set; }

        /// <summary>
        /// Receiver user full name max 255 chars 
        /// </summary>
        [Column("receiveruserfullname")]
        public string ReceiverUserFullname { get; set; }

        /// <summary>
        /// Send time
        /// </summary>
        [Column("sendtime")]
        public DateTime SendTime { get; set; }
        /// <summary>
        /// Sender info max 255 chars 
        /// </summary>
        [Column("senderusername")]
        public string SenderUserName { get; set; }

        /// <summary>
        /// Sender user full name max 255 chars 
        /// </summary>
        [Column("senderuserfullname")]
        public string SenderUserFullname { get; set; }

        /// <summary>
        /// Read status
        /// </summary>
        [Column("readstatus")]
        public bool ReadStatus { get; set; }
    }


    public class NotificationEntityValidator : BaseEntityValidator<NotificationEntity>
    {
        public NotificationEntityValidator()
        {
            RuleFor(s => s.Title)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                         .WithMessage("Title cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.Body)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                         .WithMessage("Body cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.ReceiverUserName)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                         .WithMessage("ReceiverUserName cannot be empty and 255 must be less than one character.");
            RuleFor(s => s.ReceiverUserFullname)
                         .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                         .WithMessage("ReceiverUserFullname cannot be empty and 255 must be less than one character.");
            RuleFor(s => s.SendTime).NotNull();
            RuleFor(s => s.SenderUserName)
                        .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                        .WithMessage("SenderUserName cannot be empty and 255 must be less than one character.");

            RuleFor(s => s.SenderUserFullname)
                      .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                      .WithMessage("SenderUserFullname cannot be empty and 255 must be less than one character.");
        }
    }
}
