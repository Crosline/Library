using System.Collections.Generic;
using Crosline.DebugTools;
using UnityEditor;
using UnityEditor.Build;

namespace Crosline.UnityTools.Editor {
    public class DefineSymbolHelper {

        private const char ARGS_SEPARATOR = ';';
        
        private const BuildTargetGroup DefaultBuildTargetGroup = BuildTargetGroup.Standalone;

        public static List<string> DefineSymbols {
            get {
                _defineSymbols ??= new List<string>();

                if (_defineSymbols.Count == 0) {
                    string defineSymbols;
#if UNITY_2021_1_OR_NEWER
                    defineSymbols = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(DefaultBuildTargetGroup));
#else
                    defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(DefaultBuildTargetGroup);
#endif
                    _defineSymbols = new List<string>(defineSymbols.Split(ARGS_SEPARATOR));
                }

                if (_defineSymbols.Count == 0)
                    CroslineDebug.LogError("Could not find any custom command line arguments.");

                return _defineSymbols;
            }
        }

        public static void Set(List<string> defines, BuildTargetGroup group = BuildTargetGroup.Standalone) {
#if UNITY_2021_1_OR_NEWER
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(group), defines.ToArray());
#else
                PlayerSettings.SetScriptingDefineSymbolsForGroup(group, defines.ToArray());
#endif
        }

        public static void Set(Dictionary<BuildTargetGroup, string> defines) {
            foreach (var definePair in defines) {
#if UNITY_2021_1_OR_NEWER
                PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(definePair.Key), definePair.Value);
#else
                PlayerSettings.SetScriptingDefineSymbolsForGroup(definePair.Key, definePair.Value);
#endif
            }
        }

        public static void Add(List<string> defines, BuildTargetGroup group = BuildTargetGroup.Standalone) {
            foreach (var define in defines) defines.Add(define);
        }

        public static void Add(string define) {
            _defineSymbols.Add(define);
        }

        private static List<string> _defineSymbols;
    }
}
