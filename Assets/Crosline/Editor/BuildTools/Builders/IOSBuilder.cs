namespace Crosline.BuildTools.Editor {
    public class IOSBuilder : CommonBuilder {

        public IOSBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.IOS;
        }
        
    }
}
