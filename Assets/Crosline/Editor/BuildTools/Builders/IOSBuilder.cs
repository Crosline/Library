using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class IOSBuilder : Builder {

        public IOSBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.IOS;
        }

        public IOSBuilder(params BuildState[] states) : base(new List<BuildState>(states), LoadBuildConfig("BuildConfigAsset_IOS")) { }
    }
}
