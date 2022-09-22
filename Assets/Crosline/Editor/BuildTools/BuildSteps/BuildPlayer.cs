using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class BuildPlayer : BuildStep {

        public BuildPlayer() {
            _isCritical = true;
        }

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

            if (ActiveScenes == null || ActiveScenes.Length == 0) {
                Debug.LogError($"[Builder][BuildPlayer] Scenes are empty.");
                return false;
            }

            CommonBuilder.Instance.buildReport = BuildPipeline.BuildPlayer(ActiveScenes, buildPath, target, buildOptions);

            var summary = CommonBuilder.Instance.buildReport.summary;

            Debug.Log($"[Builder][BuildPlayer] Build is completed in {summary.totalTime.Minutes} minutes\n" +
                      $"Build Size: {summary.totalSize}\n" +
                      $"Total errors: {summary.totalErrors}\n" +
                      $"Total warnings: {summary.totalWarnings}\n");
            
            Debug.Log($"[Builder][BuildPlayer] Build is located at {summary.outputPath}\n");

            return summary.result.HasFlag(BuildResult.Succeeded);
        }

        private static string[] ActiveScenes {
            get {
                return EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(x => x.path).ToArray();
            }
        }
    }
}
