using UnityEngine;

namespace Crosline.DebugTools.Gizmos.Editor {
    public static partial class CroslineGizmos {

        public static void DrawPath(Vector3[] points, bool drawPoints = false, bool arrowPoints = false) {
            if (points == null)
                return;

            for (int i = 0; i < points.Length - 1; i++) {
                UnityEngine.Gizmos.DrawLine(points[i], points[i + 1]);
                
                if (drawPoints) {
                    if (arrowPoints) {
                        DrawTriangle(points[i], points[i + 1] - points[i]);
                        continue;
                    }
                    DrawPoint(points[i]);
                }
            }
        }

        public static void DrawPoint(Vector3 point, float radius = 0.05f) {
            UnityEngine.Gizmos.DrawSphere(point, radius);
        }

    }
}
