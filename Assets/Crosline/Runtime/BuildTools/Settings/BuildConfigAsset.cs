using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config", menuName = "Crosline/Build/Build Config Asset")]
    public class BuildConfigAsset : ScriptableObject {

        public BuildOptions.BuildPlatform platform;

        public ScreenOrientation screenOrientation;

        public ScriptingImplementation backend;
        
        public ManagedStrippingLevel stripping = ManagedStrippingLevel.Disabled;

        public BuildOptions.BuildMode buildMode;

        public BuildOptions.ApiCompability apiCompability;
        
        public BuildOptions.Compression compression = BuildOptions.Compression.None;

        public string bundle = "com.crosline.projectName";

        public string version = "0.1";

    }
    
    [CreateAssetMenu(fileName = "Build Config - Android", menuName = "Crosline/Build/Build Config Asset - Android")]
    public class AndroidBuildConfigAsset : BuildConfigAsset {

        public BuildOptions.BuildPlatform platform = BuildOptions.BuildPlatform.Android;
        
        public AndroidArchitecture architecture;

    }
    
    [CreateAssetMenu(fileName = "Build Config - Android", menuName = "Crosline/Build/Build Config Asset - Android")]
    public class IOSBuildConfigAsset : BuildConfigAsset {

        public BuildOptions.BuildPlatform platform = BuildOptions.BuildPlatform.IOS;

    }
}
