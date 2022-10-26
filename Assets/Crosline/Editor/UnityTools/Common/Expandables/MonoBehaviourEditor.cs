using UnityEditor;
using UnityEngine;

namespace Crosline.UnityTools.Editor {
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourEditor : UnityEditor.Editor { }
}
