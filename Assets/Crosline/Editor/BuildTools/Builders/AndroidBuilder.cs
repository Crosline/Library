using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class AndroidBuilder : CommonBuilder {

        public AndroidBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Android;
        }
        
        public AndroidBuilder(params BuildState[] states) : base(new List<BuildState>(states), BuildOptions.BuildPlatform.Android) { }
    }
}
