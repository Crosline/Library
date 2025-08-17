// Thanks to Panteon's MidCore team
using System;
using System.Collections.Generic;
using System.IO;
using Crosline.DataTools;
using Crosline.SystemTools;
using Crosline.UnityTools.Editor;
// using Sirenix.OdinInspector;
// using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using FilePathAttribute = UnityEditor.FilePathAttribute;

namespace UnityTools.Editor {
    [FilePath("Assets/Settings/Resources/TodoHelper/TodoHelper.asset", FilePathAttribute.Location.ProjectFolder)]
    public class TodoHelper : CroslineScriptableSingleton<TodoHelper> {
        [SerializeField] private TodoHelperConfiguration Configuration;

        private Dictionary<string, TodoData.TodoLists> _todoList = new Dictionary<string, TodoData.TodoLists>();
        private static string ApplicationPath => Application.dataPath;

        private static bool _isCountingTodos;
        
        [InitializeOnLoadMethod]
        private static void CreateIfNotExists() {
            EditorApplication.delayCall += (() =>
            {
                GetOrCreate();
            });
        }

        protected override string AssetDirectory => "Assets/Settings/Resources/TodoHelper";

        protected override void SetDefaults() {
            var todoHelper = TodoHelperConfiguration.GetOrCreate();
            Configuration = todoHelper;
        }
        

        [MenuItem("Crosline/Todo Helper/Select Todo Config", false, 0)]
        public static void SelectConfigurationTool() {
            Selection.activeObject = ScriptableSingleton<TodoHelperConfiguration>.instance;
        }

        [MenuItem("Crosline/Todo Helper/Select Helper", false, 0)]
        public static void SelectTodoAsset() {
            Selection.activeObject = ScriptableSingleton<TodoHelper>.instance;
        }

        // [Button(ButtonSizes.Gigantic)]
        public void SearchForTodo() {
            if (_isCountingTodos) {
                return;
            }

            _isCountingTodos = true;

            _todoList = new Dictionary<string, TodoData.TodoLists>(Configuration.AssigneeNames.Length);

            foreach (var assigneeName in Configuration.AssigneeNames)
                _todoList.Add(assigneeName,
                    new TodoData.TodoLists(new Dictionary<string, TodoData.TodoListData>(1000), new List<string>(1000)));

            _todoList.Add("Unassigned",
                new TodoData.TodoLists(new Dictionary<string, TodoData.TodoListData>(1000), new List<string>(1000)));

            foreach (var directoryPath in Configuration.FoldersToSearchTodo) {
                var dir = new DirectoryInfo(directoryPath);
                var info = dir.GetFiles("*.cs", SearchOption.AllDirectories);

                foreach (var f in info) {
                    // Skip auto generated codes.
                    if (f.Name.Equals("GeneratedCodes.cs")) continue;

                    foreach (var todoTyping in Configuration.PossibleIgnoreCaseTodoTypings) {
                        var indexOf = 0;

                        while (true) {
                            var content = File.ReadAllText(f.FullName);

                            indexOf = content.IndexOf(todoTyping, indexOf, StringComparison.OrdinalIgnoreCase);

                            if (indexOf == -1) break;
                            var endOfLineIndex = content.IndexOf("\n", indexOf, StringComparison.InvariantCulture);

                            var checkedAllFile = false;

                            if (endOfLineIndex == -1) {
                                endOfLineIndex = content.Length;
                                checkedAllFile = true;
                            }

                            var todo = content.Substring(indexOf, endOfLineIndex - indexOf);

                            var isTodoAssigned = false;

                            var todoRemove = string.Empty;
                            var index = todo.IndexOf(':');

                            if (index != -1) todoRemove = todo.Substring(0, index);

                            foreach (var assigneeName in Configuration.AssigneeNames)
                                if (todoRemove.Contains(assigneeName, StringComparison.InvariantCultureIgnoreCase)) {
                                    AddToTodoList(f.FullName, content, todoRemove, todo, assigneeName);

                                    isTodoAssigned = true;
                                }

                            if (!isTodoAssigned) AddToTodoList(f.FullName, content, todoRemove, todo, "Unassigned");

                            indexOf = endOfLineIndex;

                            if (checkedAllFile) break;
                        }
                    }


                    // var open = f.Open(FileMode.Open);
                }

                _isCountingTodos = false;
            }


            void AddToTodoList(string folderPath, string textToSearchForTodo, string removedTodoHeader, string todo,
                string assigneeName) {
                var relativePath = ApplicationPath.MakeRelativePath(folderPath);
                var index = todo.IndexOf(':');

                var todoToWrite = todo.Substring(index + 1);


                var topic = GetTopic(removedTodoHeader);
                if (string.IsNullOrWhiteSpace(topic)) topic = "Uncategorized";


                TodoData.TodoListData todoListToAdd;

                if (_todoList[assigneeName].Topics.Contains(topic)) {
                    todoListToAdd = _todoList[assigneeName].TodoByTopic[topic];
                }
                else {
                    _todoList[assigneeName].Topics.Add(topic);
                    _todoList[assigneeName].TodoByTopic.Add(topic, new TodoData.TodoListData(new List<TodoData>()));
                    todoListToAdd = _todoList[assigneeName].TodoByTopic[topic];
                }

                var occurenceAmount = 0;

                foreach (var todoData in todoListToAdd.TodoList)
                    if (todoData.Todo.Equals(todoToWrite))
                        occurenceAmount++;

                var lineNumber = GetLineNumber(textToSearchForTodo, todo, occurenceAmount, StringComparison.Ordinal);
                todoListToAdd.TodoList.Add(new TodoData(relativePath, lineNumber, todoToWrite, removedTodoHeader));
            }
        }

        private static int GetLineNumber(string text, string lineToFind, int indexToGetTextIfMultipleOccurence,
            StringComparison comparison = StringComparison.CurrentCulture) {
            var lineNum = 0;
            var count = -1;
            lineToFind = lineToFind.TrimEnd('\r');

            using (var reader = new StringReader(text)) {
                string line;

                while ((line = reader.ReadLine()) != null) {
                    lineNum++;

                    if (line.Contains(lineToFind, comparison)) {
                        count++;

                        if (indexToGetTextIfMultipleOccurence == count) return lineNum;
                    }
                }
            }

            return -1;
        }

        private static string GetTopic(string todo) {
            return todo.GetStringBetweenSeparator('-');
        }

        public int GetTodoCount(string assignee) {
            var count = 0;

            if (_todoList.TryGetValue(assignee, out var todoLists))
                foreach (var todoList in todoLists.TodoByTopic)
                foreach (var todoData in todoList.Value.TodoList)
                    if (todoData.TodoHeader.Contains(_criticalTodoTag, StringComparison.InvariantCultureIgnoreCase) ||
                        todoData.TodoHeader.Contains(_immediateTodoTag, StringComparison.InvariantCultureIgnoreCase))
                        count += 1;

            return count;
        }

        private const string _criticalTodoTag = "CRITICAL";
        private const string _immediateTodoTag = "IMMEDIATE";
    }
}