#if CROSLINE_DEBUG
using UnityEngine;
#endif

namespace Crosline.DebugTools.Log {
    public static partial class CroslineDebug {
        private static string DefaultPrefix
        {
            get
            {
                var stackTrace = StackTraceUtility.ExtractStackTrace().Split('\n');

                return stackTrace[4].Split('(')[0].Split('.')[^1];
            }
        }

        public static void Log(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.Log($"[{GetPrefix(prefix)}]: {log}");
#endif
        }

        public static void LogWarning(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.LogWarning($"[{GetPrefix(prefix)}]: {log}");
#endif
        }

        public static void LogError(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.LogError($"[{GetPrefix(prefix)}]: {log}");
#endif
        }

        private static string GetPrefix(string prefix) {
            return string.IsNullOrEmpty(prefix) ? DefaultPrefix : prefix;
        }
    }
}
