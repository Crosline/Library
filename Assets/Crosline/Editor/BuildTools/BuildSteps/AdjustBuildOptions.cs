using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class AdjustBuildOptions : BuildStep {
        public override bool Execute() {

            switch (CommandLineHelper.Argument("development")) {
                case "Development":
                    Debug.Log("[Builder][AdjustBuildOptions] Debug: Development Mode is enabled.");
                    CommonBuilder.Instance.buildOptions |= UnityEditor.BuildOptions.Development;
                    break;
                case "DeepProfiling":
                    Debug.Log("[Builder][AdjustBuildOptions] Debug: DeepProfiling Mode is enabled.");
                    CommonBuilder.Instance.buildOptions |= UnityEditor.BuildOptions.Development | UnityEditor.BuildOptions.EnableDeepProfilingSupport;
                    break;
            }

            switch (CommonBuilder.Instance.buildConfig.compression) {
                case BuildOptions.Compression.CompressWithLz4:
                    Debug.Log("[Builder][AdjustBuildOptions] Debug: Lz4 compression is enabled.");
                    CommonBuilder.Instance.buildOptions |= UnityEditor.BuildOptions.CompressWithLz4;
                    break;
                case BuildOptions.Compression.CompressWithLz4HC:
                    Debug.Log("[Builder][AdjustBuildOptions] Debug: Lz4HC compression is enabled.");
                    CommonBuilder.Instance.buildOptions |= UnityEditor.BuildOptions.CompressWithLz4HC;
                    break;
            }

            switch (CommonBuilder.Instance.BuildPlatform) {
                case BuildOptions.BuildPlatform.Android:
                    break;

                case BuildOptions.BuildPlatform.IOS:
                    if (!CommandLineHelper.ArgumentTrue("replaceMode")) {
                        Debug.Log("[Builder][AdjustBuildOptions] Debug: CleanBuild Mode is enabled.");
                        CommonBuilder.Instance.buildOptions |= UnityEditor.BuildOptions.AcceptExternalModificationsToPlayer;
                    }

                    if (CommandLineHelper.ArgumentTrue("debugMode") || CommandLineHelper.ArgumentTrue("testEnvironment")) {
#if UNITY_2021_1_OR_NEWER
                        CommonBuilder.Instance.buildOptions |= UnityEditor.BuildOptions.SymlinkSources;
#else
                        CommonBuilder.Instance.buildOptions |= UnityEditor.BuildOptions.SymlinkLibraries;
#endif
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
