namespace Crosline.BuildTools.Editor {
    public abstract class BuildState {
        public static BuildState Instance => _instance;

        private static BuildState _instance = null;

        public string Name => _name;
        protected string _name = "";

        public BuildOptions.BuildPlatform BuildPlatform => _buildPlatform;
        protected BuildOptions.BuildPlatform _buildPlatform;
        
        public System.Collections.Generic.List<BuildSteps.BuildStep> BuildSteps => _buildSteps;
        protected System.Collections.Generic.List<BuildSteps.BuildStep> _buildSteps;

        public bool isCritical = false;

        protected BuildState(System.Collections.Generic.List<BuildSteps.BuildStep> buildSteps) {
            _buildSteps = buildSteps;
        }

        public void StartState() {
            for (int i = 0; i < _buildSteps.Count; i++) {
                _buildSteps[i].Execute();
            }
        }
    }
}
