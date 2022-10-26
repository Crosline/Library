using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crosline.UnityTools.Editor.ToolbarExtender {
    public class ToolbarExtensionDrawer {
        private static GUIStyle _buttonStyle = null;

        private static ScriptableObject _currentToolbar;
        private static VisualElement _leftParent;
        private static VisualElement _rightParent;

        private static int lastInstanceID;

        private static readonly Type _toolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");

        private static HashSet<MethodInfo> _methods = new();

        internal void TryDrawToolbar() {
            EditorApplication.update += OnGUI;
        }

        private void OnGUI() {
            EditorApplication.update -= OnGUI;

            if (_currentToolbar == null) {
                var toolbars = Resources.FindObjectsOfTypeAll(_toolbarType);
                _currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
            }

            if (_currentToolbar == null)
                return;

            PrepareParent(ref _leftParent, "ToolbarZoneLeftAlign");
            PrepareParent(ref _rightParent, "ToolbarZoneRightAlign");

            AttachToolbars();
        }

        public void SetAvailableMethods(IEnumerable<MethodInfo> methods) {
            _methods = new HashSet<MethodInfo>(methods);
        }

        private VisualElement CreateToolbarButton(string icon, Action onClick, string tooltip = null) {
            var buttonVE = new Button(onClick);
            buttonVE.tooltip = tooltip;
            FitChildrenStyle(buttonVE);

            var iconVE = new VisualElement();
            iconVE.AddToClassList("unity-editor-toolbar-element__icon");
#if UNITY_2021_2_OR_NEWER
            iconVE.style.backgroundImage = Background.FromTexture2D((Texture2D)EditorGUIUtility.IconContent(icon).image);
            iconVE.style.height = 16;
            iconVE.style.width = 16;
            iconVE.style.alignSelf = Align.Center;
#else
            iconVE.style.backgroundImage = Background.FromTexture2D(EditorGUIUtility.FindTexture(icon));
#endif
            buttonVE.Add(iconVE);

            return buttonVE;
        }

        private void FitChildrenStyle(VisualElement element) {
            element.AddToClassList("unity-toolbar-button");
            element.AddToClassList("unity-editor-toolbar-element");
            element.RemoveFromClassList("unity-button");
#if UNITY_2021_2_OR_NEWER
            element.style.paddingRight = 8;
            element.style.paddingLeft = 8;
            element.style.justifyContent = Justify.Center;
            element.style.display = DisplayStyle.Flex;
            element.style.borderTopLeftRadius = 2;
            element.style.borderTopRightRadius = 2;
            element.style.borderBottomLeftRadius = 2;
            element.style.borderBottomRightRadius = 2;

            element.style.marginRight = 1;
            element.style.marginLeft = 1;
#else
            element.style.marginRight = 2;
            element.style.marginLeft = 2;
#endif
        }

        private void AttachToolbars() {
            var toolbarButtons =
                _methods.ToDictionary(method => method, method => method.GetCustomAttribute<ToolbarAttribute>());

            foreach (var attr in toolbarButtons.OrderByDescending(x => x.Value.order)) {
                var parent = attr.Value.direction == Direction.Left ? _leftParent : _rightParent;
                parent.Add(CreateToolbarButton(attr.Value.iconName, () => attr.Key.Invoke(null, null), attr.Value.toolName));
            }
        }

        private void PrepareParent(ref VisualElement parent, string toolbarZoneAlign) {
            RemoveCurrentParent(ref parent);
            
            if (parent != null)
                return;

            var root = _currentToolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(_currentToolbar);

            if (root == null)
                return;

            var mRoot = root as VisualElement;

            parent = new VisualElement()
            {
                style =
                {
                    flexGrow = 1,
                    flexDirection = FlexDirection.Row
                }
            };

            parent.Add(new VisualElement()
            {
                style =
                {
                    flexGrow = 1
                }
            });

            var toolbarZoneElement = mRoot.Q(toolbarZoneAlign);
            toolbarZoneElement.Add(parent);
        }

        private void RemoveCurrentParent(ref VisualElement parent) {
            if (parent != null && _currentToolbar.GetInstanceID() != lastInstanceID) {
                parent.RemoveFromHierarchy();
                parent = null;
                lastInstanceID = _currentToolbar.GetInstanceID();
            }
        }
    }
}