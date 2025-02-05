using UnityEditor;
using UnityEngine;

namespace Crosline.UnityTools.Editor {
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableObject), true)]
    public class ScriptableObjectCustomEditor : UnityEditor.Editor { }
}
