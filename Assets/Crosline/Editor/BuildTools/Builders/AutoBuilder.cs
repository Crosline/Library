using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class AutoBuilder {

#if UNITY_EDITOR_OSX
        private static bool isOSX = true;
#else
        private static bool isOSX = false;
#endif

        private static Builder Builder;

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
            if (Builder != null || !isOSX)
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
            if (Builder != null || !isOSX)
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
