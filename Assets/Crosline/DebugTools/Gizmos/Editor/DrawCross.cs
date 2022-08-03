using UnityEngine;

namespace Crosline.DebugTools.Gizmos.Editor {
    public static partial class Gizmos {
        public static void DrawCross(Vector3 position, float size) {
            float d = size / 2.0f;
            Vector3 p = position;
            UnityEngine.Gizmos.DrawLine(p + Vector3.left * d, p + Vector3.right * d);
            UnityEngine.Gizmos.DrawLine(p + Vector3.up * d, p + Vector3.down * d);
            UnityEngine.Gizmos.DrawLine(p + Vector3.back * d, p + Vector3.forward * d);
        }
    }
}
