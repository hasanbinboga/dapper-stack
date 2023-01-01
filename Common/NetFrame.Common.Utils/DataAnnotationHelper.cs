using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Common.Utils
{
    public static class DataAnnotationHelper
    {
        #region Table
        public static string GetTableName<T>()
        {
            return GetClassAttributeValue<T, TableAttribute, string>(attr => attr.Name);
        } 
        #endregion

        #region MaxLength
        public static int GetMaxLength<T>(Expression<Func<T, string>> propExpression)
        {
            return GetPropertyAttributeValue<T, string, MaxLengthAttribute, int>(propExpression, attr => attr.Length);
        }  
        #endregion

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
