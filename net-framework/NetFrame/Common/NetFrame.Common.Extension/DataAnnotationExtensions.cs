using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace NetFrame.Common.Extension
{
    /// <summary>
    /// Extensions for Data Annotations
    /// </summary>
    public static class DataAnnotationExtensions
    {
        #region Table
        /// <summary>
        /// Get Table Name FROM Table attribute
        /// </summary>
        /// <typeparam name="T">Type name</typeparam>
        /// <returns>Database Table Name</returns>
        public static string GetTableName<T>()
        {
            return GetClassAttributeValue<T, TableAttribute, string>(attr => attr.Name);
        }

        /// <summary>
        /// Get Table Name extension method
        /// </summary>
        /// <typeparam name="T">Type name</typeparam>
        /// <param name="instance">Instance of class</param>
        /// <returns>Database Table name</returns>
        public static string GetTableName<T>(this T instance)
        {
            return GetTableName<T>();
        }

        #endregion

        #region MaxLength

        /// <summary>
        /// Get maximum length FROM attribute
        /// </summary>
        /// <typeparam name="T">Type of instance</typeparam>
        /// <param name="propExpression">property expression</param>
        /// <returns>Maximum length value</returns>
        public static int GetMaxLength<T>(Expression<Func<T, string>> propExpression)
        {
            return GetPropertyAttributeValue<T, string, MaxLengthAttribute, int>(propExpression, attr => attr.Length);
        }

        /// <summary>
        /// Extension method for maximum lenth attribute
        /// </summary>
        /// <typeparam name="T">Type of instance</typeparam>
        /// <param name="instance">instance of class</param>
        /// <param name="propExpression">Property expression</param>
        /// <returns>Maximum length value</returns>
        public static int GetMaxLength<T>(this T instance, Expression<Func<T, string>> propExpression)
        {
            return GetMaxLength<T>(propExpression);
        }
        #endregion

        /// <summary>
        /// Get property attribute value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <typeparam name="TOut">Type output</typeparam>
        /// <typeparam name="TAttribute">Attribute type</typeparam>
        /// <typeparam name="TValue">value</typeparam>
        /// <param name="propExpression">property expression</param>
        /// <param name="valueSelector">value selector</param>
        /// <returns>Instance of TValue Type</returns>
        /// <exception cref="MissingMemberException"></exception>
        private static TValue GetPropertyAttributeValue<T, TOut, TAttribute, TValue>(Expression<Func<T, TOut>> propExpression, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var expression = (MemberExpression)propExpression.Body;
            var propInfo = (PropertyInfo)expression.Member;
            var attr = propInfo.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;

            if (attr == null)
            {
                throw new MissingMemberException(typeof(T).Name + "." + propInfo.Name, typeof(TAttribute).Name);
            }

            return valueSelector(attr);
        }

        /// <summary>
        /// Get property attribute value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <typeparam name="TAttribute">Attribute type</typeparam>
        /// <typeparam name="TValue">Value</typeparam>
        /// <param name="valueSelector">Value selector</param>
        /// <returns>Instance of TValue Type</returns>
        /// <exception cref="MissingMemberException"></exception>
        private static TValue GetClassAttributeValue<T, TAttribute, TValue>(Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var attr = typeof(T).GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;

            if (attr == null)
            {
                throw new MissingMemberException(typeof(T).Name + "." + typeof(TAttribute).Name);
            }

            return valueSelector(attr);
        }
    }
}
