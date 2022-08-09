using System.Diagnostics;
using Crosline.BuildTools.Editor.Settings;
using Crosline.DebugTools;
using Crosline.TestTools;
using Crosline.UnityTools;
using UnityEditor;
using UnityEngine;

namespace Crosline {
    public class MonoTester : MonoBehaviour {
        private Vector3[] testPath = {
            Vector3.zero,
            Vector3.one,
            Vector3.one * 2f,
            Vector3.one * 3f
        };

        [SerializeField]
        private float test1;
        [SerializeField]
        private float test2;
        [Space]
        [SerializeField]
        private float test3;
        [Separator]
        [SerializeField]
        private float test4;
        [Separator(20)]
        [SerializeField]
        private float test5;
        [Separator(30)]
        [SerializeField]
        private float test6;
        [Separator]
        [Expandable]
        [SerializeField]
        private BuildConfigAsset _buildConfigAsset;

        private void Start() {
            CroslineDebug.LogError("hi");
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            CroslineGizmos.DrawPath(testPath, true, true);
        }
        
        [Benchmark]
        public void BenchmarkTest1() {
            byte[] b = new byte[100000000];
            for (int i = 0; i < 100000000; i++) {
                b[i] = 0x00000001;
            }
        }

        [Benchmark]
        public void BenchmarkTest2() {
            byte[] b = new byte[100];
            for (int i = 0; i < 100; i++) {
                b[i] = 0x00000001;
            }
        }

        [Benchmark(parameters: new object[] {50})]
        public void BenchmarkTest3(int size) {
            byte[] b = new byte[size];
            for (int i = 0; i < size; i++) {
                b[i] = 0x00000001;
            }
        }

        [MenuItem("Crosline/Test/LogDebug")]
        public static void TestLogDebug() {
            CroslineDebug.Log("Start");
        }

        [MenuItem("Crosline/Test/LogWarning")]
        public static void TestLogWarning() {
            CroslineDebug.LogWarning("Start");
        }

        [MenuItem("Crosline/Test/LogError")]
        public static void TestLogError() {
            CroslineDebug.LogError("error");
        }
    }
}