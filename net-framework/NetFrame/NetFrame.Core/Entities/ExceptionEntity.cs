using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Entity için Exception özelliklerini tutar.
    /// </summary>
    [Table("exceptions")]
    public class ExceptionEntity : BaseEntity
    {
        /// <summary>
        /// Exception alınan modül adı max 2000 karakter
        /// </summary>
        [Column("modulename")]
        public string ModuleName { get; set; }
        /// <summary>
        /// Exception alınan sınıf adı max 2000 karakter
        /// </summary>
        [Column("classname")]
        public string ClassName { get; set; }
        /// <summary>
        /// Exception code bilgisi
        /// </summary>
        [Column("exceptioncode")]
        public long ExceptionCode { get; set; }
        /// <summary>
        /// Exception açıklama bilgisi max 2000 karakter
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Exception stack trace bilgisi max 8000 karakter
        /// </summary>
        [Column("stacktrace")]
        public string Stacktrace { get; set; }
        /// <summary>
        /// Exception tip bilgisi max 2000 karakter
        /// </summary> 
        [Column("exceptiontype")]
        public string ExceptionType { get; set; }
    }
}
