using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class CleanOldBuilds : BuildStep {

        private string _buildFolder;

        private int _buildAmountToKeep;

        public CleanOldBuilds(int buildAmountToKeep = 10) {
            _platform = BuildOptions.BuildPlatform.Generic;
            _buildFolder = CommonBuilder.BuildFolder;
            _buildAmountToKeep = buildAmountToKeep;
        }

        public override bool Execute() {
            if (!Directory.Exists(_buildFolder)) {
                Debug.Log("[Builder] Warning: Build folder cannot be founded.");

                return true;
            }

            List<string> files;

            if (CommonBuilder.Instance.BuildPlatform.HasFlagAny(BuildOptions.BuildPlatform.Mobile)) {
                files = Directory.GetFiles(_buildFolder).ToList();
                files.Sort((f1, f2) => File.GetCreationTimeUtc(f1).CompareTo(File.GetCreationTimeUtc(f2)));
            }
            else {
                files = Directory.GetDirectories(_buildFolder).ToList();
                files.Sort((f1, f2) => Directory.GetCreationTimeUtc(f1).CompareTo(Directory.GetCreationTimeUtc(f2)));
            }

            Debug.Log($"[Builder][CleanOldBuilds] Debug: {files.Count} file found in the Build Folder.");
            Debug.Log($"[Builder][CleanOldBuilds] Debug: Build Folder is: {_buildFolder}");

            if (files.Count >= _buildAmountToKeep)
                for (var i = 0; i < files.Count - _buildAmountToKeep; i++)
                    if (CommonBuilder.Instance.BuildPlatform.HasFlagAny(BuildOptions.BuildPlatform.Mobile)) {
                        File.Delete(files[i]);
                    }
                    else {
                        Directory.Delete(files[i]);
                    }

            return true;
        }
    }
}
