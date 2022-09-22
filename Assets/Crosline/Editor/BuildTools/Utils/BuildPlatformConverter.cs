using UnityEditor;
using static Crosline.BuildTools.BuildOptions;

namespace Crosline.BuildTools.Editor {
    public static class BuildPlatformConverter {

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
        
        public static  BuildTargetGroup ToBuildTargetGroup(this BuildPlatform platform) {
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

    }
}
