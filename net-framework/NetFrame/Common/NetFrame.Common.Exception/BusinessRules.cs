namespace NetFrame.Common.Exception
{
    /// <summary>
    /// Static class that keeps a list of business rule-related errors
    /// </summary>
    public static class BusinessRules
    {
        static BusinessRules()
        {
            BusinessExceptions = new List<BusinessException>();
        }

        /// <summary>
        /// List of business rule errors
        /// </summary>
        [ThreadStatic]
        public static List<BusinessException> BusinessExceptions;

        /// <summary>
        /// Add new Business rule error
        /// </summary>
        /// <param name="businessException">Business rule error</param>
        public static void Add(BusinessException businessException)
        {
            if (BusinessExceptions == null)
            {
                BusinessExceptions = new List<BusinessException>();
            }

            BusinessExceptions.Insert(0, businessException);
        }
    }
}
