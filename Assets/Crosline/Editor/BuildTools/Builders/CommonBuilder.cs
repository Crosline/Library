using System;
using System.Collections.Generic;
using System.IO;
using Crosline.BuildTools.Editor.Settings;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;

namespace Crosline.BuildTools.Editor {
    public abstract class CommonBuilder : IBuilder {

        public static CommonBuilder Instance => _instance;

        private static CommonBuilder _instance = null;

        private static readonly char SEPARATOR = Path.DirectorySeparatorChar;

        #region Build Path and Name
        [Obsolete] private static string MainBuildFolder => $"{Directory.GetParent(UnityEngine.Application.dataPath).FullName}{SEPARATOR}Builds";

#pragma warning disable CS0612
#if UNITY_ANDROID
        public static string BuildFolder => $"{MainBuildFolder}{SEPARATOR}Android";
#elif UNITY_IOS
        public static string BuildFolder => $"{MainBuildFolder}{SEPARATOR}IOS";
#elif UNITY_STANDALONE_WIN
        public static string BuildFolder => $"{MainBuildFolder}{SEPARATOR}Windows";
#else
        public static string BuildFolder => $"{MainBuildFolder}{SEPARATOR}Unknown";
#endif
#pragma warning restore CS0612

        private static string BuildName {
            get {
                var charsToClean = new char[] { ' ', ';', ',', '\'' };

                var tempProductName = PlayerSettings.productName.Split(charsToClean, StringSplitOptions.RemoveEmptyEntries);
                var cleanProductName = string.Join(string.Empty, tempProductName);

                return $"{cleanProductName}-{CommandLineHelper.Argument("buildNumber")}";
            }
        }



#if UNITY_ANDROID
        public static string BuildPath {
            get {
                var path = $"{BuildFolder}{SEPARATOR}{BuildName}";
                
                if (CommandLineHelper.Arguments["export"].Equals("true") || CommandLineHelper.Arguments["export"].Equals("1")) {
                    return $"{Directory.GetParent(UnityEngine.Application.dataPath).FullName}{SEPARATOR}{BuildFolder}{SEPARATOR}{BuildName}";
                }
                
                if (CommandLineHelper.Arguments["appBundle"].Equals("true") || CommandLineHelper.Arguments["appBundle"].Equals("1")) {
                    path += ".abb";
                }
                else {
                    path += ".apk";
                }

                return path;
            }
        }
#elif !UNITY_IOS
        public static string BuildPath => $"{BuildFolder}{SEPARATOR}{BuildName}";
#elif UNITY_STANDALONE_WIN
        public static string BuildPath => $"{BuildFolder}{SEPARATOR}{BuildName}";
#else
        public static string BuildPath => $"{BuildFolder}{SEPARATOR}{BuildName}";
#endif
        #endregion

        public BuildConfigAsset buildConfig;

        public BuildReport buildReport;

        public UnityEditor.BuildOptions buildOptions = UnityEditor.BuildOptions.None;

        public BuildOptions.BuildPlatform BuildPlatform => _buildPlatform;

        protected BuildOptions.BuildPlatform _buildPlatform = BuildOptions.BuildPlatform.Generic;

        protected List<BuildState> _buildStates = new List<BuildState>();

        protected CommonBuilder() {
            _instance = this;
            string error = null;
            buildConfig = BuildSettingsManager.TryGetConfig(ref error, customName: "BuildConfigAsset_Generic");
            buildConfig.platform = _buildPlatform;
        }

        protected CommonBuilder(List<BuildState> states, BuildOptions.BuildPlatform buildPlatform, BuildConfigAsset buildConfigAsset) {
            _instance = this;
            _buildStates = states;
            _buildPlatform = buildPlatform;
            buildConfig = buildConfigAsset;

            if (!_buildPlatform.HasFlag(buildConfig.platform)) {
                UnityEngine.Debug.Log($"[Builder] Error: Builder {nameof(CommonBuilder)} is not compatible with build config {buildConfig.name}.");
                EditorApplication.Exit(2);
            }
        }

        protected CommonBuilder(List<BuildState> states, BuildOptions.BuildPlatform buildPlatform) {
            _instance = this;
            _buildStates = states;
            _buildPlatform = buildPlatform;
        }

        public void StartBuild() {
            StartBuild(-1);
        }

        public void StartBuild(int callbackOrder) {
            foreach (var buildState in _buildStates) {
                if (!buildState.BuildPlatform.HasFlag(_buildPlatform))
                    UnityEngine.Debug.Log($"[Builder] Error: Build State {buildState.Name} is not compatible with {_buildPlatform}.");

                if (buildState.PostBuildCallback != callbackOrder)
                    continue;

                var buildSteps = buildState.BuildSteps;

                UnityEngine.Debug.Log($"[Builder] Info: Build State {buildState.Name} is started!");

                foreach (var buildStep in buildSteps) {
                    if (!buildStep.Platform.HasFlag(_buildPlatform)) {
                        UnityEngine.Debug.LogError($"[Builder] Error: Build Step {buildStep.Name} is not compatible with {_buildPlatform}.");
                    }

                    UnityEngine.Debug.Log($"[Builder] Info: Build Step {buildStep.Name} is started!");

                    if (buildStep.Execute()) {
                        UnityEngine.Debug.Log($"[Builder] Debug: Build Step {buildStep.Name} is completed!");
                    }
                    else {
                        UnityEngine.Debug.Log($"[Builder] Warning: Build Step {buildStep.Name} could not be completed!");

                        if (buildStep.IsCritical) {
                            UnityEngine.Debug.LogError($"[Builder] Error: Build Step {buildStep.Name} is Critical! Failing the build.");
                            EditorApplication.Exit(2);
                        }
                    }
                }
            }
        }

        #region PostProcessBuild Starters
        [PostProcessBuild(1)]
        public static void OnPostProcessBuild1(BuildTarget target, string pathToBuiltProject) {
            Instance.StartBuild(1);
        }

        [PostProcessBuild(100)]
        public static void OnPostProcessBuild100(BuildTarget target, string pathToBuiltProject) {
            Instance.StartBuild(100);
        }

        [PostProcessBuild(300)]
        public static void OnPostProcessBuild300(BuildTarget target, string pathToBuiltProject) {
            Instance.StartBuild(300);
        }

        [PostProcessBuild(500)]
        public static void OnPostProcessBuild500(BuildTarget target, string pathToBuiltProject) {
            Instance.StartBuild(500);
        }

        [PostProcessBuild(1000)]
        public static void OnPostProcessBuild1000(BuildTarget target, string pathToBuiltProject) {
            Instance.StartBuild(1000);
        }
        #endregion

    }
}
