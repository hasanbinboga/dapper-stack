using System.Reflection;

namespace NetFrame.Common.Utils
{
    /// <summary>
    /// Class containing Reflection helper functions
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Returns the fields of the class given as a parameter as a list
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns>Class field list</returns>
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
        /// Returns the fieldInfo of the field called Field
        /// </summary>
        /// <param name="T">Type</param>
        /// <param name="fieldName">Field name</param>
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
        /// Returns propertyInfo of property called property
        /// </summary>
        /// <param name="T">Type</param>
        /// <param name="propertyName">Property name</param>
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
