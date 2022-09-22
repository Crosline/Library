using System.Collections.Generic;
using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class PostBuild : BuildState {
        
        public PostBuild(List<BuildStep> buildSteps) : base(buildSteps) {
            _name = nameof(PostBuild);
            _postBuildCallback = 1;
        }

        public PostBuild() {
            _name = nameof(PostBuild);
            _postBuildCallback = 1;
        }

    }
}
