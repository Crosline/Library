#if CROSLINE_DEBUG
using UnityEngine;
#endif

namespace Crosline.DebugTools {
    public static partial class CroslineDebug {
#if CROSLINE_DEBUG
        private static string DefaultPrefix
        {
            get
            {
                var stackTrace = StackTraceUtility.ExtractStackTrace().Split('\n');

                Debug.Log("stackTrace");
                
                return stackTrace[4].Split('(')[0].Split('.')[^1];
                return "";
            }
        }
#endif

        public static void Log(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.Log($"[{GetPrefix(prefix)}]: {log}");
#endif
        }

        public static void Warning(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.LogWarning($"[{GetPrefix(prefix)}]: {log}");
#endif
        }

        public static void LogError(string log, string prefix = "") {
#if CROSLINE_DEBUG
            Debug.LogError($"[{GetPrefix(prefix)}]: {log}");
#endif
        }
#if CROSLINE_DEBUG
        private static string GetPrefix(string prefix) {
            return string.IsNullOrEmpty(prefix) ? DefaultPrefix : prefix;
        }
        
#endif
    }
}
