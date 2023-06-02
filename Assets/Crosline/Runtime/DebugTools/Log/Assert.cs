#if CROSLINE_DEBUG || UNITY_EDITOR
#define DEBUG_EDITOR
#endif


#if DEBUG_EDITOR
using UnityEngine;
#endif

namespace Crosline.DebugTools {
    public static partial class CroslineDebug {
        public static void Assert(bool condition, string log, string prefix = "") {
#if DEBUG_EDITOR
            Debug.Assert(condition, $"[{GetPrefix(prefix)}] {log}");
#endif
        }

        public static void AssertException(bool condition, System.Exception e, string prefix = "") {
            if (condition)
                return;

            Assert(false, $"[{GetPrefix(prefix)}] {e.Message}");

            throw e;
        }
    }
}