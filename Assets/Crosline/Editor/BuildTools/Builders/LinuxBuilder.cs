namespace Crosline.BuildTools.Editor {
    public class LinuxBuilder : CommonBuilder {

        public LinuxBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Linux;
        }
        
    }
}
