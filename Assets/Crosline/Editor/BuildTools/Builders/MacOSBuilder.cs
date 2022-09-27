using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class MacOSBuilder : Builder {

        public MacOSBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.MacOS;
        }

        public MacOSBuilder(params BuildState[] states) : base(new List<BuildState>(states), LoadBuildConfig("BuildConfigAsset_MacOS")) { }
    }
}
