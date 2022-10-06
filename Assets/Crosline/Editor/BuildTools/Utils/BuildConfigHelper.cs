using UnityEditor;
using UnityEngine;
using static Crosline.BuildTools.BuildOptions;

namespace Crosline.BuildTools.Editor {
    public static class BuildConfigConverter {

        public static BuildTarget ToBuildTarget(this BuildPlatform platform) {
            switch (platform) {
                case BuildPlatform.Android:
                    return BuildTarget.Android;

                case BuildPlatform.IOS:
                    return BuildTarget.iOS;

                case BuildPlatform.Windows:
                    return BuildTarget.StandaloneWindows64;

                case BuildPlatform.MacOS:
                    return BuildTarget.StandaloneOSX;

                case BuildPlatform.Linux:
                    return BuildTarget.StandaloneLinux64;
            }

            return default;
        }

        public static BuildTargetGroup ToBuildTargetGroup(this BuildPlatform platform) {
            switch (platform) {
                case BuildPlatform.Android:
                    return BuildTargetGroup.Android;

                case BuildPlatform.IOS:
                    return BuildTargetGroup.iOS;

                case BuildPlatform.Windows:
                    return BuildTargetGroup.Standalone;

                case BuildPlatform.MacOS:
                    return BuildTargetGroup.Standalone;

                case BuildPlatform.Linux:
                    return BuildTargetGroup.Standalone;

                case BuildPlatform.Generic:
                    return BuildTargetGroup.Unknown;

            }

            return default;
        }

        public static UIOrientation ToUIOrientation(this ScreenOrientation orientation) {
            switch (orientation) {
                case ScreenOrientation.Portrait:
                    return UIOrientation.Portrait;
                case ScreenOrientation.LandscapeLeft:
                    return UIOrientation.LandscapeLeft;
                case ScreenOrientation.LandscapeRight:
                    return UIOrientation.LandscapeRight;
                case ScreenOrientation.AutoRotation:
                    return UIOrientation.AutoRotation;
                case ScreenOrientation.PortraitUpsideDown:
                    return UIOrientation.PortraitUpsideDown;
            }

            return default;
        }

        public static ScriptingImplementation ToScriptingImplementation(this ScriptingBackend backend) {
            switch (backend) {
                case ScriptingBackend.IL2CPP:
                    return ScriptingImplementation.IL2CPP;
                case ScriptingBackend.Mono:
                    return ScriptingImplementation.Mono2x;
            }

            return default;
        }

        public static AndroidArchitecture ToAndroidArchitecture(this Architecture architecture) {
            switch (architecture) {
                case Architecture.ARMv7:
                    return AndroidArchitecture.ARMv7;
                case Architecture.ARM64:
                    return AndroidArchitecture.ARM64;
            }

            return default;
        }


        public static bool HasFlagAny<T>(this T value, T flag) where T : System.Enum {
            return value.HasFlag(flag) || flag.HasFlag(value);
        }

    }
}
