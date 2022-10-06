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
            _buildConfigAsset.version = EditorGUILayout.TextField("Version", _buildConfigAsset.version);
            _buildConfigAsset.bundle = EditorGUILayout.TextField("Bundle Id", _buildConfigAsset.bundle);
            _buildConfigAsset.platform = (BuildOptions.BuildPlatform) EditorGUILayout.EnumPopup("Platform", _buildConfigAsset.platform);
            _buildConfigAsset.backend = (BuildOptions.ScriptingBackend) EditorGUILayout.EnumPopup("Backend", _buildConfigAsset.backend);
            _buildConfigAsset.stripping = (BuildOptions.ManagedStrippingLevel) EditorGUILayout.EnumPopup("Stripping", _buildConfigAsset.stripping);
            _buildConfigAsset.compression = (BuildOptions.Compression) EditorGUILayout.EnumPopup("Compression", _buildConfigAsset.compression);
            _buildConfigAsset.buildMode = (BuildOptions.BuildMode) EditorGUILayout.EnumPopup("Build Mode", _buildConfigAsset.buildMode);
            _buildConfigAsset.apiCompability = (BuildOptions.ApiCompability) EditorGUILayout.EnumPopup("API Compability", _buildConfigAsset.apiCompability);

            if (_buildConfigAsset is MobileBuildConfigAsset mobileAsset) {
                mobileAsset.screenOrientation = (ScreenOrientation) EditorGUILayout.EnumPopup("Screen Orientation", mobileAsset.screenOrientation);
            }
            if (_buildConfigAsset is AndroidBuildConfigAsset androidAsset) {
                androidAsset.architecture = (BuildOptions.Architecture) EditorGUILayout.EnumPopup("Android Architecture", androidAsset.architecture);
            }

            EditorGUILayout.EndVertical();

            if (buildConfigName != _buildConfigAsset.name) {
                BuildSettingsManager.RenameConfig(_buildConfigAsset, buildConfigName);
            }
        }
    }
}
