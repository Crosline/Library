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

        public static void Set(List<string> defines, BuildTargetGroup group) {
#if UNITY_2021_1_OR_NEWER
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(group), defines.ToArray());
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(group, defines.ToArray());
#endif
        }

        public static void Set(List<string> defines) {
#if UNITY_2021_1_OR_NEWER
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Android, defines.ToArray());
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.iOS, defines.ToArray());
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Standalone, defines.ToArray());
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, defines.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, defines.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines.ToArray());
#endif
        }

        public static void Set() {
#if UNITY_2021_1_OR_NEWER
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Android, DefineSymbols.ToArray());
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.iOS, DefineSymbols.ToArray());
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Standalone, DefineSymbols.ToArray());
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, DefineSymbols.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, DefineSymbols.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, DefineSymbols.ToArray());
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

        public static void Add(List<string> defines) {
            foreach (var define in defines)
                Add(define);
        }

        public static void Add(string define) {
            if (!_defineSymbols.Contains(define))
                _defineSymbols.Add(define);
            else {
                CroslineDebug.Log($"Symbol: {define} already exists.");
            }
        }

        public static void Remove(List<string> defines) {
            foreach (var define in defines)
                Remove(define);
        }

        public static void Remove(string define) {
            if (_defineSymbols.Contains(define))
                _defineSymbols.Remove(define);
            else {
                CroslineDebug.Log($"Symbol: {define} doesn't exist.");
            }
        }

        private static List<string> _defineSymbols;
    }
}
