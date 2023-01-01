using System.Runtime.Serialization;

namespace NetFrame.Common.Exception
{
    /// <summary>
    /// Error class to be used in errors that occur in the Utils project
    /// </summary>
    public class UtilsException : BaseException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UtilsException()
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Utils project Exception message info</param>
        public UtilsException(string message) : base(message)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Utils project Exception message info</param>
        /// <param name="exception">Utils project Exception info</param>
        public UtilsException(string message, System.Exception exception) : base(message, exception)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo">Utils project Exception serialization information</param>
        /// <param name="context">Utils project Exception stream context information</param>
        public UtilsException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            PersistException(this);
        }
    }
}
