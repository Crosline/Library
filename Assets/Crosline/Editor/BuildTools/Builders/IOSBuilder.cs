namespace Crosline.BuildTools.Editor {
    public class IOSBuilder : CommonBuilder {

        public IOSBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.IOS;
        }
        
        public IOSBuilder(System.Collections.Generic.List<BuildStates.BuildState> states) : base(states, BuildOptions.BuildPlatform.IOS) { }
    }
}
