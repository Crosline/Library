namespace Crosline.BuildTools.Editor {
    public class AndroidBuilder : CommonBuilder {

        public AndroidBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Android;
        }
        
        public AndroidBuilder(System.Collections.Generic.List<BuildStates.BuildState> states) : base(states, BuildOptions.BuildPlatform.Android) { }
    }
}
