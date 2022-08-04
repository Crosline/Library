#if CROSLINE_DEBUG
using UnityEngine;
#endif

namespace Crosline.DebugTools {
    public static partial class CroslineDebug {
        private static string DefaultPrefix
        {
            get
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                var testTrace = StackTraceUtility.ExtractStackTrace().Split('\n')[4];
                int spaceIndex = testTrace.IndexOf(' ');

                for (int i = spaceIndex; i >= 0; i--) {
                    char c = testTrace[i];

                    if (c.Equals('.'))
                        break;
                    sb.Append(c);
                }

                return sb.ToString().Reverse();
            }
        }

        public static void Log(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.Log($"[{GetPrefix(prefix)}] {log}");
#endif
        }

        public static void LogWarning(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.LogWarning($"[{GetPrefix(prefix)}] {log}");
#endif
        }

        public static void LogError(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.LogError($"[{GetPrefix(prefix)}] {log}");
#endif
        }
#if CROSLINE_DEBUG
        private static string Reverse(this string s) {
            char[] c = s.ToCharArray();
            System.Array.Reverse(c);

            return new string(c);
        }

        private static string GetPrefix(string prefix) {
            return string.IsNullOrEmpty(prefix) ? DefaultPrefix : prefix;
        }
#endif
    }
}
