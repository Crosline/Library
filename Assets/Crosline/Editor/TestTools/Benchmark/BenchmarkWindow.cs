using UnityEditor;
using UnityEngine;

namespace Crosline.TestTools.Editor.Benchmark {
    public class BenchmarkWindow : EditorWindow { //TODO - Crosline: You stupid bastard complete this immediately
        
        public static BenchmarkWindow benchmarkWindow;
        
        private void OnEnable() {
            //_buildConfigDrawer = new BuildConfigDrawer();

            //RefreshAvailableAssets();
        }
        
        [MenuItem("Crosline/Subsystems/B")]
        public static void Initialize() {
            benchmarkWindow = (BenchmarkWindow) GetWindow(typeof(BenchmarkWindow), false, "Benchmark");
            benchmarkWindow.Show();
            benchmarkWindow.minSize = new Vector2(720, 360);
            benchmarkWindow.maxSize = new Vector2(740, 420);
                
                Debug.Log(BenchmarkManager.MethodInfos);
        }
        
        public static bool IsOpen() {
            return benchmarkWindow != null;
        }
    }
}