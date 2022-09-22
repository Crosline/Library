using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class TestStep : BuildStep {

        public override bool Execute() {
            Debug.Log("Command Line Arguments as kv pair:");

            var clargs = CommandLineHelper.Arguments;

            foreach (var clarg in clargs) {
                Debug.Log($"Key: {clarg.Key}, Value: {clarg.Value}");
            }

            return true;
        }
    }
}
