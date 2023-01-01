using System.Runtime.Serialization;

namespace NetFrame.Common.Exception
{
    /// <summary>
    /// Exception class for Extension Library
    /// </summary>
    public class ExtensionException : BaseException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExtensionException()
        {
            PersistException(this);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Extension Library Exception message info </param>
        public ExtensionException(string message) : base(message)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Extension Library Exception message info </param>
        /// <param name="exception">Extension Library Exception info</param>
        public ExtensionException(string message, System.Exception exception) : base(message, exception)
        {
            PersistException(this);
        }
         
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo">Extension Library Exception serialization info</param>
        /// <param name="context">Extension Library Exception stream context info</param>
        public ExtensionException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            PersistException(this);
        }
    }
}
