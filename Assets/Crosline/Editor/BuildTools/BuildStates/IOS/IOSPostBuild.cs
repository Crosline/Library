using System.Collections.Generic;
using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class IOSPostBuild : PostBuild {
        public IOSPostBuild(List<BuildStep> buildSteps) : base(buildSteps) {
            _postBuildCallback = 1000;
            _buildPlatform = BuildOptions.BuildPlatform.IOS;
        }

        public IOSPostBuild() {
            _postBuildCallback = 1000;
            _buildPlatform = BuildOptions.BuildPlatform.IOS;

            _buildSteps = new List<BuildStep>() {
                
            };
        }
    }
}
