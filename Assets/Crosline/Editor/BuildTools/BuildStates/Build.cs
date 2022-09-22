using System.Collections.Generic;
using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class Build : BuildState {

        public Build(params BuildStep[] buildSteps) : base(new List<BuildStep>(buildSteps)) {
            _name = nameof(Build);
        }

        public Build() {
            _name = nameof(Build);

            _buildSteps = new List<BuildStep>() {
                new BuildPlayer(),
                new BuildReportAnalyzer()
            };
        }


    }
}
