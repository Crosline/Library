using UnityEngine;

namespace Crosline.DebugTools {
    public static partial class CroslineGizmos {

        public static void DrawPath(Vector3[] points, bool drawPoints = false, bool arrowPoints = false) {
            if (points == null)
                return;

            for (int i = 0; i < points.Length - 1; i++) {
                UnityEngine.Gizmos.DrawLine(points[i], points[i + 1]);

                if (drawPoints) {
                    if (arrowPoints) {
                        if (i == 0) {
                            DrawTriangle(points[i], points[i + 1] - points[i]);
                        }

                        DrawTriangle(points[i + 1], points[i + 1] - points[i]);

                        continue;
                    }

                    if (i == 0) {
                        DrawPoint(points[i]);
                    }

                    DrawPoint(points[i + 1]);
                }
            }
        }

        public static void DrawPoint(Vector3 point, float radius = 0.05f) {
            UnityEngine.Gizmos.DrawSphere(point, radius);
        }

    }
}
