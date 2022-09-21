namespace Crosline.BuildTools.Editor.BuildSteps {
    [System.Serializable]
    public abstract class BuildStep {
        public string Name => _name;
        protected string _name;
        
        public BuildOptions.BuildPlatform Platform => _platform;
        protected BuildOptions.BuildPlatform _platform;

        public abstract bool Execute();
    }
}
