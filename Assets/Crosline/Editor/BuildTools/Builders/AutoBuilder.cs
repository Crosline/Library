using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class AutoBuilder {
#if UNITY_EDITOR_OSX
        private const bool IsOSX = true;
#else
        private const bool IsOSX = false;
#endif

        private static Builder Builder;

        public static bool TryBuild(BuildOptions.BuildPlatform selectedBuildPlatform) {

            switch (selectedBuildPlatform) {
                case BuildOptions.BuildPlatform.Android:
                    Android();
                    break;
                case BuildOptions.BuildPlatform.IOS:
                    IOS();
                    break;
                case BuildOptions.BuildPlatform.Windows:
                    Windows();
                    break;
                case BuildOptions.BuildPlatform.MacOS:
                    MacOS();
                    break;
                case BuildOptions.BuildPlatform.Linux:
                    Linux();
                    break;
                default:
                    return false;
            }

            return true;
        }

        public static void Android() {
            if (Builder != null)
                return;

            Builder = new AndroidBuilder(
                new AndroidPreBuild(),
                new Build(),
                new AndroidPostBuild(1000)
            );

            Builder.StartBuild();
        }

        public static void IOS() {
            if (Builder != null || !IsOSX)
                return;

            Builder = new IOSBuilder(
                new IOSPreBuild(),
                new Build(),
                new IOSPostBuild(1000)
            );

            Builder.StartBuild();
        }

        public static void Windows() {
            if (Builder != null)
                return;

            Builder = new WindowsBuilder(
                new PreBuild(),
                new Build(),
                new PostBuild(1000)
            );

            Builder.StartBuild();
        }

        public static void MacOS() {
            if (Builder != null || !IsOSX)
                return;

            Builder = new MacOSBuilder();
        }

        public static void Linux() {
            if (Builder != null)
                return;

            Builder = new LinuxBuilder();
        }
    }
}