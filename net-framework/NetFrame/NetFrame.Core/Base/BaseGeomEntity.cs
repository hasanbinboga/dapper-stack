using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core
{
    /// <summary>
    /// It is the main data class derived FROM the Entity class. Unlike BaseEntity, there are GeomWkt and Geometry porperties.
    /// </summary>
    public class BaseGeomEntity : GeomEntity
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

        [StringLength(100, ErrorMessage = "")]
        [Column("createipaddress")]
        public string CreateIpAddress { get; set; }

        /// <summary>
        /// Id of the user who added the data
        /// </summary>
        [StringLength(255, ErrorMessage = "")]
        [Column("createusername")]
        public string CreateUserName { get; set; }


        /// <summary>
        /// User Id information that updated the data
        /// </summary>
        [StringLength(255, ErrorMessage = "")]
        [Column("updateusername")]
        public string UpdateUserName { get; set; }

        /// <summary>
        /// User IP information updating data
        /// </summary>
        [StringLength(100, ErrorMessage = "")]
        [Column("updateipaddress")]
        public string UpdateIpAddress { get; set; }


        /// <summary>
        /// Entity's data history information
        /// </summary>
        [StringLength(100, ErrorMessage = "")]
        [Column("history")]
        public  dynamic[] History { get; set; }
    }
}
