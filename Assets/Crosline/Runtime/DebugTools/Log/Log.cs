#if CROSLINE_DEBUG || UNITY_EDITOR
#define DEBUG_EDITOR
#endif

using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
#if DEBUG_EDITOR
using System.Collections.Generic;
using UnityEngine;
#endif

namespace Crosline.DebugTools {
    public static partial class CroslineDebug {
#if DEBUG_EDITOR
        [DebuggerHidden]
        private static string DefaultPrefix {
            get {
                var stackTraceOutput = StackTraceUtility.ExtractStackTrace().Split('\n')[5];
                var spaceIndex = stackTraceOutput.IndexOf(' ') - 1;

                var prefixList = new List<char>();

                for (var i = spaceIndex; i >= 0; i--) {
                    var c = stackTraceOutput[i];

                    if (c.Equals('.'))
                        break;

                    prefixList.Add(c);
                }

                prefixList.Reverse();
                var prefixArray = prefixList.ToArray();

                return new string(prefixArray);
            }
        }
#endif

        [DebuggerHidden]
        public static void Log(string log, string prefix = "") {
#if DEBUG_EDITOR
            Console.WriteLine(BuildLog(log, prefix));
#endif
        }

        [DebuggerHidden]
        public static void LogWarning(string log, string prefix = "") {
#if DEBUG_EDITOR
            Debug.LogWarning(BuildLog(log, prefix));
#endif
        }

        [DebuggerHidden]
        public static void LogError(string log, string prefix = "") {
#if DEBUG_EDITOR
            Debug.LogError(BuildLog(log, prefix));
#endif
        }

#if DEBUG_EDITOR
        [DebuggerHidden]
        private static string GetPrefix(string prefix) {
            return string.IsNullOrEmpty(prefix) ? DefaultPrefix : prefix;
        }

        [DebuggerHidden]
        private static string GetStylizedPrefix(string prefix) {
            return $"<size=14><color=#808080>{GetPrefix(prefix)}</size></color>";
        }

        private static string BuildLog(string log, string prefix) {
            return $"[{GetPrefix(prefix)}] {log}";
        }
#endif
    }
}
