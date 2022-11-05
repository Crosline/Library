using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config - Mobile", menuName = "Crosline/Build/Build Config/Mobile")]
    public class MobileBuildConfigAsset : BuildConfigAsset {
        public ScreenOrientation screenOrientation = ScreenOrientation.Portrait;
    }
}
