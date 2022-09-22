using Crosline.UnityTools.Editor;

namespace Crosline.BuildTools.Editor.BuildSteps {
    public class SetProductionMode : BuildStep {

        private const string DebugSymbol = "CROSLINE_DEBUG";
        
        public override bool Execute() {

            bool isDebugMode = false;

            switch (CommandLineHelper.Arguments["debugMode"]) {
                case "true":
                case "1":
                    isDebugMode = true;
                    break;
            }

            if (isDebugMode) {
                DefineSymbolHelper.Add(DebugSymbol);
            }
            else {
                DefineSymbolHelper.Remove(DebugSymbol);
            }
            
            DefineSymbolHelper.Set();
            
            return true;
        }
    }
}
