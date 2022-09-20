namespace Crosline.BuildTools.Editor {
    public abstract class BuildState {
        public static BuildState Instance => _instance;

        private static BuildState _instance = null;

        public string name = "";

        protected BuildOptions.BuildPlatform _buildPlatform;
        
        protected System.Collections.Generic.List<BuildSteps.BuildStep> _buildSteps;

        public bool isCritical = false;

        protected BuildState() {
            
        }

        public void StartState() {
            for (int i = 0; i < _buildSteps.Count; i++) {
                _buildSteps[i].Execute();
            }
        }
    }
}
