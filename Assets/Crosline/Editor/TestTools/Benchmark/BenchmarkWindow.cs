using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Crosline.TestTools.Editor {
    public class BenchmarkWindow : EditorWindow {

        public static BenchmarkWindow benchmarkWindow;

        private Vector2 _scrollPos = Vector2.zero;

        private Dictionary<MethodInfo, long> methodInfos = new();

        private bool _isMethodRefreshed = false;

        private void OnEnable() {
            GetMethodInfos();
        }

        [MenuItem("Crosline/Subsystems/Benchmark")]
        public static void Initialize() {
            benchmarkWindow = (BenchmarkWindow) GetWindow(typeof(BenchmarkWindow), false, "Benchmark");
            benchmarkWindow.Show();
            benchmarkWindow.minSize = new Vector2(445, 720);
            benchmarkWindow.maxSize = new Vector2(445, 1080);
        }

        public static bool IsOpen() {
            return benchmarkWindow != null;
        }

        private void GetMethodInfos() {
            methodInfos = BenchmarkManager.MethodInfos;
        }

        private void OnGUI() {
            DrawToolbar();

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            EditorGUILayout.BeginVertical();

            foreach (var method in methodInfos) {
                DrawMethodInfo(method.Key, method.Value);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();

            if (_isMethodRefreshed) {
                GetMethodInfos();
            }
        }

        private void DrawToolbar() {

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            EditorGUILayout.LabelField("Method");

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Reset", EditorStyles.toolbarButton, GUILayout.Width(60))) {
                BenchmarkManager.ResetAllBenchmark();
            }

            if (GUILayout.Button("Reload", EditorStyles.toolbarButton, GUILayout.Width(60))) {
                BenchmarkManager.FillMethodInfo();
            }

            if (GUILayout.Button("Run All", EditorStyles.toolbarButton, GUILayout.Width(60))) {
                BenchmarkManager.TestCachedMethods();
            }

            EditorGUILayout.EndHorizontal();

        }

        private void RefreshMethodAfterUpdate() {
            EditorApplication.update -= RefreshMethodAfterUpdate;
            BenchmarkManager.ResetBenchmark(selectedMethod);
        }

        private void RunMethodAfterUpdate() {
            EditorApplication.update -= RunMethodAfterUpdate;
            BenchmarkManager.TestMethod(selectedMethod);
        }

        private MethodInfo selectedMethod;

        private void DrawMethodInfo(MethodInfo method, long sec) {
            EditorGUILayout.BeginHorizontal(GUI.skin.box);


            GUILayout.Label(sec >= -1 ? EditorGUIUtility.IconContent("d_console.erroricon.inactive.sml") : EditorGUIUtility.IconContent("d_console.erroricon"), GUILayout.Width(20), GUILayout.Height(40));
            
            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MaxWidth(240), GUILayout.Height(40));
            EditorGUILayout.LabelField(method.Name, new GUIStyle() {
                fontSize = 15,
                normal = {
                    textColor = Color.white
                }
            });

            EditorGUILayout.LabelField($"Class: {method.DeclaringType.Name}", new GUIStyle() {
                fontSize = 10,
                normal = {
                    textColor = Color.white
                }
            });

            EditorGUILayout.EndVertical();

            {
                EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(60), GUILayout.Height(38));
                EditorGUILayout.LabelField(sec >= 0 ? $"{sec}ms" : (sec == -1 ? string.Empty : "error"), GUILayout.Width(55), GUILayout.Height(38));
                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button(EditorGUIUtility.IconContent("d_Refresh@2x"), GUILayout.Width(45), GUILayout.Height(45))) {
                selectedMethod = method;
                EditorApplication.update += RefreshMethodAfterUpdate;
            }

            EditorGUILayout.Separator();

            if (GUILayout.Button(EditorGUIUtility.IconContent("d_PlayButton@2x"), GUILayout.Width(45), GUILayout.Height(45))) {
                selectedMethod = method;
                EditorApplication.update += RunMethodAfterUpdate;
            }


            EditorGUILayout.EndHorizontal();
        }
    }
}
