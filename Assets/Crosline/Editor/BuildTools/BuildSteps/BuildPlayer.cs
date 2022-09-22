using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class BuildPlayer : BuildStep {

        public override bool Execute() {

            var target = CommonBuilder.Instance.BuildPlatform.ToBuildTarget();
            var buildOptions = CommonBuilder.Instance.buildOptions;
            var buildPath = CommonBuilder.BuildPath;

            if (target.HasFlag(BuildTarget.Android) && (buildPath.Contains(".apk") || buildPath.Contains(".aab"))) {
                if (!Directory.Exists(Directory.GetParent(buildPath).FullName))
                    Directory.CreateDirectory(buildPath);
            }
            else if (!Directory.Exists(buildPath)) {
                Directory.CreateDirectory(buildPath);
            }


            CommonBuilder.Instance.buildReport = BuildPipeline.BuildPlayer(ActiveScenes, buildPath, target, buildOptions);

            var summary = CommonBuilder.Instance.buildReport.summary;
            
            Debug.Log($"[Builder][BuildPlayer] Build is completed in {summary.totalTime.Minutes} minutes\n"+
                      $"Build Size: {summary.totalSize}"+
                      $"Total errors: {summary.totalErrors}"+
                      $"Total warnings: {summary.totalWarnings}");

            return true;
        }

        private static string[] ActiveScenes {
            get {
                return EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(x => x.path).ToArray();
            }
        }
    }
}
