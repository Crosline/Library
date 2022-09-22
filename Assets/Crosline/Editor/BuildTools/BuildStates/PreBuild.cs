using System.Collections.Generic;
using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class PreBuild : BuildState {

        public PreBuild(params BuildStep[] buildSteps) : base(new List<BuildStep>(buildSteps)) {
            _name = nameof(PreBuild);
        }

        public PreBuild() {
            _name = nameof(PreBuild);

            _buildSteps = new List<BuildStep>() {
                new CleanOldBuilds(),
                new SwitchActiveBuildTarget(),
                new AdjustPlayerSettings(),
                new AdjustBuildOptions(),
                new SetProductionMode()
            };
        }

    }
}
