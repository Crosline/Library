using UnityEditor;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class CheckActiveBuildTarget : BuildStep {

        public CheckActiveBuildTarget() {
            _isCritical = true;
        }
        
        public override bool Execute() {
            bool isCorrectPlatform = Builder.Instance.BuildPlatform.ToBuildTarget() == EditorUserBuildSettings.activeBuildTarget;
            
            UnityEngine.Debug.Log($"[Builder] Error: Build target did not changed to {this.Name}. Exiting.");
            
            return isCorrectPlatform;
        }
    }
}