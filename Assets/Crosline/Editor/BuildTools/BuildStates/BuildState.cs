using Crosline.BuildTools.Editor.BuildSteps;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildStates {
    
    [CreateAssetMenu(fileName = "Build State", menuName = "Crosline/Build/Build State Asset")]
    public class BuildState : ScriptableObject {

        public string name = "State Name";

        public BuildOptions.BuildPlatform platform = BuildOptions.BuildPlatform.Generic;

        public System.Collections.Generic.List<IBuildStep> buildSteps = new();

        public bool isCritical = false;

    }
}
