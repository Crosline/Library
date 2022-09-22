using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class AdjustPlayerSettings : BuildStep {
        public override bool Execute() {
            PlayerSettings.bundleVersion = CommonBuilder.Instance.buildConfig.version;
            var buildTargetGroup = CommonBuilder.Instance.BuildPlatform.ToBuildTargetGroup();
            PlayerSettings.SetApplicationIdentifier(buildTargetGroup, CommonBuilder.Instance.buildConfig.bundle);
            
            PlayerSettings.SetScriptingBackend(buildTargetGroup, CommonBuilder.Instance.buildConfig.backend);

            switch (CommonBuilder.Instance.BuildPlatform) {
                case BuildOptions.BuildPlatform.Android:
                    if (CommonBuilder.Instance.buildConfig is AndroidBuildConfigAsset androidConfig) {
                        PlayerSettings.Android.targetArchitectures = androidConfig.architecture;
                    }
                    break;

                case BuildOptions.BuildPlatform.IOS:
                    if (!CommandLineHelper.ArgumentTrue("cleanBuild")) {
                        PlayerSettings.SetIncrementalIl2CppBuild(buildTargetGroup, true);
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
