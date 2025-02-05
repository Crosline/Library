using System;
using System.Collections.Generic;
// using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct TodoData {
    [HideInInspector] public string ScriptPath;
    [HideInInspector] public int Line;

    [HideInInspector] public string Todo;

    [HideInInspector] public string TodoHeader;

    public TodoData(string scriptPath, int line, string todo, string todoHeader) {
        ScriptPath = scriptPath;
        Line = line;
        Todo = todo;
        TodoHeader = todoHeader;
    }

    public struct TodoListData {
        // [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true, DraggableItems = false,
            // NumberOfItemsPerPage = 1000)]
        public List<TodoData> TodoList;

        public TodoListData(List<TodoData> todoList) {
            TodoList = todoList;
        }
    }


    public struct TodoLists {
        // [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine, IsReadOnly = true, KeyLabel = "Topic",
            // ValueLabel = "To do")]
        public Dictionary<string, TodoListData> TodoByTopic;

        [HideInInspector] public List<string> Topics;

        public TodoLists(Dictionary<string, TodoListData> todoByTopic, List<string> topics) {
            TodoByTopic = todoByTopic;
            Topics = topics;
        }
    }

    public void GotoContext() {
        var loadedAsset = AssetDatabase.LoadAssetAtPath(ScriptPath, typeof(object));
        AssetDatabase.OpenAsset(loadedAsset.GetInstanceID(), Line);
    }

    private static GUIStyle _ButtonStyle;

    private static GUIStyle ButtonStyle
    {
        get
        {
            if (_ButtonStyle == null) {
                _ButtonStyle = new GUIStyle(GUI.skin.button);
                _ButtonStyle.alignment = TextAnchor.MiddleLeft;
            }

            return _ButtonStyle;
        }
    }

    // [OnInspectorGUI]
    private void DrawTextAsButton() {
        if (GUILayout.Button(Todo, ButtonStyle)) GotoContext();
    }
}