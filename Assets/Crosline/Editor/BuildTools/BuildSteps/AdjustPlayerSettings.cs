using UnityEditor;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class AdjustPlayerSettings : BuildStep {
        public override bool Execute() {
            PlayerSettings.bundleVersion = CommonBuilder.Instance.buildConfig.version;
            PlayerSettings.SetScriptingBackend(CommonBuilder.Instance.BuildPlatform.ToBuildTargetGroup(), CommonBuilder.Instance.buildConfig.backend);

            switch (CommonBuilder.Instance.BuildPlatform) {
                case BuildOptions.BuildPlatform.Android:
                    PlayerSettings.Android.targetArchitectures = ((AndroidBuildConfigAsset) CommonBuilder.Instance.buildConfig).architecture;

                    break;

                case BuildOptions.BuildPlatform.IOS:
                    if (!CommandLineHelper.ArgumentTrue("cleanBuild")) {
                        PlayerSettings.SetIncrementalIl2CppBuild(CommonBuilder.Instance.BuildPlatform.ToBuildTargetGroup(), true);
                    }

                    break;

                case BuildOptions.BuildPlatform.Windows:
                    break;

                case BuildOptions.BuildPlatform.MacOS:
                    break;

                case BuildOptions.BuildPlatform.Linux:
                    break;

            }

            return true;
        }
    }
}
