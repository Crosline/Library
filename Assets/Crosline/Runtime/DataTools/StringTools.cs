using System.Linq;

namespace Crosline.DataTools {
    public static class StringTools {
        public static bool IsAscii(this char c) {
            return c <= sbyte.MaxValue;
        }
        
        public static bool IsAscii(this string str) {
            return str.All(c => c.IsAscii());
        }
        
        public static int CountSubstring(this string text, string subString)
        {
            return (text.Length - text.Replace(subString, "").Length) / subString.Length;
        }
    }
}