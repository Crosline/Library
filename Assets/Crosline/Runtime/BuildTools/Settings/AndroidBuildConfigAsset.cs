using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config - Android", menuName = "Crosline/Build/Build Config/Android")]
    public class AndroidBuildConfigAsset : MobileBuildConfigAsset {
        
        public BuildOptions.Architecture architecture;
        
    }
}
