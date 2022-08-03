using UnityEngine;

namespace Crosline.DebugTools.Gizmos.Editor {
    public static partial class Gizmos {

        public static void DrawPath(Vector3[] points) {
            if (points == null)
                return;

            for (int i = 0; i < points.Length - 1; i++) {
                UnityEngine.Gizmos.DrawLine(points[i], points[i + 1]);
            }
        }

    }
}
