using UnityEditor;

namespace Crosline.UnityTools.Editor {
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Component), true)]
    public class ComponentCustomEditor : UnityEditor.Editor { }
}
