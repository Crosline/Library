using System.Collections.Generic;
using System.IO;
using System.Linq;
using Crosline.DebugTools;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class CleanOldBuilds : BuildStep {

        private string _buildFolder;

        private int _buildAmountToKeep = 10;

        public CleanOldBuilds() {
            _platform = BuildOptions.BuildPlatform.Generic;
            _buildFolder = CommonBuilder.BuildFolder;
        }

        public override bool Execute() {
            if (!Directory.Exists(_buildFolder)) {
                CroslineDebug.Log("Build folder cannot be founded.");

                return true;
            }

            List<string> files;

            if (CommonBuilder.Instance.BuildPlatform.HasFlag(BuildOptions.BuildPlatform.Mobile)) {
                files = Directory.GetFiles(_buildFolder).ToList();
                files.Sort((f1, f2) => File.GetCreationTimeUtc(f1).CompareTo(File.GetCreationTimeUtc(f2)));
            }
            else {
                files = Directory.GetDirectories(_buildFolder).ToList();
                files.Sort((f1, f2) => Directory.GetCreationTimeUtc(f1).CompareTo(Directory.GetCreationTimeUtc(f2)));
            }


            if (files.Count >= _buildAmountToKeep)
                for (var i = files.Count-_buildAmountToKeep; i > 0; i--)
                    if (CommonBuilder.Instance.BuildPlatform.HasFlag(BuildOptions.BuildPlatform.Mobile)) {
                        File.Delete(files[i]);
                    }
                    else {
                        Directory.Delete(files[i]);
                    }

            return true;
        }
    }
}
