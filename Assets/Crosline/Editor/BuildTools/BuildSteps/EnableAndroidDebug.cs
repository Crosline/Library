using System;
using System.IO;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class EnableAndroidDebug : BuildStep {

        public EnableAndroidDebug() {
            _platform = BuildOptions.BuildPlatform.Android;
        }

        public override bool Execute() {
            try {
                string androidManifestPath = Path.Combine(Application.dataPath, "Plugins", "Android", "AndroidManifest.xml");
                string manifest = File.ReadAllText(androidManifestPath);
                manifest = manifest.Replace("android:debuggable=\"true\"", "");
                File.WriteAllText(androidManifestPath, manifest);
            }
            catch (Exception e) {
                Debug.Log($"[Builder][EnableAndroidDebug] Error: Android Manifest could not be edited. {e}");

                return false;
            }

            return true;
        }
    }
}
