﻿using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class AdjustPlayerSettings : BuildStep {
        public override bool Execute() {
            PlayerSettings.bundleVersion = Builder.Instance.buildConfig.version;
            var buildTargetGroup = Builder.Instance.BuildPlatform.ToBuildTargetGroup();
            PlayerSettings.SetApplicationIdentifier(buildTargetGroup, Builder.Instance.buildConfig.bundle);
            PlayerSettings.SetScriptingBackend(buildTargetGroup, Builder.Instance.buildConfig.backend.ToScriptingImplementation());

            if (Builder.Instance.buildConfig is MobileBuildConfigAsset mobileBuildConfigAsset) {
                PlayerSettings.defaultInterfaceOrientation = mobileBuildConfigAsset.screenOrientation.ToUIOrientation();
                Screen.orientation = mobileBuildConfigAsset.screenOrientation;
            }

            if (Builder.Instance.buildConfig.backend.HasFlagAny(BuildOptions.ScriptingBackend.IL2CPP)) {
                PlayerSettings.stripEngineCode = true;
            }

            switch (Builder.Instance.BuildPlatform) {
                case BuildOptions.BuildPlatform.Android:
                    if (Builder.Instance.buildConfig is AndroidBuildConfigAsset androidConfig) {
                        PlayerSettings.Android.targetArchitectures = androidConfig.architecture.ToAndroidArchitecture();
                    }

                    break;

                case BuildOptions.BuildPlatform.IOS:
                    PlayerSettings.SetIncrementalIl2CppBuild(buildTargetGroup, !CommandLineHelper.ArgumentTrue("cleanBuild"));

                    PlayerSettings.iOS.scriptCallOptimization = ScriptCallOptimizationLevel.FastButNoExceptions;
                    PlayerSettings.iOS.appleEnableAutomaticSigning = true;
                    PlayerSettings.iOS.requiresFullScreen = true;
                    PlayerSettings.iOS.hideHomeButton = true;
                    PlayerSettings.accelerometerFrequency = 0;

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
