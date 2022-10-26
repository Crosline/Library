using System;
using System.Collections.Generic;
using Crosline.UnityTools;
using UnityEditor;

namespace Crosline.ToolbarExtender.Editor {
    
    [InitializeOnLoad]
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