using System.Linq;
using System.Text;
using Crosline.BuildTools;
using Crosline.DebugTools;
using Crosline.TestTools;
using Crosline.UnityTools;
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Crosline {
    public class MonoTester : MonoBehaviour {
        private Vector3[] testPath =
        {
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

        [Benchmark]
        public void BenchmarkTest3(int size) {
            byte[] b = new byte[size];

            for (int i = 0; i < size; i++) {
                b[i] = 0x00000001;
            }
        }

        [Benchmark(5)]
        public void ConcatTest() {
            for (int i = 0; i < 1000; i++) {
                var coca = string.Concat("its sometimes so ", 5, " but also ", Color.blue, " but not ", true, 5, " but also ", Color.blue, " but not ", true);

            }
        }
        
        public void ConcatTestT() {
            for (int i = 0; i < 1000; i++) {
                var coca = string.Concat("its sometimes so ", 5, " but also ", Color.blue, " but not ", true, 5, " but also ", Color.blue, " but not ", true);
            }
        }

        [Benchmark(5)]
        public void StringDollarTest() {
            for (int i = 0; i < 1000; i++) {
                var x = $"its sometimes so {5} but also {Color.blue} but not {true}{5} but also {Color.blue} but not {true}";
            }
        }

        [Benchmark(5)]
        public void StringBuilderTest() {
            var sb = new StringBuilder();

            for (int i = 0; i < 1000; i++) {
                sb.Clear();
                sb.Append("its sometimes so ");
                sb.Append(5);
                sb.Append(" but also ");
                sb.Append(Color.blue);
                sb.Append(" but not ");
                sb.Append(true);
                sb.Append(5);
                sb.Append(" but also ");
                sb.Append(Color.blue);
                sb.Append(" but not ");
                sb.Append(true);

            }
        }

        [Benchmark(5)]
        public void BruteStringTest() {
            for (int i = 0; i < 1000; i++) {
                var br = "";
                br += "its sometimes so ";
                br += 5;
                br += " but also ";
                br += Color.blue;
                br += " but not ";
                br += true;
                br += 5;
                br += " but also ";
                br += Color.blue;
                br += " but not ";
                br += true;

            }
        }

#if UNITY_EDITOR
        [MenuItem("Crosline/Test/Log/Debug")]
        public static void TestLogDebug() {
            CroslineDebug.Log("Start");
        }

        [MenuItem("Crosline/Test/Log/Warning")]
        public static void TestLogWarning() {
            CroslineDebug.LogWarning("Start");
        }

        [MenuItem("Crosline/Test/Log/Error")]
        public static void TestLogError() {
            CroslineDebug.LogError("error");
        }
        
        [MenuItem("Crosline/Test/Directory/One")]
        public static void TestDirectoryOne() {
            var c = Path.DirectorySeparatorChar;
            var files = Directory.GetDirectories($"C:{c}Directory{c}Test{c}").ToList();
            files.Sort((f1, f2) => Directory.GetCreationTimeUtc(f1).CompareTo(Directory.GetCreationTimeUtc(f2)));
            
            Debug.Log(files.Count);
        }
        
        [MenuItem("Crosline/Test/Directory/Two")]
        public static void TestDirectoryTwo() {
            var c = Path.DirectorySeparatorChar;
            var files = Directory.GetDirectories($"C:{c}Directory{c}Test").ToList();
            files.Sort((f1, f2) => Directory.GetCreationTimeUtc(f1).CompareTo(Directory.GetCreationTimeUtc(f2)));
            
            Debug.Log(files.Count);
        }
        
        [MenuItem("Crosline/Test/Directory/Three")]
        public static void TestDirectoryThree() {
            var c = Path.DirectorySeparatorChar;
            var files = Directory.GetDirectories($"C:{c}Directory{c}Test{c}1").ToList();
            files.Sort((f1, f2) => Directory.GetCreationTimeUtc(f1).CompareTo(Directory.GetCreationTimeUtc(f2)));
            
            Debug.Log(files.Count);
        }
        
        

#endif
    }
}
