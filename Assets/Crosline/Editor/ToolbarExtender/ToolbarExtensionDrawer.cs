using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Crosline.UnityTools;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

namespace Crosline.ToolbarExtender.Editor {
    public class ToolbarExtensionDrawer {

        private ScriptableObject _currentToolbar;
        private VisualElement _root;
        private Dictionary<ToolbarZone, VisualElement> _parents;

        private int lastInstanceID;

        private readonly Type
#if UNITY_EDITOR
            _toolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");
#else
            _toolbarType;
#endif

        private HashSet<MethodInfo> _methods = new();

        internal void TryDrawToolbar() {
#if UNITY_EDITOR
            EditorApplication.update += OnGUI;
#endif
        }

        private void OnGUI() {
#if UNITY_EDITOR
            EditorApplication.update -= OnGUI;
#endif

            if (_currentToolbar == null) {
                var toolbars = Resources.FindObjectsOfTypeAll(_toolbarType);
                _currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
            }

            if (_currentToolbar == null)
                return;

            if (_root == null) {
                _root = _currentToolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(_currentToolbar) as VisualElement;
            }

            if (_root == null)
                return;


            if (_parents == null) {
                var tempParents = new Dictionary<ToolbarZone, VisualElement> {
                    {ToolbarZone.LeftAlign, null},
                    {ToolbarZone.MiddleLeftAlign, null},
                    {ToolbarZone.MiddleRightAlign, null},
                    {ToolbarZone.RightAlign, null}
                };

                _parents = new Dictionary<ToolbarZone, VisualElement>(tempParents);

                foreach (var toolbarParent in _parents) {
                    tempParents[toolbarParent.Key] = PrepareParent(toolbarParent.Value, toolbarParent.Key);
                }

                _parents = tempParents;
            }

            AttachToolbars();
        }

        public void SetAvailableMethods(IEnumerable<MethodInfo> methods) {
            _methods = new HashSet<MethodInfo>(methods);
        }

        private VisualElement CreateToolbarButton(string icon, Action onClick, string tooltip = null) {
            var buttonVE = new Button(onClick);
            buttonVE.text = icon;
            buttonVE.tooltip = tooltip;
            FitChildrenStyle(buttonVE);

            var iconVE = new VisualElement();
            iconVE.AddToClassList("unity-editor-toolbar-element__icon");
#if UNITY_2021_2_OR_NEWER && UNITY_EDITOR
            // iconVE.style.backgroundImage = Background.FromTexture2D((Texture2D)EditorGUIUtility.IconContent(icon).image);
            iconVE.style.height = 16;
            iconVE.style.width = 16;
            iconVE.style.alignSelf = Align.Center;
#elif UNITY_EDITOR
            iconVE.style.backgroundImage = Background.FromTexture2D(EditorGUIUtility.FindTexture(icon));
#endif
            buttonVE.Add(iconVE);

            return buttonVE;
        }

        private void FitChildrenStyle(VisualElement element) {
            element.AddToClassList("unity-toolbar-button");
            element.AddToClassList("unity-editor-toolbar-element");
            element.RemoveFromClassList("unity-button");
        }

        private void AttachToolbars() {
            var toolbarButtons =
                _methods.ToDictionary(method => method, method => method.GetCustomAttribute<ToolbarAttribute>());

            foreach (var attr in toolbarButtons.OrderByDescending(x => x.Value.Order)) {
                var parent = _parents[attr.Value.ToolbarZone];
                parent.Add(CreateToolbarButton(attr.Value.Label, () => attr.Key.Invoke(null, null), attr.Value.ToolTip));

                _parents[attr.Value.ToolbarZone] = parent;
            }
        }

        private VisualElement PrepareParent(VisualElement parent, ToolbarZone toolbarZoneAlign) {
            RemoveCurrentParent(ref parent);

            if (parent != null)
                return parent;

            int flex = ToolbarZone.Middle.HasFlag(toolbarZoneAlign)
                ? 1 : 0;

            parent = new VisualElement() {
                style = {
                    flexGrow = flex,
                    flexDirection = FlexDirection.Row
                }
            };

            parent.Add(new VisualElement() {
                style = {
                    flexGrow = flex,
                }
            });


            var toolbarZoneName = ToolbarZone.Left.HasFlag(toolbarZoneAlign)
                ? "ToolbarZoneLeftAlign"
                : "ToolbarZoneRightAlign";

            var toolbarZoneElement = _root.Q(toolbarZoneName);
            toolbarZoneElement.Add(parent);
            return parent;
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
