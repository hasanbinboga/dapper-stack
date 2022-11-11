using System.Runtime.Serialization;

namespace NetFrame.Common.Exception
{
    /// <summary>
    /// Base Exception class
    /// </summary>
    public class BaseException : System.Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseException()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public BaseException(string message) : base(message)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="exception">Base Exception</param>
        public BaseException(string message, System.Exception exception) : base(message, exception)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo">Exception serialization info</param>
        /// <param name="context">Exception stream context info</param>
        public BaseException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Persist Exception
        /// </summary>
        /// <param name="exception">Exception bilgisi</param>
        protected void PersistException(BaseException exception)
        {

        }
    }
}