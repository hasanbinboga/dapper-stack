using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetFrame.Common.Utils
{
    /// <summary>
    /// Reflection yardımcı fonksiyonlarını içeren sınıf
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Parametre olarak verilen sınıfın fieldlarını liste olarak döner
        /// </summary>
        /// <param name="t">Sınıf tipi</param>
        /// <returns>Sınıf field listesi</returns>
        public static IEnumerable<FieldInfo> GetAllFields(Type t)
        {
            if (t == null)
                return Enumerable.Empty<FieldInfo>();

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.Static | BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;
            return t.GetFields(flags).Concat(GetAllFields(t.BaseType));
        }
        /// <summary>
        /// Field adı verilen field'in fieldInfo'sunu dönmeyi sağlar.
        /// </summary>
        /// <param name="T">Sınıf tipi</param>
        /// <param name="fieldName">Field adı</param>
        /// <returns>FieldInfo</returns>
        public static FieldInfo GetNamedField(Type T, string fieldName)
        {
            if (T == null)
            {
                return null;
            }

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.Static | BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase;
            try
            {
                FieldInfo info = T.GetField(fieldName, flags);
                if (info == null)
                {
                    return GetNamedField(T.BaseType, fieldName);
                }

                return info;
            }
            catch (System.Exception)
            {
                return GetNamedField(T.BaseType, fieldName);
            }
        }
        /// <summary>
        /// Property adı verilen property'in propertyInfo'sunu dönmeyi sağlar.
        /// </summary>
        /// <param name="T">Sınıf tipi</param>
        /// <param name="propertyName">Property adı</param>
        /// <returns>PropertyInfo</returns>
        public static PropertyInfo GetNamedProperty(Type T, string propertyName)
        {
            if (T == null)
            {
                return null;
            }

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.Static | BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase;
            try
            {
                PropertyInfo info = T.GetProperty(propertyName, flags);
                if (info == null)
                {
                    return GetNamedProperty(T.BaseType, propertyName);
                }

                return info;
            }
            catch (System.Exception)
            {
                return GetNamedProperty(T.BaseType, propertyName);
            }
        }
    }
}
