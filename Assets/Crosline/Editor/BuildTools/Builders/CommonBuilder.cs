using System.Collections.Generic;
using Crosline.BuildTools.Editor.BuildStates;

namespace Crosline.BuildTools.Editor {
    public abstract class CommonBuilder : IBuilder {

        public static CommonBuilder Instance => _instance;

        private static CommonBuilder _instance = null;
        
        protected BuildOptions.BuildPlatform _buildPlatform;

        protected List<BuildState> _buildStates;

        protected CommonBuilder() {
            _instance = this;
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Generic;
        }
        
        protected CommonBuilder(List<BuildState> states, BuildOptions.BuildPlatform buildPlatform) {
            _buildStates = states;
            _buildPlatform = buildPlatform;
        }


        public void Execute() {
            
            foreach (var buildState in _buildStates) {
                if (!buildState.platform.HasFlag(_buildPlatform)) {
                    UnityEngine.Debug.Log($"[Builder] Error: Build State {buildState.name}'s is not compatible with {_buildPlatform}.");
                }
                
                var platform = buildState.platform;
                var buildSteps = buildState.buildSteps;
                
                foreach (var buildStep in buildSteps) {
                    if (!buildStep.platform.HasFlag(_buildPlatform)) {                    
                        UnityEngine.Debug.Log($"[Builder] Error: Build Step {buildStep.name}'s is not compatible with {_buildPlatform}.");
                    }
                    
                    buildStep.Execute(platform);
                }
            }
        }
    }
}
