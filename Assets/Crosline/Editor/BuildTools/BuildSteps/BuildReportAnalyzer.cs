﻿using System.Linq;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class BuildReportAnalyzer : BuildStep {

        private bool _useCriticalErrorMessages = false;

        private string[] _criticalErrorMessages = new[] {
            "Error building Player",
            "Gradle",
            "Unity3d command line execution failed with status",
            "stop command received",
            "Unity-iPhone.xcworkspace' does not exist",
            "Cocoapods installation failure"
        };

        public BuildReportAnalyzer() {
            _isCritical = true;
        }

        public override bool Execute() {
            var buildReport = CommonBuilder.Instance.buildReport;

            if (buildReport.summary.totalErrors > 0) {
                var errorMessages = buildReport.steps.SelectMany(x => x.messages).Where(x => x.type.HasFlag(LogType.Error));

                foreach (var error in errorMessages) {
                    var errorContent = error.content;
                    Debug.LogError(errorContent);

                    if (_criticalErrorMessages.Any(x => errorContent.Contains(x))) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}