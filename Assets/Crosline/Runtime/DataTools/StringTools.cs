using System.Linq;

namespace Crosline.DataTools {
    public static class StringTools {
        public static bool IsAscii(this char c) {
            return c <= sbyte.MaxValue;
        }

        public static bool IsAscii(this string str) {
            return str.All(c => c.IsAscii());
        }

        public static int CountSubstring(this string text, string subString) {
            return (text.Length - text.Replace(subString, "").Length) / subString.Length;
        }

        private static string Reverse(this string s) {
            char[] c = s.ToCharArray();
            System.Array.Reverse(c);

            return new string(c);
        }

        public static string GetStringBetweenSeparator(this string input, char separator) {
            var posFrom = input.IndexOf(separator);

            if (posFrom == -1)
                return string.Empty;

            var posTo = input.IndexOf(separator, posFrom + 1);

            if (posTo != -1)
                return input.Substring(posFrom + 1, posTo - posFrom - 1);

            return string.Empty;
        }
    }
}