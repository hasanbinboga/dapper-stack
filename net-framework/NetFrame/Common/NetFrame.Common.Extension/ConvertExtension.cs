using NetFrame.Common.Exception;

namespace NetFrame.Common.Extension
{
    /// <summary>
    /// Data conversion operations class
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// Function to convert incoming object type object to Int32
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>Result converted to Int32</returns>
        public static int ToInt32(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (System.Exception ex)
            {
                throw new ExtensionException("Conversion error", ex);
            }
        }

        /// <summary>
        /// Function to convert incoming object type object to double
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>Result converted to double</returns>
        public static double ToDouble(this object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch (System.Exception ex)
            {
                throw new ExtensionException("Conversion error", ex);
            }
        }

        /// <summary>
        /// Function to convert incoming object type object to decimal
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Result converted to decimal</returns>
        public static decimal ToDecimal(this object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (System.Exception ex)
            {
                throw new ExtensionException("Conversion error", ex);
            }
        }

        /// <summary>
        /// Function to convert incoming object type object to bool
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Result converted to Boolean</returns>
        public static bool ToBoolean(this object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch (System.Exception ex)
            {
                throw new ExtensionException("Conversion error", ex);
            }
        }

        /// <summary>
        /// Function to convert incoming object type object to long
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Result converted to long</returns>
        public static long ToLong(this object obj)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch (System.Exception ex)
            {
                throw new ExtensionException("Conversion error", ex);
            }
        }
    }
}