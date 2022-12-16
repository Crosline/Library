using UnityEditor;
using UnityEditor.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Crosline.SceneManager.Editor
{
    [InitializeOnLoad]
    public static class LoadMainScene {
        private const string LastSceneKey = "CroslineLastUsedScene";
        
        static LoadMainScene()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.EnteredPlayMode)
                return;

            if (playModeStateChange == PlayModeStateChange.ExitingEditMode) {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
                    EditorPrefs.SetString(LastSceneKey, UnitySceneManager.GetActiveScene().path);
                    
                    var firstScenePath = EditorBuildSettings.scenes[0].path;
                    EditorSceneManager.OpenScene(firstScenePath, OpenSceneMode.Single);
                }
            }

            if (playModeStateChange == PlayModeStateChange.EnteredEditMode) {
                var lastScenePath = EditorPrefs.GetString(LastSceneKey, UnitySceneManager.GetActiveScene().path);
                EditorSceneManager.OpenScene(lastScenePath);
            }
        }
    }
}
