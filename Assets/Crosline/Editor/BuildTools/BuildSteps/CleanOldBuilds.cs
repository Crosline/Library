using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class CleanOldBuilds : BuildStep {

        private int _buildAmountToKeep;

        public CleanOldBuilds(int buildAmountToKeep = 10) {
            _platform = BuildOptions.BuildPlatform.Generic;
            _buildAmountToKeep = buildAmountToKeep;
        }

        public override bool Execute() {
            var buildFolder = Builder.BuildFolder;
            if (!Directory.Exists(buildFolder)) {
                Debug.Log("[Builder] Warning: Build folder cannot be founded.");

                return true;
            }

            List<string> files;

            if (Builder.Instance.BuildPlatform.HasFlagAny(BuildOptions.BuildPlatform.Android)) {
                files = Directory.GetFiles(buildFolder).ToList();
                files.Sort((f1, f2) => File.GetCreationTimeUtc(f1).CompareTo(File.GetCreationTimeUtc(f2)));
            }
            else if (Builder.Instance.BuildPlatform.HasFlagAny(BuildOptions.BuildPlatform.Standalone)) {
                files = Directory.GetDirectories($"{buildFolder}{Path.DirectorySeparatorChar}").ToList();
                files.Sort((f1, f2) => Directory.GetCreationTimeUtc(f1).CompareTo(Directory.GetCreationTimeUtc(f2)));
            }
            else {
                Debug.Log($"[Builder][CleanOldBuilds] Debug: Skipping");
                return true;
            }

            Debug.Log($"[Builder][CleanOldBuilds] Debug: {files.Count} file found in the Build Folder.");
            Debug.Log($"[Builder][CleanOldBuilds] Debug: Build Folder is: {buildFolder}");

            if (files.Count >= _buildAmountToKeep)
                for (var i = 0; i < files.Count - _buildAmountToKeep; i++)
                    if (Builder.Instance.BuildPlatform.HasFlagAny(BuildOptions.BuildPlatform.Mobile)) {
                        File.Delete(files[i]);
                    }
                    else {
                        Directory.Delete(files[i]);
                    }

            return true;
        }
    }
}
