using System.Collections.Generic;
using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class TestState : BuildState {
        public TestState(List<BuildStep> buildSteps) : base(buildSteps) {
            _name = "TestState";
            _buildPlatform = BuildOptions.BuildPlatform.Generic;
        }
    }
}
