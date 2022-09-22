using System;
using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class SetAndroidSDKVariables : BuildStep {

        public SetAndroidSDKVariables() {
            _platform = BuildOptions.BuildPlatform.Android;
        }

        public override bool Execute() {
            string androidSdk = Environment.GetEnvironmentVariable("ANDROID_HOME");
            string androidNdk = Environment.GetEnvironmentVariable("ANDROID_NDK_HOME");
            string androidNdkR16B = Environment.GetEnvironmentVariable("ANDROID_NDK_HOME_R16");

            if (string.IsNullOrEmpty(androidSdk)) {
                androidSdk = EditorPrefs.GetString("AndroidSdk", "");
            }
            else {
                EditorPrefs.GetString("AndroidSdk", androidSdk);
            }

            if (string.IsNullOrEmpty(androidNdk)) {
                androidNdk = EditorPrefs.GetString("AndroidNdk", "");
            }
            else {
                EditorPrefs.GetString("AndroidNdk", androidNdk);
            }

            if (string.IsNullOrEmpty(androidNdkR16B)) {
                androidNdkR16B = EditorPrefs.GetString("AndroidNdkR16B", "");
            }
            else {
                EditorPrefs.GetString("AndroidNdkR16B", androidNdkR16B);
            }

            Debug.Log("[SetAndroidSDKVariables] AndroidSdk: "+androidSdk);
            Debug.Log("[SetAndroidSDKVariables] AndroidNdk: "+androidNdk);
            Debug.Log("[SetAndroidSDKVariables] AndroidNdkR16B: "+androidNdkR16B);

            return true;
        }
    }
}
