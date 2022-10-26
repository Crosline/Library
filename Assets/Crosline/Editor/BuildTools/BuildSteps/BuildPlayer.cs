using System;
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

            var target = Builder.Instance.BuildPlatform.ToBuildTarget();
            var buildOptions = Builder.Instance.buildOptions;
            var buildPath = Builder.BuildPath;
            var buildFolder = Builder.BuildFolder;

            if (!Directory.Exists(buildFolder)) {
                Directory.CreateDirectory(buildFolder);
            }

            if (ActiveScenes == null || ActiveScenes.Length == 0) {
                Debug.LogError($"[Builder][BuildPlayer] Error: Scenes are empty.");
                return false;
            }

            try {
                Builder.Instance.buildReport = BuildPipeline.BuildPlayer(ActiveScenes, buildPath, target, buildOptions);
            }
            catch (Exception e) {
                Debug.LogError($"[Builder][BuildPlayer] Error: Build is failed with an exception\n{e}");
                return false;
            }

            var summary = Builder.Instance.buildReport.summary;

            Debug.Log($"[Builder][BuildPlayer] Info: Build is completed\n" +
                      $"Build time: {summary.totalTime.Minutes} minutes {summary.totalTime.Seconds} seconds\n" +
                      $"Build Size: {summary.totalSize*Mathf.Pow(10, -6):0.00}\n" +
                      $"Total errors: {summary.totalErrors}\n" +
                      $"Total warnings: {summary.totalWarnings}\n");

            if (!summary.result.HasFlagAny(BuildResult.Succeeded)) {
                return false;
            }

            Debug.Log($"[Builder][BuildPlayer] Debug: Build is located at {summary.outputPath}\n");

            return true;
        }

        private static string[] ActiveScenes {
            get {
                return EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(x => x.path).ToArray();
            }
        }
    }
}
