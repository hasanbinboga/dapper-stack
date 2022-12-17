using FluentValidation;

namespace NetFrame.Core.Entities.Validators
{
    /// <summary>
    /// Entity'nin uygunluğunu kontrol eden doğrulama sınıfı
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityValidator<T> : AbstractValidator<T> where T : Entity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EntityValidator()
        {
            
        }
    }
}
