namespace Crosline.BuildTools.Editor.BuildSteps {
    public class EnableXCodeExceptions : BuildStep {

        public EnableXCodeExceptions() {
            _platform = BuildOptions.BuildPlatform.IOS;
        }

        public override bool Execute() {
            return true;
        }
    }
}
