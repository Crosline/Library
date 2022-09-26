using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class WindowsBuilder : CommonBuilder {

        public WindowsBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Windows;
        }

        public WindowsBuilder(params BuildState[] states) : base(new List<BuildState>(states), LoadBuildConfig("BuildConfigAsset_Windows")) { }
    }
}
