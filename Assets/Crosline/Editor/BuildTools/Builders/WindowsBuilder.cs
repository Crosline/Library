namespace Crosline.BuildTools.Editor {
    public class WindowsBuilder : CommonBuilder {

        public WindowsBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Windows;
        }
        
        public WindowsBuilder(System.Collections.Generic.List<BuildState> states) : base(states, BuildOptions.BuildPlatform.Windows) { }
    }
}
