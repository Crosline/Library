using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class IOSPostBuild : PostBuild {
        public IOSPostBuild(params BuildStep[] buildSteps) : base(buildSteps) {
            _postBuildCallback = 1000;
            _buildPlatform = BuildOptions.BuildPlatform.IOS;
        }

        public IOSPostBuild(int postBuildCallback = 1) : base(postBuildCallback) {
            _postBuildCallback = 1000;
            _buildPlatform = BuildOptions.BuildPlatform.IOS;

            var steps = new BuildStep[] {
            };

            _buildSteps.InsertRange(0, steps);
        }
    }
}
