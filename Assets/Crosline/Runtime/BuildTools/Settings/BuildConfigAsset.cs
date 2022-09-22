using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config", menuName = "Crosline/Build/Build Config Asset")]
    public class BuildConfigAsset : ScriptableObject {

        public BuildOptions.BuildPlatform platform;

        public ScreenOrientation screenOrientation;

        public BuildOptions.ScriptingBackend backend;

        public BuildOptions.BuildMode buildMode;

        public BuildOptions.ApiCompability apiCompability;

        public string bundle = "com.crosline.projectName";

        public string version = "0.1";

    }
}
