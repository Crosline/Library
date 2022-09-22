using UnityEditor;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class SetKeystorePass : BuildStep {

        private const string KeystorePass = "5Q3Ae9GeahBY4TbZunqVd84w";

        public SetKeystorePass() {
            _platform = BuildOptions.BuildPlatform.Android;
        }

        public override bool Execute() {
            PlayerSettings.Android.useCustomKeystore = true;

            PlayerSettings.Android.keystorePass = KeystorePass;
            PlayerSettings.Android.keyaliasPass = KeystorePass;

            return true;
        }
    }
}
