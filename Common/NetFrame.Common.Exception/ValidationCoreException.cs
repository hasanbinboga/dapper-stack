using System.Runtime.Serialization;

namespace NetFrame.Common.Exception
{
    /// <summary>
    /// doğrulama exception sınıfı
    /// </summary>
    public class ValidationCoreException : BaseException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ValidationCoreException()
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Doğrulama Exception mesaj bilgisi</param>
        public ValidationCoreException(string message) : base(message)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Doğrulama Exception mesaj bilgisi</param>
        /// <param name="exception">Doğrulama katmanı Exception bilgisi</param>
        public ValidationCoreException(string message, BaseException exception) : base(message, exception)
        {
            PersistException(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializationInfo">Doğrulama Exception serialization bilgisi</param>
        /// <param name="context">Doğrulama Exception stream context bilgisi</param>
        public ValidationCoreException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            PersistException(this);
        }
    }
}
