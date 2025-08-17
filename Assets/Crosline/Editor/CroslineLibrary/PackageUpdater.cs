using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine.Networking;

namespace Crosline.CroslineLibrary.Editor {
    public class PackageUpdaterWindow : EditorWindow {
        public static PackageUpdaterWindow packageUpdaterWindow;

        private const string URL = "https://api.github.com/repos/Crosline/Library/tags";

        private static string ManifestPath =>
            $"{Application.dataPath.Remove(Application.dataPath.Length - "/Assets".Length, "/Assets".Length)}{Path.DirectorySeparatorChar}Packages{Path.DirectorySeparatorChar}manifest.json";

        private const string LibrarySuffix = "com.crosline.library";
        private static string _currentVersion = "0.0.0";
        private static string _latestVersion = "0.0.0";

        private static bool IsInteractable = false;

        [MenuItem("Crosline/Update Crosline Library", false, 999)]
        public static void CroslinePackageUpdater() {
            packageUpdaterWindow =
                (PackageUpdaterWindow)GetWindow(typeof(PackageUpdaterWindow), false, "Crosline Package Updater");

            packageUpdaterWindow.Show();
            packageUpdaterWindow.minSize = new Vector2(450, 250);
            packageUpdaterWindow.maxSize = packageUpdaterWindow.minSize;
        }

        private void OnGUI() {
            
            GUILayout.BeginVertical();
            GUILayout.Label($"Latest Version: {_latestVersion}");
            GUILayout.Label($"Current Version: {_currentVersion}");
            GUILayout.EndVertical();

            if (_currentVersion == _latestVersion) {
                GUILayout.Label("There is nothing to update.");
                GUILayout.FlexibleSpace();

                return;
            }

            if (!IsInteractable) {
                GUILayout.Label("Please wait while package version is loading.");
                GUILayout.FlexibleSpace();

                return;
            }

            if (GUILayout.Button("Refresh")) {
                GetCurrentVersion();
                MakeRequest();
            }

            if (GUILayout.Button("Try Update")) {
                TryUpdateVersion();
            }
        }


        private static bool TryUpdateVersion() {
            var settings = ScriptableSingleton<CroslineLibrarySettings>.instance;

            if (settings == null) {
                return false;
            }
            
            settings.Save();
            
            if (settings.Version.Equals("development"))
                return true;

            if (settings.Version == _latestVersion)
                return false;

            var manifestContent = string.Empty;

            using (StreamReader reader = new StreamReader(ManifestPath)) {
                while (reader.Peek() >= 0) {
                    var line = reader.ReadLine();

                    if (string.IsNullOrEmpty(line) || !line.Contains(LibrarySuffix)) {
                        manifestContent += line + "\n";

                        continue;
                    }

                    var newLine =
                        $"    \"com.crosline.library\": \"https://github.com/Crosline/Library.git?path=/Assets/Crosline#{_latestVersion}\",\n";

                    manifestContent += newLine;

                    settings.Version = _latestVersion;
                    settings.Save();
                }
            }

            using (StreamWriter writer = new StreamWriter(ManifestPath))
                writer.Write(manifestContent);

            return true;
        }

        private static bool GetCurrentVersion() {
            var settings = ScriptableSingleton<CroslineLibrarySettings>.instance;

            if (settings == null) {
                return false;
            }
            
            settings.Save();

            if (settings.Version.Equals("development"))
                return true;

            settings.Version = "0.0.0";
            settings.Save();

            using (StreamReader reader = new StreamReader(ManifestPath)) {
                while (reader.Peek() >= 0) {
                    var line = reader.ReadLine();

                    if (string.IsNullOrEmpty(line) || !line.Contains(LibrarySuffix))
                        continue;

                    var hashIndex = line.IndexOf('#');
                    var lastPart = line.Substring(hashIndex + 1);
                    _currentVersion = lastPart.Replace("\"", string.Empty).Replace(",", string.Empty);
                    settings.Version = _currentVersion;
                    settings.Save();

                    return true;
                }
            }

            return true;
        }

        public void OnEnable() {
            MakeRequest();
            GetCurrentVersion();
        }

        private void MakeRequest() {
            EditorCoroutineUtility.StartCoroutine(MakeRequestRoutine(), this);
        }

        IEnumerator MakeRequestRoutine() {
            IsInteractable = false;
            UnityWebRequest request = UnityWebRequest.Get(URL);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(request.error);
            }
            else {
                foreach (var line in request.downloadHandler.text.Split("\n")) {
                    if (line.Contains("name")) {
                        var versionLine = line;
                        versionLine = versionLine.Replace("\"", string.Empty);
                        versionLine = versionLine.Replace(",", string.Empty);
                        versionLine = versionLine.Replace("name", string.Empty);
                        versionLine = versionLine.Replace(":", string.Empty);
                        versionLine = versionLine.Replace("\r", string.Empty);
                        versionLine = versionLine.Replace("\t", string.Empty);
                        versionLine = versionLine.Replace(" ", string.Empty);

                        _latestVersion = versionLine;
                        IsInteractable = true;

                        break;
                    }
                }
            }
        }
    }
}