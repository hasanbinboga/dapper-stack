using System.Runtime.Serialization;

namespace NetFrame.Common.Exception
{
    /// <summary>
    /// DataAccess exception sınıfı
    /// </summary>
    public class DataAccessException : BaseException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataAccessException()
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">DataAccess projesi Exception mesaj bilgisi</param>
        public DataAccessException(string message) : base(message)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">DataAccess projesi Exception mesaj bilgisi</param>
        /// <param name="exception">DataAccess projesi Exception bilgisi</param>
        public DataAccessException(string message, BaseException exception) : base(message, exception)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo">DataAccess projesi Exception serialization bilgisi</param>
        /// <param name="context">DataAccess projesi Exception stream context bilgisi</param>
        public DataAccessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            PersistException(this);
        }
    }
}
