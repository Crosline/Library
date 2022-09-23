using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class IOSPreBuild : PreBuild {

        public IOSPreBuild(params BuildStep[] buildSteps) : base(buildSteps) {
            _buildPlatform = BuildOptions.BuildPlatform.IOS;
        }

        public IOSPreBuild() : base() {
            _buildPlatform = BuildOptions.BuildPlatform.IOS;

            var steps = new BuildStep[] {
            };

            _buildSteps.InsertRange(0, steps);
        }

    }
}
