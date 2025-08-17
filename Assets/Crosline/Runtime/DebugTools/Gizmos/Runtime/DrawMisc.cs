using UnityEngine;

namespace Crosline.DebugTools {
    public static partial class CroslineGizmos {
        public static void DrawCross(Vector3 position, float size) {
            var d = size / 2.0f;
            var p = position;
            Gizmos.DrawLine(p + Vector3.left * d, p + Vector3.right * d);
            Gizmos.DrawLine(p + Vector3.up * d, p + Vector3.down * d);
            Gizmos.DrawLine(p + Vector3.back * d, p + Vector3.forward * d);
        }

        public static void DrawArrow(Vector3 position, Vector3 direction, float size = 0.1f) {
            var k = 1.175f * 0.33f;
            var right = GizmosUtils.GetRightFromNormal(direction);

            direction = direction.normalized * size * k;
            right = right.normalized * size * k;

            Gizmos.DrawLine(position - direction - right, position + direction * 2);
            Gizmos.DrawLine(position + direction * 2, position - direction + right);
            Gizmos.DrawLine(position - direction + right, position - direction * 0.33f);
            Gizmos.DrawLine(position - direction * 0.33f, position - direction - right);
        }

        public static void DrawTriangle(Vector3 position, Vector3 direction, float size = 0.1f) {
            var k = 1.175f * 0.33f;
            var right = GizmosUtils.GetRightFromNormal(direction);

            direction = direction.normalized * size * k;
            right = right.normalized * size * k;

            DrawTriangle(
                position - direction - right,
                position + direction * 2,
                position - direction + right
            );
        }

        public static void DrawTriangle(Vector3 position1, Vector3 position2, Vector3 position3) {
            Gizmos.DrawLine(position1, position2);
            Gizmos.DrawLine(position2, position3);
            Gizmos.DrawLine(position3, position1);
        }

        public static void DrawCone(Vector3 position, Vector3 direction, float size = 0.1f) {
            var mesh = CreateConeMesh(size);
            Gizmos.DrawMesh(mesh, position, Quaternion.LookRotation(direction));
        }

        private static Mesh CreateConeMesh(float size = 0.1f) {
            var height = size * 2f;
            const int segments = 8;
            var mesh = new Mesh();

            var vertexCount = segments + 2; // Base vertices + tip + center
            var vertices = new Vector3[vertexCount];
            var triangles = new int[segments * 3 * 2]; // Base + side faces

            // Define the base center vertex
            vertices[0] = Vector3.zero;

            // Define the top vertex
            vertices[1] = new Vector3(0, 0, height);

            var angleStep = 2 * Mathf.PI / segments;

            // Generate base vertices
            for (var i = 0; i < segments; i++) {
                var angle = i * angleStep;
                var x = Mathf.Cos(angle) * size;
                var y = Mathf.Sin(angle) * size;

                vertices[i + 2] = new Vector3(x, y, 0);
            }

            // Create triangles for the base
            for (var i = 0; i < segments; i++) {
                var nextIndex = (i + 1) % segments;
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = nextIndex + 2;
                triangles[i * 3 + 2] = i + 2;
            }

            // Create triangles for the side faces
            var sideOffset = segments * 3;

            for (var i = 0; i < segments; i++) {
                var nextIndex = (i + 1) % segments;
                triangles[sideOffset + i * 3] = 1;
                triangles[sideOffset + i * 3 + 1] = i + 2;
                triangles[sideOffset + i * 3 + 2] = nextIndex + 2;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}
