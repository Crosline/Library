using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Crosline.TestTools.Editor.Benchmark {
    public class BenchmarkWindow : EditorWindow { //TODO - Crosline: You stupid bastard complete this immediately
        
        public static BenchmarkWindow benchmarkWindow;
        
        private Vector2 _scrollPos = Vector2.zero;

        private Dictionary<MethodInfo, long> methodInfos = new Dictionary<MethodInfo, long>();

        private bool _isMethodRefreshed = false;
        
        private void OnEnable() {
            ResetMethodInfos();
        }
        
        [MenuItem("Crosline/Subsystems/Benchmark")]
        public static void Initialize() {
            benchmarkWindow = (BenchmarkWindow) GetWindow(typeof(BenchmarkWindow), false, "Benchmark");
            benchmarkWindow.Show();
            benchmarkWindow.minSize = new Vector2(640, 480);
            benchmarkWindow.maxSize = new Vector2(640, 480);
        }
        
        public static bool IsOpen() {
            return benchmarkWindow != null;
        }

        private void ResetMethodInfos() {
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
                ResetMethodInfos();
            }
        }

        private void DrawToolbar() {

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar); 
            
            EditorGUILayout.LabelField("Class");
            EditorGUILayout.LabelField("Method");
            
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("Reset", EditorStyles.toolbarButton, GUILayout.Width(60))) {
                BenchmarkManager.ResetAllBenchmark();
            }
            
            if (GUILayout.Button("Refresh", EditorStyles.toolbarButton, GUILayout.Width(60))) {
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
            EditorGUILayout.BeginHorizontal(GUI.skin.box, GUILayout.MaxWidth(640));
            EditorGUILayout.LabelField(method.DeclaringType.Name);
            EditorGUILayout.LabelField(method.Name);
            GUILayout.FlexibleSpace();
            if (sec >= 0) {
                EditorGUILayout.LabelField($"{sec}ms");
            }

            if (GUILayout.Button("Refresh", EditorStyles.toolbarButton, GUILayout.Width(60))) {
                selectedMethod = method;
                EditorApplication.update += RefreshMethodAfterUpdate;
            }
            EditorGUILayout.Separator();
            if (GUILayout.Button("Run", EditorStyles.toolbarButton, GUILayout.Width(60))) {
                selectedMethod = method;
                EditorApplication.update += RunMethodAfterUpdate;
            }
            
            
            EditorGUILayout.EndHorizontal();
        }
    }
}