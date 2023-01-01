using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace NetFrame.Common.Extension
{
    /// <summary>
    /// Class of actions to be performed on json
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Convert method to json
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="obj">object to be converted to json</param>
        /// <param name="includeNull">is includes null</param>
        /// <returns>Converted result to json</returns>
		public static string ToJson<T>(this T obj, bool includeNull = true)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[] { new StringEnumConverter() },
                NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }


        /// <summary>
        /// Örneğin Some.PropertyName'i "some." olarak döndürür.
        /// </summary>
        /// <param name="pascalCaseName">string</param>
        /// <returns>result converted to camel case</returns>
        private static string ConvertFullNameToCamelCase(string pascalCaseName)
        {
            var parts = pascalCaseName.Split('.')
                .Select(ConvertToCamelCase);

            return string.Join(".", parts);
        }

        /// <summary>
        /// Converts string object to camel case standard
        /// </summary>
        /// <param name="s">String to be converted</param>
        /// <returns>result converted to camel case</returns>
		private static string ConvertToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            if (!char.IsUpper(s[0]))
                return s;
            char[] chars = s.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                    break;
                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }
            return new string(chars);
        }
    }
}
