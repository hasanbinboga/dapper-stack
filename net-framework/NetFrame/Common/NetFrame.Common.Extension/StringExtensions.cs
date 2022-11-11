using System.Text;
using System.Text.RegularExpressions;

namespace NetFrame.Common.Extension
{
    /// <summary>
    /// String extensions class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the first character of a string to uppercase
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>converted string</returns>
        public static string CapitalizeFirstCharacter(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            if (value.Length > 1)
            {
                return value[0].ToString().ToUpper() + value.Substring(1);
            }
            return value.ToUpper();
        }

        /// <summary>
        /// Capitalizes the first letter of all words in a given string.
        /// </summary>
        /// <param name="value">string value</param>
        /// <param name="seperator">saperator</param>
        /// <returns>converted string</returns>
        public static string CapitalizeAllFirstCharacters(this string value, char seperator = ' ')
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            string[] wordList = value.Split(seperator);
            StringBuilder result = new StringBuilder();
            foreach (var kelime in wordList)
            {
                if (value.Length > 1)
                {
                    result.Append(kelime[0].ToString().ToUpper());
                    result.Append(kelime.Substring(1));
                }
                else
                {
                    result.Append(kelime.ToUpper());
                }
                result.Append(" ");
            }
            return result.ToString().TrimEnd();
        }

        /// <summary>
        /// String is null or empty
        /// </summary>
        /// <param name="source">string value</param>
        /// <returns>Whether the string is empty</returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// Compares the string against a regex pattern.
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="pattern">Pattern</param>
        /// <returns>Strin is matching with pattern</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            var regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        /// <summary>
        /// Convert to Int32.
        /// </summary>
        /// <param name="value">Çevirilmek istenen string</param>
        /// <returns>Int32 sonuç</returns>
        public static int? ToInt32(this string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert to Int64.
        /// </summary>
        /// <param name="value">Çevirilmek istenen string</param>
        /// <returns>Int64 sonuç</returns>
        public static long? ToInt64(this string value)
        {
            long result;
            if (long.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert to DateTime
        /// </summary>
        /// <param name="value">çevrilmek istenen string</param>
        /// <returns>DateTime sonuç</returns>
        public static DateTime? ToDateTime(this string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert to Decimal
        /// </summary>
        /// <param name="value">çevrilmek istenen string</param>
        /// <returns>Decimal sonuç</returns>
        public static decimal? ToDecimal(this string value)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }


        /// <summary>
        /// Checks whether the given string is suitable for the E-mail format.
        /// </summary>
        /// <param name="value">Kontrol edilmek istenen string</param>
        /// <returns>E-mail formatına uygun olup olmadığı sonucu</returns>
        public static bool IsEMail(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            if (value == "asdf@adf.com" || value == "123@123.com")
            {
                return false;
            }
            try
            {
                var email = new System.Net.Mail.MailAddress(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the given string consists of numbers only.
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>The result of whether the string consists of numbers only</returns>
        public static bool IsNumberOnly(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i]))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Shortens the string to the given size and puts 3 dots at the end.
        /// </summary>
        /// <param name="value"> string value </param>
        /// <param name="size"> size of result string</param>
        /// <param name="addThreePoints">Add 3 dots to the end?</param>
        /// <returns>new string</returns>
        public static string Truncate(this string value, int size, bool addThreePoints = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            if (value.Length < size)
            {
                return value;
            }
            if (!addThreePoints)
            {
                return value.Substring(0, size);
            }
            else
            {
                if (value.Length < size - 3)
                {
                    return $"{value}...";
                }
                return $"{value.Substring(0, size - 3)}...";
            }
        }


        /// <summary>
        /// The desired function is applied to all strings on the given string list.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="enumerable">string lists</param>
        /// <param name="action">Action</param>
        /// <returns>Action applied to the given list</returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var target in enumerable)
            {
                action(target);
            }
            return enumerable;
        }


        /// <summary>
        /// method that obtains string in desired format
        /// </summary>
        /// <param name="template">template</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public static string ToFormat(this string template, params object[] parameters)
        {
            return parameters.Length == 0 ? template : string.Format(template, parameters);
        }
         
    }
}
