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
        /// <param name="message">Business rules Exception message info </param>
        public BusinessException(string message) : base(message)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Business rules Exception message info </param>
        /// <param name="exception">Business rules Exception info</param>
        public BusinessException(string message, BaseException exception) : base(message, exception)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo">Business rules Exception serialization info</param>
        /// <param name="context">Business rules Exception stream context info</param>
        public BusinessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            PersistException(this);
        }
    }
}
