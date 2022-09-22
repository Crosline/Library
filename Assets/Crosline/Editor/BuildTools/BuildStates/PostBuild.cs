using System.Collections.Generic;
using Crosline.BuildTools.Editor.BuildSteps;

namespace Crosline.BuildTools.Editor {
    public class PostBuild : BuildState {

        public PostBuild(params BuildStep[] buildSteps) : base(new List<BuildStep>(buildSteps)) {
            _name = nameof(PostBuild);
            _postBuildCallback = 1;
        }

        public PostBuild(int postBuildCallback = 1) {
            _name = nameof(PostBuild);
            _postBuildCallback = postBuildCallback;
            
            _buildSteps = new List<BuildStep>();
        }

    }
}
