#if CROSLINE_DEBUG
using System.Collections.Generic;
using UnityEngine;
#endif

namespace Crosline.DebugTools {
    public static partial class CroslineDebug {
#if CROSLINE_DEBUG
        private static string DefaultPrefix {
            get {
                var stackTraceOutput = StackTraceUtility.ExtractStackTrace().Split('\n')[4];
                int spaceIndex = stackTraceOutput.IndexOf(' ') - 1;

                List<char> prefixList = new List<char>();

                for (int i = spaceIndex; i >= 0; i--) {
                    char c = stackTraceOutput[i];

                    if (c.Equals('.'))
                        break;

                    prefixList.Add(c);
                }

                prefixList.Reverse();
                char[] prefixArray = prefixList.ToArray();

                return new string(prefixArray);
            }
        }
#endif

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
