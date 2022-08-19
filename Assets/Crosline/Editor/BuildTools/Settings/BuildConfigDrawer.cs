using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools.Editor.Settings {
    public class BuildConfigDrawer {
        private BuildConfigAsset _buildConfigAsset;

        public void DrawBuildConfigGUI() {
            _buildConfigAsset = BuildSettingsWindow.buildConfigAsset;

            if (_buildConfigAsset == null)
                return;
            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinWidth(360));

            var buildConfigName = EditorGUILayout.TextField("Asset Name", _buildConfigAsset.name);
            _buildConfigAsset.platform = (BuildOptions.BuildPlatform) EditorGUILayout.EnumPopup("platform", _buildConfigAsset.platform);
            _buildConfigAsset.backend = (BuildOptions.ScriptingBackend) EditorGUILayout.EnumPopup("Backend", _buildConfigAsset.backend);
            _buildConfigAsset.buildMode = (BuildOptions.BuildMode) EditorGUILayout.EnumPopup("Build Mode", _buildConfigAsset.buildMode);
            _buildConfigAsset.apiCompability = (BuildOptions.ApiCompability) EditorGUILayout.EnumPopup("API Compability", _buildConfigAsset.apiCompability);

            EditorGUILayout.EndVertical();

            if (buildConfigName != _buildConfigAsset.name) {
                BuildSettingsManager.RenameConfig(_buildConfigAsset, buildConfigName);
            }
        }
    }
}
