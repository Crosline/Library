using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config", menuName = "Crosline/Build/Build Config Asset")]
    public class BuildConfigAsset : ScriptableObject {

        public BuildOptions.BuildPlatform platform;

        public ScreenOrientation screenOrientation;

        public BuildOptions.BuildMode buildMode;

        public BuildOptions.ApiCompability apiCompability;

        public BuildOptions.Compression compression = BuildOptions.Compression.None;

        public string bundle = "com.crosline.projectName";

        public string version = "0.1";
        
#if UNITY_EDITOR
        public ScriptingImplementation backend;

        public ManagedStrippingLevel stripping = ManagedStrippingLevel.Disabled;
#endif

    }

    [CreateAssetMenu(fileName = "Build Config - Android", menuName = "Crosline/Build/Build Config Asset - Android")]
    public class AndroidBuildConfigAsset : BuildConfigAsset {
        
#if UNITY_EDITOR
        public AndroidArchitecture architecture;
#endif
        
    }

    [CreateAssetMenu(fileName = "Build Config - Android", menuName = "Crosline/Build/Build Config Asset - Android")]
    public class IOSBuildConfigAsset : BuildConfigAsset { }
}
