using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Crosline.BuildTools.Editor {
    public abstract class CommonBuilder : IBuilder {

        public static CommonBuilder Instance => _instance;

        private static CommonBuilder _instance = null;

        private static readonly char SEPARATOR = Path.DirectorySeparatorChar;

        [System.Obsolete] private static string MainBuildFolder = "Build"; 

#pragma warning disable CS0612
#if UNITY_ANDROID
        private static string BuildFolder => $"{MainBuildFolder}{SEPARATOR}Android";
#elif UNITY_IOS
        private static string BuildFolder => $"{MainBuildFolder}{SEPARATOR}IOS";
#elif UNITY_STANDALONE_WIN
        private static string BuildFolder => $"{MainBuildFolder}{SEPARATOR}Windows";
#endif
#pragma warning restore CS0612
        
        private static string BuildName
        {
            get
            {
                char[] charsToClean = new char[]{' ', ';', ',', '\''};
                
                var tempProductName = PlayerSettings.productName.Split(charsToClean, StringSplitOptions.RemoveEmptyEntries);
                var cleanProductName = string.Join(string.Empty, tempProductName);
                
                var commandLineArgs = Environment.GetCommandLineArgs();

                return $"{cleanProductName}-{commandLineArgs.SkipWhile(x => !x.Equals("-buildNumber")).Skip(1).FirstOrDefault()}";
            }
        }
        
        public static string BuildPath => $"{BuildFolder}{SEPARATOR}{BuildName}";

        protected BuildOptions.BuildPlatform _buildPlatform;

        protected List<BuildState> _buildStates;

        protected CommonBuilder() {
            _instance = this;
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Generic;
        }
        
        protected CommonBuilder(List<BuildState> states, BuildOptions.BuildPlatform buildPlatform) {
            _instance = this;
            _buildStates = states;
            _buildPlatform = buildPlatform;
        }


        public void Execute() {
            //
            // foreach (var buildState in _buildStates) {
            //     if (!buildState.platform.HasFlag(_buildPlatform)) {
            //         UnityEngine.Debug.Log($"[Builder] Error: Build State {buildState.name} is not compatible with {_buildPlatform}.");
            //     }
            //     
            //     var platform = buildState.platform;
            //     var buildSteps = buildState.buildSteps;
            //     
            //     foreach (var buildStep in buildSteps) {
            //         if (!buildStep.platform.HasFlag(_buildPlatform)) {                    
            //             UnityEngine.Debug.Log($"[Builder] Error: Build Step {buildStep.name} is not compatible with {_buildPlatform}.");
            //         }
            //         
            //         buildStep.Execute(platform);
            //     }
            // }
        }
    }
}
