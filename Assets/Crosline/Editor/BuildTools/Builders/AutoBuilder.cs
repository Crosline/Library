using System.Collections.Generic;

namespace Crosline.BuildTools.Editor {
    public class AutoBuilder {
        
        private const string buildStateAssetDirectory = "Assets/Crosline/Editor/BuildTools/BuildStates/AutoBuilder/";
        
#if UNITY_EDITOR_OSX
        private static bool isOSX = true;
#else
        private static bool isOSX = false;
#endif

        //TODO - Crosline: Don't forget to implement an AutoBuilder after the builders.

        private static CommonBuilder Builder;
        
        public void Android() {
            if (Builder != null) {
                return;
            }

            List<BuildState> androidBuildStates = new List<BuildState>() {
            };
            
            Builder = new AndroidBuilder(androidBuildStates);
        }
        
        public void IOS() {
            if (Builder != null || !isOSX) {
                return;
            }
            
            Builder = new IOSBuilder();
        }
        
        public void Windows() {
            if (Builder != null) {
                return;
            }
            
            Builder = new WindowsBuilder();
        }
        
        public void MacOS() {
            if (Builder != null || !isOSX) {
                return;
            }
            
            Builder = new MacOSBuilder();
        }
        
        public void Linux() {
            if (Builder != null) {
                return;
            }
            
            Builder = new LinuxBuilder();
        }
    }
}
