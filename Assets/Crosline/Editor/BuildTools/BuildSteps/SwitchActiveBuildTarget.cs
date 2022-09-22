using UnityEditor;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class SwitchActiveBuildTarget : BuildStep {

        public override bool Execute() {
            var buildTarget = CommonBuilder.Instance.BuildPlatform;
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget.ToBuildTargetGroup(), buildTarget.ToBuildTarget());

            return true;
        }
    }
}
