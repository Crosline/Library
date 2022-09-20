namespace Crosline.BuildTools.Editor.BuildSteps {
    [System.Serializable]
    public abstract class BuildStep {
        
        public string name;
        
        public BuildOptions.BuildPlatform platform;

        public abstract bool Execute();
    }
}
