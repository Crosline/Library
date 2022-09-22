using UnityEditor;
using UnityEngine;

namespace Crosline.BuildTools.Editor.Settings {
    public static class BuildSettingsManager {
        private const string buildConfigAssetDirectory = "Assets/Crosline/Runtime/BuildTools/BuildConfig/";

        internal static BuildConfigAsset TryGetConfig(ref string error, BuildOptions.BuildPlatform buildPlatform = BuildOptions.BuildPlatform.Windows, string customName = "") {
            BuildConfigAsset buildConfigAsset = null;

            var name = string.IsNullOrEmpty(customName) ? $"BuildConfigAsset_{buildPlatform.ToString()}" : customName;

            var assetPath = $"{buildConfigAssetDirectory}{name}.asset";

            buildConfigAsset = AssetDatabase.LoadAssetAtPath<BuildConfigAsset>(assetPath);

            if (buildConfigAsset == null) {
                buildConfigAsset = ScriptableObject.CreateInstance<BuildConfigAsset>();
                buildConfigAsset.name = $"{name}";
                buildConfigAsset.platform = buildPlatform;

                if (!System.IO.Directory.Exists(buildConfigAssetDirectory)) {
                    System.IO.Directory.CreateDirectory(Application.dataPath + buildConfigAssetDirectory.Remove(0, "Assets".Length));
                }

                AssetDatabase.CreateAsset(buildConfigAsset, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                error = $"{name} created at {assetPath}";
            }

            return buildConfigAsset;
        }

        internal static void TryRemoveConfig(ref string error, string assetName) {
            if (string.IsNullOrEmpty(assetName)) {
                error = "File couldn't be removed, name shouldn't be empty.";

                return;
            }

            BuildConfigAsset buildConfigAsset = null;

            var assetPath = $"{buildConfigAssetDirectory}{assetName}.asset";

            buildConfigAsset = AssetDatabase.LoadAssetAtPath<BuildConfigAsset>(assetPath);

            if (buildConfigAsset != null) {
                AssetDatabase.DeleteAsset(assetPath);
                AssetDatabase.Refresh();
                error = $"{assetName} deleted from {assetPath}";
            }
        }

        internal static string[] GetAllAvailableConfigs() {
            string[] guids = AssetDatabase.FindAssets("t:BuildConfigAsset", new[] { buildConfigAssetDirectory });

            string[] assetNames = new string[guids.Length];

            for (int i = 0; i < guids.Length; i++) {
                assetNames[i] = AssetDatabase.LoadAssetAtPath<BuildConfigAsset>(AssetDatabase.GUIDToAssetPath(guids[i])).name;
            }

            return assetNames;
        }

        internal static void RenameConfig(ScriptableObject so, string name) {
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(so), name);
            so.name = name;
            BuildSettingsWindow.RefreshAvailableAssets();
        }

    }
}
