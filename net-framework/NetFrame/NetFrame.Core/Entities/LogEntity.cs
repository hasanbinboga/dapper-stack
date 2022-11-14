using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Log temel bilgilerini içeren sınıf
    /// </summary>

    public class LogEntity : Entity
    {
        /// <summary>
        /// Service-WebApi URI bilgisi 
        /// </summary>
         
        [Column("serviceuri")]
        public string ServiceUri { get; set; }

        /// <summary>
        /// Service-WebApi' nin Controller sınıfı bilgisi.
        /// </summary>
        
        [Column("controller")]
        public string Controller { get; set; }

        /// <summary>
        /// Service-WebApi' nin çalışan metodu.
        /// </summary>
        [Column("method")] 
        public string Method { get; set; }

        /// <summary>
        /// Kullanıcıdan sunucuya gelen talep bilgisi
        /// </summary>
        [Column("requesturi")]
        public string RequestUri { get; set; }

        /// <summary>
        /// Kullanıcıdan sunucuya gelen talebe bağlı json object bilgisi
        /// </summary>
        [Column("requestjsonobject")]
        public string RequestJsonObject { get; set; }

        
        /// <summary>
        /// Kullanıcıdan sunucuya gelen talebe bağlı XML object bilgisi
        /// </summary>
        [Column("requestxmlobject")]
        public string RequestXmlObject { get; set; }


        /// <summary>
        /// Sunucunun talebe karşı döndüğü durum kodu bilgisi
        /// </summary>
        [Column("statuscode")]
        public HttpStatusCode StatusCode { get; set; }

        
        /// <summary>
        /// Kullanıcıdan sunucuya gelen talebe bağlı json object bilgisi
        /// </summary>
        [Column("responsejsonobject")]
        public string ResponseJsonObject { get; set; }

        /// <summary>
        /// Kullanıcıdan sunucuya gelen talebe bağlı XML object bilgisi
        /// </summary>
        [Column("responsexmlobject")]
        public string ResponseXmlObject { get; set; }

        /// <summary>
        /// Kullanıcı Adı bilgisi
        /// </summary> 
        [Column("username")] 
        public string UserName { get; set; }

        /// <summary>
        /// Kullanıcının Ip Adres bilgisi
        /// </summary>
        [Column("useripaddress")] 
        public string UserIpAddress { get; set; }


        /// <summary>
        /// Log un kayıt zamanı
        /// </summary>
        [Column("createtime")] 
        public DateTime CreateTime { get; set; }
    }
}
