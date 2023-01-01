using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core
{
    /// <summary>
    /// Master data class derived FROM Entity class
    /// </summary>

    public class BaseEntity : Entity
    {
        /// <summary>
        /// Data added date information
        /// </summary>
        [Column("create_time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Data update date information
        /// </summary>
        [Column("update_time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// User IP information that added the data
        /// </summary> 
        [Column("create_ip_address")]
        public string CreateIpAddress { get; set; } = string.Empty;

        /// <summary>
        /// Id of the user who added the data
        /// </summary> 
        [Column("create_user_name")]
        public string CreateUserName { get; set; } = string.Empty;


        /// <summary>
        /// User Id information that updated the data
        /// </summary> 
        [Column("update_user_name")]
        public string UpdateUserName { get; set; } = string.Empty;

        /// <summary>
        /// User IP information updating data
        /// </summary> 
        [Column("update_ip_address")]
        public string UpdateIpAddress { get; set; } = string.Empty;


        /// <summary>
        /// Entity's data history information
        /// </summary> 
        [Column("history")]
        public dynamic[] History { get; set; } = new dynamic[] { };
    }
}
