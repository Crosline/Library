using UnityEditor;
using UnityEditor.SceneManagement;
using SceneManager = UnityEngine.SceneManagement;

namespace Crosline.SceneManager.Editor
{
    [InitializeOnLoad]
    public static class EditorBuildSettingsSceneInjector {
        
        static EditorBuildSettingsSceneInjector()
        {
            if (EditorBuildSettings.scenes.Length < 3) {
                EditorBuildSettings.scenes = new EditorBuildSettingsScene[]
                {
                    new EditorBuildSettingsScene("Assets/Scenes/Initialize.unity", true),
                    new EditorBuildSettingsScene("Assets/Scenes/Loading.unity", true),
                    new EditorBuildSettingsScene("Assets/Scenes/Main.unity", true)
                };
            }
        }
    }
}