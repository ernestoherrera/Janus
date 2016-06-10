using JanusCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JanusCore.Extensions
{
    public static class StringExtensions
    {
        private static Regex whitespacePattern;
        private static Regex blsJunkPattern;
        private static Regex separateWordsPattern;

        public static string ToMd5(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var enc = new ASCIIEncoding();

            return enc.GetString(new MD5CryptoServiceProvider().ComputeHash(enc.GetBytes(str)));
        }

        public static string FromBase64(this string s)
        {
            var bytes = Convert.FromBase64String(s);

            return Encoding.Unicode.GetString(bytes);
        }

        public static long ToLong(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0;

            long result = 0;
            long.TryParse(s, out result);

            return result;
        }

        public static int ToInt(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0;

            int result = 0;
            int.TryParse(s, out result);

            return result;
        }

        public static TEnum ToEnum<TEnum>(this string s, TEnum defaultValue = default(TEnum))
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("ToEnum is only intended to convert to an enum.", "TEnum");

            var intValue = s?.ToInt();

            return intValue.HasValue ? (TEnum)Enum.ToObject(typeof(TEnum), intValue.Value) : defaultValue;
        }

        public static decimal ToDecimal(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0.0m;

            var sTemp = s.Replace("$", "");
            sTemp = sTemp.Replace(",", "");
            sTemp = sTemp.Replace("%", "");
            sTemp = sTemp.Replace("(", "");
            sTemp = sTemp.Replace(")", "");

            decimal result = 0;
            decimal.TryParse(sTemp, out result);

            // If the original number had parens around it, it was negative.
            if (s.StartsWith("("))
                result = -result;

            return result;
        }

        public static DateTime? ToDateTime(this string s)
        {
            DateTime d;

            if (DateTime.TryParse(s, out d))
                return d;

            return null;
        }

        public static bool ToBool(this string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                var input = s.ToUpper();

                if (input == "Y" || input == "X" || input == "1")
                    return true;

                bool converted;

                if (bool.TryParse(input, out converted))
                    return converted;
            }

            return false;
        }

        public static Guid ToGuid(this string s)
        {
            Guid guid;

            return Guid.TryParse(s, out guid) ? guid : Guid.Empty;
        }

        public static string Truncate(this string s, int maxLength)
        {
            if (maxLength < 0)
                throw new ArgumentException("maxLength must be non-negative.");

            return s == null || s.Length < maxLength ? s : s.Substring(0, maxLength);
        }

        public static bool IsInt(this string s)
        {
            int i;

            return s != null && int.TryParse(s, out i);
        }

        public static string CollapseWhitespace(this string input)
        {
            if (whitespacePattern == null)
                whitespacePattern = new Regex(@"(\s)+", RegexOptions.Compiled);

            return string.IsNullOrEmpty(input) ? input : whitespacePattern.Replace(input, "$1");
        }

        public static string ToCleanBlsString(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;

            if (blsJunkPattern == null)
                blsJunkPattern = new Regex(@"[\?¬]\*+", RegexOptions.Compiled);

            return blsJunkPattern.Replace(s, " ").CollapseWhitespace().Trim();
        }

        public static string SeparateWords(this string s)
        {
            if (s == null)
                return null;

            if (separateWordsPattern == null)
                separateWordsPattern = new Regex(@"([A-Z])", RegexOptions.Compiled);

            return separateWordsPattern.Replace(s, " $1").Trim();
        }

        public static string StripNonAlphaNumeric(this string s, params char[] whitelist)
        {
            if (s == null)
                return null;

            var sb = new StringBuilder();

            foreach (var c in s.Where(wl => Char.IsLetterOrDigit(wl) || (whitelist != null && whitelist.Contains(wl))))
                sb.Append(c);

            return sb.ToString();
        }

        public static string StripWhitespace(this string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : Regex.Replace(input, @"\s+", string.Empty);
        }

        public static string Replace(this string s, char[] oldChars, char[] newChars)
        {
            if (s == null)
                return null;

            if (oldChars == null || newChars == null)
                return s;

            for (int i = 0; i < oldChars.Length; i++)
            {
                if (i < newChars.Length)
                    s = s.Replace(oldChars[i], newChars[i]);
                else
                    s = s.Replace(new string(oldChars[i], 1), string.Empty);
            }

            return s;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static string Bracket(this string s)
        {
            return s == null ? null : "[" + s + "]";
        }

        public static bool IsOnlyNumeric(this string s)
        {
            return !string.IsNullOrWhiteSpace(s) && s.All(c => (char.IsNumber(c)) || (char.IsWhiteSpace(c)));
        }

        public static IEnumerable<string> GetStartingSubstrings(this string word, int minLength = 1)
        {
            var substrings = new List<string>();

            if (word == null)
                return substrings.ToArray();

            word = word.Trim();

            if (string.IsNullOrEmpty(word))
                return substrings.ToArray();

            for (int i = minLength; i <= word.Length; i++)
                substrings.Add(word.Substring(0, i));

            return substrings.ToArray();
        }

        public static string ToSingleQuotedString(this Guid guid)
        {
            return Constants.SINGLE_QUOTE + guid.ToString() + Constants.SINGLE_QUOTE;
        }
    }
}
