using Crosline.UnityTools.Editor;
using UnityEngine;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class SetProductionMode : BuildStep {

        private const string DebugSymbol = "CROSLINE_DEBUG";

        private const string TestSymbol = "CROSLINE_LOCAL";

        public override bool Execute() {

            var isDebugMode = CommandLineHelper.ArgumentTrue("debugMode") || CommonBuilder.Instance.buildConfig.buildMode.Equals(BuildOptions.BuildMode.DEBUG);

            if (isDebugMode) {
                Debug.Log("[Builder][SetProductionMode] Debug: Debug Mode is enabled.");
                DefineSymbolHelper.Add(DebugSymbol);
            }
            else {
                Debug.Log("[Builder][SetProductionMode] Debug: Debug Mode is disabled.");
                DefineSymbolHelper.Remove(DebugSymbol);
            }



            var isTestEnvironment = CommandLineHelper.ArgumentTrue("testEnvironment");

            if (isTestEnvironment) {
                Debug.Log("[Builder][SetProductionMode] Debug: Test Mode is enabled.");
                DefineSymbolHelper.Add(TestSymbol);
            }
            else {
                Debug.Log("[Builder][SetProductionMode] Debug: Test Mode is disabled.");
                DefineSymbolHelper.Remove(TestSymbol);
            }

            DefineSymbolHelper.Set();

            return true;
        }
    }
}
