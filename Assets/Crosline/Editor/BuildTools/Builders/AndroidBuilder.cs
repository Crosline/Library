namespace Crosline.BuildTools.Editor {
    public class AndroidBuilder : CommonBuilder {

        public AndroidBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Android;
        }
        
    }
}
