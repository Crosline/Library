using Crosline.UnityTools.Editor;
using UnityEditor;

namespace Crosline.ToolbarExtender.Editor {
    
    [InitializeOnLoad]
    public static class ToolbarExtension {
        static ToolbarExtension() {
            var drawer = new ToolbarExtensionDrawer();
            
            drawer.SetAvailableMethods(AttributeFinder.TryFindMethods<ToolbarAttribute>());

            drawer.TryDrawToolbar();
        }
        
    }
}