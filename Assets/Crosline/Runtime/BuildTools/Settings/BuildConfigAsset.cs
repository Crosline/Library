using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config", menuName = "Crosline/Build/Build Config Asset")]
    public class BuildConfigAsset : ScriptableObject {

        public BuildOptions.BuildPlatform platform;

        public BuildOptions.BuildMode buildMode = BuildOptions.BuildMode.DEBUG;

        public BuildOptions.ApiCompability apiCompability = BuildOptions.ApiCompability.NetStandard21;

        public BuildOptions.Compression compression = BuildOptions.Compression.None;

        public string bundle = "com.crosline.projectName";

        public string version = "0.1";
        
        public BuildOptions.ScriptingBackend backend = BuildOptions.ScriptingBackend.Mono;

        public BuildOptions.ManagedStrippingLevel stripping = BuildOptions.ManagedStrippingLevel.Disabled;

    }
}
