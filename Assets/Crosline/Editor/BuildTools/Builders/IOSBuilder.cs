using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class IOSBuilder : CommonBuilder {

        public IOSBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.IOS;
        }
        
        public IOSBuilder(params BuildState[] states) : base(new List<BuildState>(states), BuildOptions.BuildPlatform.IOS) { }
    }
}
