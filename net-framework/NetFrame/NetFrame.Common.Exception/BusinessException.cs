using System.Runtime.Serialization;

namespace NetFrame.Common.Exception
{
    /// <summary>
    /// Exception class to use when there is an error with business rules
    /// </summary>
    public class BusinessException : BaseException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessException()
        {
            PersistException(this);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Business rules Exception mesaj bilgisi</param>
        public BusinessException(string message) : base(message)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">İş kuralı Exception mesaj bilgisi</param>
        /// <param name="exception">İş kuralı Exception bilgisi</param>
        public BusinessException(string message, BaseException exception) : base(message, exception)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo">İş kuralı Exception serialization bilgisi</param>
        /// <param name="context">İş kuralı Exception stream context bilgisi</param>
        public BusinessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            PersistException(this);
        }
    }
}
