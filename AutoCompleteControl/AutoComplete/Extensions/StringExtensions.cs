using System;

namespace AutoCompleteControl.AutoComplete.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return obj != null;
        }

        /// <summary>
        /// Returns true if the string is not null and not String.Empty
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string stringValue)
        {
            return !string.IsNullOrEmpty(stringValue) && stringValue.Trim().Length > 0;
        }

        /// <summary>
        /// Performs a case-insensitive comparison of strings
        /// </summary>
        public static bool EqualsIgnoreCase(this string thisString, string otherString)
        {
            return thisString.Equals(otherString, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}
