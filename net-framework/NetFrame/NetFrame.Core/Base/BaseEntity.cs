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
        [Column("createtime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Data update date information
        /// </summary>
        [Column("updatetime")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// User IP information that added the data
        /// </summary> 
        [Column("createipaddress")]
        public string CreateIpAddress { get; set; }

        /// <summary>
        /// Id of the user who added the data
        /// </summary> 
        [Column("createusername")]
        public string CreateUserName { get; set; }


        /// <summary>
        /// User Id information that updated the data
        /// </summary> 
        [Column("updateusername")]
        public string UpdateUserName { get; set; }

        /// <summary>
        /// User IP information updating data
        /// </summary> 
        [Column("updateipaddress")]
        public string UpdateIpAddress { get; set; }


        /// <summary>
        /// Entity's data history information
        /// </summary> 
        [Column("history")]
        public dynamic[] History { get; set; }
    }
}
