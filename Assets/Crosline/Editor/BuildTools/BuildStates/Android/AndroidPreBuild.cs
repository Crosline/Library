using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class AndroidPreBuild : PreBuild {

        public AndroidPreBuild(params BuildStep[] buildSteps) : base(buildSteps) {
            _buildPlatform = BuildOptions.BuildPlatform.Android;
        }

        public AndroidPreBuild() : base() {
            _buildPlatform = BuildOptions.BuildPlatform.Android;

            var steps = new BuildStep[] {
                new SetAndroidSDKVariables(),
                // new SetKeystorePass()
            };

            _buildSteps.InsertRange(0, steps);
        }

    }
}
