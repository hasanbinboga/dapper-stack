using System.Runtime.Serialization;

namespace NetFrame.Common.Exception
{
    /// <summary>
    /// Unknown exception class
    /// </summary>
    public class UnknownException : BaseException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UnknownException()
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Unknown Exception message information</param>
        public UnknownException(string message) : base(message)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Unknown Exception message information</param>
        /// <param name="exception">Exception</param>
        public UnknownException(string message, BaseException exception) : base(message, exception)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo">Unknown Exception serialization information</param>
        /// <param name="context">Unknown Exception stream context information</param>
        public UnknownException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            PersistException(this);
        }
    }
}
