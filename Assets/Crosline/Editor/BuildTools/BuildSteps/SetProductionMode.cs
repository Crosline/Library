using Crosline.UnityTools.Editor;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class SetProductionMode : BuildStep {

        private const string DebugSymbol = "CROSLINE_DEBUG";
        private const string TestSymbol = "CROSLINE_LOCAL";

        public override bool Execute() {

            var isDebugMode = CommandLineHelper.ArgumentTrue("debugMode") || CommonBuilder.Instance.buildConfig.buildMode.Equals(BuildOptions.BuildMode.DEBUG);

            if (isDebugMode)
                DefineSymbolHelper.Add(DebugSymbol);
            else
                DefineSymbolHelper.Remove(DebugSymbol);
            
            
            
            var isTestEnvironment = CommandLineHelper.ArgumentTrue("testEnvironment");
            
            if (isTestEnvironment)
                DefineSymbolHelper.Add(TestSymbol);
            else
                DefineSymbolHelper.Remove(TestSymbol);

            DefineSymbolHelper.Set();

            return true;
        }
    }
}
