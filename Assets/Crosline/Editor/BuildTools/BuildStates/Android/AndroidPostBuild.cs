using System.Collections.Generic;
using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class AndroidPostBuild : PostBuild {
        public AndroidPostBuild(List<BuildStep> buildSteps) : base(buildSteps) {
            _buildPlatform = BuildOptions.BuildPlatform.Android;
        }

        public AndroidPostBuild() {
            _buildPlatform = BuildOptions.BuildPlatform.Android;

            _buildSteps = new List<BuildStep>() {
                new EnableAndroidDebug()
            };
        }
    }
}
