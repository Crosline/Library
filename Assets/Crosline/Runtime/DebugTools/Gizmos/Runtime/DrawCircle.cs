using UnityEngine;

namespace Crosline.DebugTools {
    public static partial class CroslineGizmos { //TODO - Crosline: May be turned into CroslineDebug later.
        /// <param name="normalRadius">
        /// Specifies the normal with magnitude being the radius.
        /// </param>
        public static void DrawCircle(Vector3 origin, Vector3 normalRadius, int segments = 32) {
            (Vector3 left, Vector3 up) = GizmosUtils.GetComponentsFromNormal(normalRadius);

            var radius = normalRadius.magnitude;

            for (var i = 0; i < segments; i++) {
                var theta0 = 2f * Mathf.PI * (float) i / segments;
                var theta1 = 2f * Mathf.PI * (float) (i + 1) / segments;

                var x0 = radius * Mathf.Cos(theta0);
                var y0 = radius * Mathf.Sin(theta0);
                var x1 = radius * Mathf.Cos(theta1);
                var y1 = radius * Mathf.Sin(theta1);

                UnityEngine.Gizmos.DrawLine(origin + left * x0 + up * y0, origin + left * x1 + up * y1);
            }
        }

        public static void DrawCircle(Vector3 origin, Vector3 normal, float size, int segments = 32) {
            DrawCircle(origin, normal * size, segments);
        }

        public static void DrawWireCylinder(Vector3 origin, Vector3 normal, float radius, float height, int segments = 16) {
            Vector3 left = GizmosUtils.GetRightFromNormal(normal) * -1f;

            Vector3 top = origin + normal * height / 2f;
            Vector3 bot = origin - normal * height / 2f;

            DrawCircle(top, normal, radius, segments);
            DrawCircle(bot, normal, radius, segments);

            for (int i = 0; i < segments; i++) {
                float theta0 = 2f * Mathf.PI * (float) i / segments;
                float theta1 = 2f * Mathf.PI * (float) (i + 1) / segments;

                float x0 = radius * Mathf.Cos(theta0);
                float y0 = radius * Mathf.Sin(theta0);
                float x1 = radius * Mathf.Cos(theta1);
                float y1 = radius * Mathf.Sin(theta1);

                Vector3 fore = Vector3.Cross(left, normal);

                //UnityEngine.CroslineGizmos.DrawLine(origin + left * x0 + up * y0, origin + left * x1 + up * y1);

                UnityEngine.Gizmos.DrawLine(
                    bot + left * x0 + fore * y0,
                    bot + left * x1 + fore * y1);

                // Sides
                UnityEngine.Gizmos.DrawLine(
                    top + left * x0 + fore * y0,
                    bot + left * x0 + fore * y0);
            }
        }
    }
}
