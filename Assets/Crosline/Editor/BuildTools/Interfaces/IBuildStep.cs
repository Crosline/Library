namespace Crosline.BuildTools.Editor.BuildSteps {
    public abstract class IBuildStep {
        
        public string name;
        
        public BuildOptions.BuildPlatform platform;
        public abstract bool Execute();
    }
}
