#if CROSLINE_DEBUG || UNITY_EDITOR
#define DEBUG_EDITOR
#endif

#if DEBUG_EDITOR
using System.Collections.Generic;
using UnityEngine;
#endif

namespace Crosline.DebugTools {
    public static partial class CroslineDebug {
#if DEBUG_EDITOR
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
#if DEBUG_EDITOR
            Debug.Log(BuildLog(log, prefix));
#endif
        }

        public static void LogWarning(string log, string prefix = "") {
#if DEBUG_EDITOR
            Debug.LogWarning(BuildLog(log, prefix));
#endif
        }

        public static void LogError(string log, string prefix = "") {
#if DEBUG_EDITOR
            Debug.LogError(BuildLog(log, prefix));
#endif
        }
#if DEBUG_EDITOR
        private static string GetPrefix(string prefix) {
            return string.IsNullOrEmpty(prefix) ? DefaultPrefix : prefix;
        }
        
        private static string GetStylizedPrefix(string prefix) {
            return $"<size=14><color=#808080>{GetPrefix(prefix)}</size></color>";
        }
        
        private static string BuildLog(string log, string prefix) {
            return $"[{GetPrefix(prefix)}] {log}";
        }
#endif
    }
}
