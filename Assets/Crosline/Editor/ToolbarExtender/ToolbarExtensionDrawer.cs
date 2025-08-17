using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        private int _lastInstanceID;

        private readonly Type
            _toolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");

        private HashSet<MethodInfo> _methods;

        internal void TryDrawToolbar() {
            EditorApplication.delayCall += () =>
            {
                Initialize();

                EditorApplication.delayCall += OnGUI;
            };
        }

        private void Initialize() {
            var toolbars = Resources.FindObjectsOfTypeAll(_toolbarType);
            _currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;

            _root = _currentToolbar?.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(_currentToolbar) as VisualElement;

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

            _parents = new Dictionary<ToolbarZone, VisualElement>(tempParents);
        }

        private void OnGUI() {
            if (_currentToolbar == null)
                return;

            if (_root == null)
                return;

            if (_parents == null)
                return;

            AttachToolbars<ToolbarButtonAttribute>();
        }

        public void SetAvailableMethods(IEnumerable<MethodInfo> methods) {
            _methods = new HashSet<MethodInfo>(methods);
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
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

        private void AttachToolbars<T>() where T : ToolbarAttribute {
            var toolbarButtons =
                _methods.ToDictionary(method => method, method => method.GetCustomAttribute<T>());
            
            foreach (var attr in toolbarButtons.OrderByDescending(x => x.Value.Order)) {

                if (!attr.Key.IsStatic) {
                    throw new InvalidOperationException(
                        $"Method {attr.Key.Name} is not a static method. Please, use static methods.");
                }

                var parent = _parents[attr.Value.ToolbarZone];
                var toolbarElement = attr.Value.CreateVisualElement();
                parent.Add(CreateToolbarButton(attr.Value.Label, () => attr.Key.Invoke(null, null), attr.Value.ToolTip));

                _parents[attr.Value.ToolbarZone] = parent;
            }
        }

        private VisualElement PrepareParent(VisualElement parent, ToolbarZone toolbarZoneAlign) {
            RemoveCurrentParent(ref parent);

            if (parent != null)
                return parent;

            var flex = ToolbarZone.Middle.HasFlag(toolbarZoneAlign)
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
            if (parent == null || _currentToolbar.GetInstanceID() == _lastInstanceID)
                return;

            parent.RemoveFromHierarchy();
            parent = null;
            _lastInstanceID = _currentToolbar.GetInstanceID();
        }
    }
}
