using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools {
    [CreateAssetMenu(fileName = "Build Config - Android", menuName = "Crosline/Build/Build Config Asset - Android")]
    public class AndroidBuildConfigAsset : BuildConfigAsset {
        
#if UNITY_EDITOR
        public AndroidArchitecture architecture;
#endif
        
    }
}
