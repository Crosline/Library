﻿using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Crosline.TestTools.Editor {
    public class BenchmarkWindow : EditorWindow { //TODO - Crosline: You stupid bastard complete this immediately

        public static BenchmarkWindow benchmarkWindow;

        private Vector2 _scrollPos = Vector2.zero;

        private Dictionary<MethodInfo, long> methodInfos = new();

        private bool _isMethodRefreshed = false;
        
        private static Texture2D _startIcon;
        private static Texture2D _refreshIcon;
        private static Texture2D _successIcon;
        private static Texture2D _failIcon;

        private void OnEnable() {
            GetMethodInfos();
            
            _startIcon = Resources.Load("Textures/start-32") as Texture2D;
            _refreshIcon = Resources.Load("Textures/trash-32") as Texture2D;
            _successIcon = Resources.Load("Textures/success-16") as Texture2D;
            _failIcon = Resources.Load("Textures/warning-16") as Texture2D;
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


            GUILayout.Label(sec >= 0 ? _successIcon : _failIcon, GUILayout.Width(20), GUILayout.Height(40));
            
            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MaxWidth(240), GUILayout.Height(40));
            EditorGUILayout.LabelField(method.Name, new GUIStyle()
            {
                fontSize = 15,
                normal =
                {
                    textColor = Color.white
                }
            });
            EditorGUILayout.LabelField($"Class: {method.DeclaringType.Name}", new GUIStyle()
            {
                fontSize = 10,
                normal =
                {
                    textColor = Color.white
                }
            });
            EditorGUILayout.EndVertical();
            
            {
                EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(60), GUILayout.Height(38));
                EditorGUILayout.LabelField(sec >= 0 ? $"{sec}ms" : string.Empty, GUILayout.Width(55), GUILayout.Height(38));
                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button(_refreshIcon, GUILayout.Width(45), GUILayout.Height(45))) {
                selectedMethod = method;
                EditorApplication.update += RefreshMethodAfterUpdate;
            }
            EditorGUILayout.Separator();

            if (GUILayout.Button(_startIcon, GUILayout.Width(45), GUILayout.Height(45))) {
                selectedMethod = method;
                EditorApplication.update += RunMethodAfterUpdate;
            }


            EditorGUILayout.EndHorizontal();
        }
    }
}
