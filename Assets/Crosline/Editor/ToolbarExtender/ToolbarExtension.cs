using System;
using System.Collections.Generic;
using Crosline.UnityTools.Editor;
using UnityEditor;

namespace Crosline.ToolbarExtender.Editor {
    
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class ToolbarExtension {
        private static int _toolCount;

        public static readonly HashSet<Action> ToolbarGUI = new HashSet<Action>();

        private static ToolbarExtensionDrawer _drawer;

        static ToolbarExtension() {
            _drawer = new ToolbarExtensionDrawer();
            
            _drawer.SetAvailableMethods(AttributeFinder.TryFindMethods<ToolbarAttribute>());

            _drawer.TryDrawToolbar();
        }
        
    }
}