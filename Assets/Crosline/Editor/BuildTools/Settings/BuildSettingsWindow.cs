using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools.Editor.Settings {
    public class BuildSettingsWindow : EditorWindow {
        public static BuildSettingsWindow buildSettingsWindow;

        public static BuildConfigAsset buildConfigAsset;

        private BuildConfigDrawer _buildConfigDrawer;

        private SerializedObject _serializedObject;

        [MenuItem("Crosline/Subsystems/Build Settings")]
        public static void Initialize() {
            buildSettingsWindow = (BuildSettingsWindow) GetWindow(typeof(BuildSettingsWindow), false, "Build Settings");
            buildSettingsWindow.Show();
            buildSettingsWindow.minSize = new Vector2(720, 360);
            buildSettingsWindow.maxSize = new Vector2(740, 420);
        }

        public static bool IsOpen() {
            return buildSettingsWindow != null;
        }

        private void OnEnable() {
            if (buildConfigAsset == null)
                buildConfigAsset = BuildSettingsManager.TryGetConfig(ref _error, _selectedBuildPlatform);

            if (buildConfigAsset != null) {
                _selectedCustomName = buildConfigAsset.name;
                _selectedBuildPlatform = buildConfigAsset.platform;
            }

            _serializedObject = new SerializedObject(buildConfigAsset);

            _buildConfigDrawer = new BuildConfigDrawer();

            RefreshAvailableAssets();
        }

        private static BuildOptions.BuildPlatform _selectedBuildPlatform = BuildOptions.BuildPlatform.Windows;

        private static string _selectedCustomName = "";

        private static string[] _availableAssets;

        private static string _error = "";

        private Vector2 _scrollPos = Vector2.zero;

        private void OnGUI() {
            _serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            DrawToolbar();

            EditorGUILayout.BeginHorizontal();

            DrawAvailableAssets();
            _buildConfigDrawer.DrawBuildConfigGUI();

            EditorGUILayout.EndHorizontal();

            if (!string.IsNullOrEmpty(_error))
                EditorGUILayout.HelpBox(_error, MessageType.Error);
#if UNITY_2021_1_OR_NEWER
            AssetDatabase.SaveAssetIfDirty(buildConfigAsset);
#else
            AssetDatabase.SaveAssets();
#endif
        }

        private void DrawToolbar() {

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            _selectedCustomName = EditorGUILayout.TextField(_selectedCustomName, GUILayout.MinWidth(100), GUILayout.Width(200), GUILayout.MaxWidth(480));
            GUILayout.FlexibleSpace();

            var oldSelectedPlatform = _selectedBuildPlatform;
            _selectedBuildPlatform = (BuildOptions.BuildPlatform) EditorGUILayout.EnumPopup(_selectedBuildPlatform, GUILayout.Width(75));

            if (oldSelectedPlatform != _selectedBuildPlatform) {
                GetConfigWithCustomName("", _selectedBuildPlatform);
                _selectedCustomName = "";
            }

            if (GUILayout.Button("Build", EditorStyles.toolbarButton))
                _error = "Build is not implemented yet."; //TODO - Crosline: Don't forget to connect build to here.

            if (GUILayout.Button("Create", EditorStyles.toolbarButton))
                GetConfigWithCustomName(_selectedCustomName, _selectedBuildPlatform);

            if (GUILayout.Button("Remove", EditorStyles.toolbarButton))
                RemoveConfigWithCustomName(_selectedCustomName);

            EditorGUILayout.EndHorizontal();

        }

        private void DrawAvailableAssets() {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MaxWidth(360));

            foreach (var asset in _availableAssets) {
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(asset, GUILayout.MinWidth(200), GUILayout.MaxWidth(300)))
                    GetConfigWithCustomName(asset, _selectedBuildPlatform);

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Remove", EditorStyles.toolbarButton, GUILayout.Width(60)))
                    RemoveConfigWithCustomName(asset);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        public static void RefreshAvailableAssets() {
            _availableAssets = BuildSettingsManager.GetAllAvailableConfigs();
        }

        private static void GetConfigWithCustomName(string customName, BuildOptions.BuildPlatform buildPlatform = BuildOptions.BuildPlatform.Windows) {
            _error = "";
            buildConfigAsset = BuildSettingsManager.TryGetConfig(ref _error, buildPlatform, customName);
            _selectedBuildPlatform = buildConfigAsset.platform;
            _selectedCustomName = buildConfigAsset.name;

            RefreshAvailableAssets();
        }

        private static void RemoveConfigWithCustomName(string assetName) {
            _error = "";
            BuildSettingsManager.TryRemoveConfig(ref _error, assetName);
            RefreshAvailableAssets();
        }
    }

}
