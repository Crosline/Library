using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config - Mobile", menuName = "Crosline/Build/Build Config Asset - Mobile")]
    public class MobileBuildConfigAsset : BuildConfigAsset {
        public ScreenOrientation screenOrientation = ScreenOrientation.Portrait;
    }
}
