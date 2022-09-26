using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class LinuxBuilder : CommonBuilder {

        public LinuxBuilder() : base() {
            _buildStates = new();
            _buildPlatform = BuildOptions.BuildPlatform.Linux;
        }

        public LinuxBuilder(params BuildState[] states) : base(new List<BuildState>(states), LoadBuildConfig("BuildConfigAsset_Linux")) { }
    }
}
