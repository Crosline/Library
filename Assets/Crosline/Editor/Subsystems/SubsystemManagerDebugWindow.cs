using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Subsystems.Core.Editor
{
    public class SubsystemManagerDebugWindow : EditorWindow
    {
        private Vector2 _scrollPosition;

        [MenuItem("AAA/Subsystems/Subsystem Debugger")]
        public static void ShowWindow()
        {
            GetWindow<SubsystemManagerDebugWindow>("Subsystem Debugger");
        }

        private void OnGUI()
        {
            if (OnGUI_EditMode())
                return;

            var subsystems = SubsystemManager.Subsystems;
            if (subsystems == null || subsystems.Count == 0)
            {
                GUILayout.Label("No subsystems registered.");
                return;
            }

            GUILayout.Label("Registered Subsystems", EditorStyles.largeLabel);

            GUILayout.BeginScrollView(_scrollPosition);

            foreach (var subsystem in subsystems)
            {
                EditorGUILayout.Space();

                GUILayout.BeginVertical("box");
                GUILayout.Label(subsystem.GetType().Name, EditorStyles.boldLabel);
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                DisplayFields(subsystem);
                GUILayout.EndVertical();
            }

            GUILayout.EndScrollView();
        }

        private static bool OnGUI_EditMode() {
            if (Application.isPlaying)
                return false;

            if (SubsystemManager.IsInitialized)
            {
                GUILayout.Label("Subsystem Manager is initialized in Editor mode.");
                if (GUILayout.Button("Shutdown"))
                {
                    SubsystemManager.ForceShutdown_Editor();
                }

                return true;
            }
            
            
            GUILayout.Label("Subsystem Manager is not initialized.");
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Initialize"))
            {
                SubsystemManager.ForceInitialize_Editor();
            }

            
            return true;
        }


        private void DisplayFields(object obj, string prefix = "")
        {
            if (obj == null) return;

            var fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var properties = obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            // var methods = obj.GetType()
            // .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            EditorGUILayout.Space();
            GUILayout.Label("Fields", EditorStyles.miniBoldLabel);
            foreach (var field in fields)
            {
                if (
                    // field.IsDefined(typeof(NonSerializedAttribute), false) || 
                    field.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false))
                    continue;

                object value = field.GetValue(obj);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"{prefix}{field.Name}", GUILayout.Width(200));
                if (value == null)
                {
                    EditorGUILayout.LabelField("null");
                }
                // else if (value.GetType().IsClass && value.GetType() != typeof(string))
                // {
                //     EditorGUILayout.LabelField(value.GetType().Name);
                //     DisplayFields(value, $"{prefix}{field.Name}.");
                // }
                else
                {
                    EditorGUILayout.LabelField(value.ToString());
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            GUILayout.Label("Properties", EditorStyles.miniBoldLabel);
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false))
                    continue;

                object value = property.GetValue(obj);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"{prefix}{property.Name}", GUILayout.Width(200));
                if (value == null)
                {
                    EditorGUILayout.LabelField("null");
                }
                else
                {
                    EditorGUILayout.LabelField(value.ToString());
                }

                EditorGUILayout.EndHorizontal();
            }


            // EditorGUILayout.LabelField("Methods");
            // foreach (var method in methods)
            // {
            //     if (
            //         method.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false) ||
            //         method.IsDefined(typeof(System.ObsoleteAttribute), false))
            //         continue;
            //
            //     EditorGUILayout.BeginHorizontal();
            //     EditorGUILayout.LabelField($"{prefix}{method.Name}", GUILayout.Width(200));
            //     EditorGUILayout.EndHorizontal();
            // }
        }
    }
}