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

            var target = CommonBuilder.Instance.BuildPlatform.ToBuildTarget();
            var buildOptions = CommonBuilder.Instance.buildOptions;
            var buildPath = CommonBuilder.BuildPath;
            var buildFolder = CommonBuilder.BuildFolder;

            if (!Directory.Exists(buildFolder)) {
                Directory.CreateDirectory(buildFolder);
            }

            if (ActiveScenes == null || ActiveScenes.Length == 0) {
                Debug.LogError($"[Builder][BuildPlayer] Error: Scenes are empty.");
                return false;
            }

            try {
                CommonBuilder.Instance.buildReport = BuildPipeline.BuildPlayer(ActiveScenes, buildPath, target, buildOptions);
            }
            catch (Exception e) {
                Debug.LogError($"[Builder][BuildPlayer] Error: Build is failed with an exception\n{e}");
                return false;
            }

            var summary = CommonBuilder.Instance.buildReport.summary;

            Debug.Log($"[Builder][BuildPlayer] Info: Build is completed\n" +
                      $"Build time: {summary.totalTime.TotalMinutes} minutes {summary.totalTime.Seconds} seconds\n" +
                      $"Build Size: {summary.totalSize*Mathf.Pow(10, -6):0.00}\n" +
                      $"Total errors: {summary.totalErrors}\n" +
                      $"Total warnings: {summary.totalWarnings}\n");
            
            Debug.Log($"[Builder][BuildPlayer] Debug: Build is located at {summary.outputPath}\n");

            return summary.result.HasFlagAny(BuildResult.Succeeded);
        }

        private static string[] ActiveScenes {
            get {
                return EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(x => x.path).ToArray();
            }
        }
    }
}
