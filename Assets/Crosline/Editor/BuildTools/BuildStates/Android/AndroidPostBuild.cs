using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class AndroidPostBuild : PostBuild {
        public AndroidPostBuild(params BuildStep[] buildSteps) : base(buildSteps) {
            _buildPlatform = BuildOptions.BuildPlatform.Android;
        }

        public AndroidPostBuild(int postBuildCallback = 1) : base(postBuildCallback) {
            _buildPlatform = BuildOptions.BuildPlatform.Android;

            var steps = new BuildStep[] {
                new EnableAndroidDebug()
            };

            _buildSteps.InsertRange(0, steps);
        }
    }
}
