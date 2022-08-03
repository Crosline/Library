using UnityEditor;
using UnityEngine;

namespace Crosline.Builder.Editor.Settings
{
    public class BuildConfigDrawer
    {
        private BuildConfigAsset _buildConfigAsset;
        
        public void DrawBuildConfigGUI()
        {
            _buildConfigAsset = BuildSettingsWindow.buildConfigAsset;
            if (_buildConfigAsset == null)
                return;
            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinWidth(360));
            
            var buildConfigName = EditorGUILayout.TextField("Asset Name", _buildConfigAsset.name);
            _buildConfigAsset.platform = (BuildConfigAsset.BuildPlatform) EditorGUILayout.EnumPopup("Platform", _buildConfigAsset.platform);
            _buildConfigAsset.backend = (BuildConfigAsset.ScriptingBackend) EditorGUILayout.EnumPopup("Backend", _buildConfigAsset.backend);
            _buildConfigAsset.buildMode = (BuildConfigAsset.BuildMode) EditorGUILayout.EnumPopup("Build Mode", _buildConfigAsset.buildMode);
            _buildConfigAsset.apiCompability = (BuildConfigAsset.ApiCompability) EditorGUILayout.EnumPopup("API Compability", _buildConfigAsset.apiCompability);

            EditorGUILayout.EndVertical();

            if (buildConfigName != _buildConfigAsset.name)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(_buildConfigAsset), buildConfigName);
                _buildConfigAsset.name = buildConfigName;
                BuildSettingsWindow.RefreshAvailableAssets();
            }
        }
    }
}
