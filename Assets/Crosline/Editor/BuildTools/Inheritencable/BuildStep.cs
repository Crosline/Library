namespace Crosline.BuildTools.Editor.BuildSteps {
    [System.Serializable]
    public abstract class BuildStep {
        public string Name => _name;

        private string _name;

        public bool IsCritical => _isCritical;

        protected bool _isCritical = false;

        public BuildOptions.BuildPlatform Platform => _platform;

        protected BuildOptions.BuildPlatform _platform = BuildOptions.BuildPlatform.Generic;

        public abstract bool Execute();

        protected BuildStep() {
            _name = this.GetType().Name;
        }
    }
}
