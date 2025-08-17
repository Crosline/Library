using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config", menuName = "Crosline/Build/Build Config/Default")]
    public class BuildConfigAsset : ScriptableObject {

        public BuildOptions.BuildPlatform platform;

        public BuildOptions.BuildMode buildMode = BuildOptions.BuildMode.DEBUG;

        public BuildOptions.ApiCompability apiCompability = BuildOptions.ApiCompability.NetStandard21;

        public BuildOptions.Compression compression = BuildOptions.Compression.None;

        public string bundle = "com.crosline.projectName";

        public string version = "0.1";
        
        public BuildOptions.ScriptingBackend backend = BuildOptions.ScriptingBackend.Mono;

        public BuildOptions.ManagedStrippingLevel stripping = BuildOptions.ManagedStrippingLevel.Disabled;
        
        public string[] defineSymbolsToAdd = new string[0];
        public string[] defineSymbolsToRemove = new string[0];
        public string[] excludedFiles = new string[0];

    }
}
