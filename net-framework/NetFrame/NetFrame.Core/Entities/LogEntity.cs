using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Class containing log infos
    /// </summary>

    public class LogEntity : Entity
    {
        /// <summary>
        /// Service-WebApi URI info
        /// </summary>
         
        [Column("serviceuri")]
        public string ServiceUri { get; set; } = string.Empty;

        /// <summary>
        /// Controller class info of Service-WebApi.
        /// </summary>

        [Column("controller")]
        public string Controller { get; set; } = string.Empty;

        /// <summary>
        /// Method name of Service-WebApi.
        /// </summary>
        [Column("method")] 
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// Request uri info
        /// </summary>
        [Column("requesturi")]
        public string RequestUri { get; set; } = string.Empty;

        /// <summary>
        /// request object as json
        /// </summary>
        [Column("requestjsonobject")]
        public string RequestJsonObject { get; set; } = string.Empty;


        /// <summary>
        /// Request object as xml
        /// </summary>
        [Column("requestxmlobject")]
        public string RequestXmlObject { get; set; } = string.Empty;


        /// <summary>
        /// Http status code
        /// </summary>
        [Column("statuscode")]
        public HttpStatusCode StatusCode { get; set; }

        
        /// <summary>
        /// Response object as json
        /// </summary>
        [Column("responsejsonobject")]
        public string ResponseJsonObject { get; set; } = string.Empty;

        /// <summary>
        /// Response object as xml
        /// </summary>
        [Column("responsexmlobject")]
        public string ResponseXmlObject { get; set; } = string.Empty;

        /// <summary>
        /// User name
        /// </summary> 
        [Column("username")] 
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// User Ip address
        /// </summary>
        [Column("useripaddress")] 
        public string UserIpAddress { get; set; } = string.Empty;


        /// <summary>
        /// Log time
        /// </summary>
        [Column("createtime")] 
        public DateTime CreateTime { get; set; }
    }

    public class LogEntityValidator<T> : AbstractValidator<T> where T : LogEntity
    {
        public LogEntityValidator()
        {
            RuleFor(s => s.ServiceUri)
                .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                .WithMessage("ServiceUri cannot be empty and 2000 must be less than one character.");

            RuleFor(s => s.Controller)
                            .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                            .WithMessage("Controller cannot be empty and 2000 must be less than one character.");

            RuleFor(s => s.Method)
                            .Must(s => !string.IsNullOrEmpty(s) && s.Length < 2000)
                            .WithMessage("Method cannot be empty and 2000 must be less than one character.");
            RuleFor(s => s.RequestUri)
                            .Must(s => !string.IsNullOrEmpty(s) && s.Length < 8000)
                            .WithMessage("RequestUri cannot be empty and 8000 must be less than one character.");


            RuleFor(s => s.UserName)
                            .Must(s => !string.IsNullOrEmpty(s) && s.Length < 255)
                            .WithMessage("UserName cannot be empty and 255 must be less than one character.");

            RuleFor(s => s.UserIpAddress).NotNull();

            RuleFor(s => s.CreateTime).NotNull();

        }

    }
}
