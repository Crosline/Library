namespace Crosline.BuildTools.Editor {
    public class MacOSBuilder : CommonBuilder {

        public MacOSBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.MacOS;
        }
        
        public MacOSBuilder(System.Collections.Generic.List<BuildStates.BuildState> states) : base(states, BuildOptions.BuildPlatform.MacOS) { }
    }
}
