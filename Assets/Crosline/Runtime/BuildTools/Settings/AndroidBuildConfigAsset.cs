using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config - Android", menuName = "Crosline/Build/Build Config Asset - Android")]
    public class AndroidBuildConfigAsset : MobileBuildConfigAsset {
        
        public BuildOptions.Architecture architecture;
        
    }
}
