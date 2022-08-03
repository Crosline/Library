using UnityEngine;

namespace Crosline.Builder.Editor.Settings
{
    [CreateAssetMenu(fileName = "Build Config", menuName = "Crosline/Build/Build Config Asset")]
    public class BuildConfigAsset : ScriptableObject
    {
        public enum BuildPlatform
        {
            Windows = 0,
            Linux = 1,
            MacOS = 2,
            Android = 3,
            IOS = 4
        }
        public enum ScriptingBackend
        {
            IL2CPP = 0,
            Mono = 1
        }
        public enum ApiCompability
        {
            NetStandard21 = 0,
            NetFramework = 1
        }

        public enum BuildMode
        {
            DEBUG = 0,
            RELEASE = 1
        }

        public BuildPlatform platform;

        public ScriptingBackend backend;

        public BuildMode buildMode;

        public ApiCompability apiCompability;

        public string bundle = "com.crosline.projectName";
        
        public string version = "0.1";


    }
}
