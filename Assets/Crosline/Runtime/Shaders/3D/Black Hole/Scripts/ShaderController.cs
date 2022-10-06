using System.Collections;
using UnityEngine;

namespace Crosline.Shaders
{
    public class ShaderController : MonoBehaviour
    {
        [SerializeField]
        private Material[] _materials;
        
        private IEnumerator ActivateProperty(string propertyKey, float propertyValue, float activateTime = 1f) {
            for (float j = 0; j <= activateTime; j += Time.deltaTime) {
                float newValue = Mathf.Lerp(0, propertyValue, j / activateTime);

                if (Mathf.Abs(newValue-propertyValue) < 0.1) 
                    newValue = propertyValue;
                
                foreach (var material in _materials)
                    material.SetProperty(propertyKey, newValue);
                
                yield return null;
            }
        }
        
        private IEnumerator ActivateProperty(string propertyKey, Color propertyValue, float activateTime = 1f) {
            for (float j = 0; j <= activateTime; j += Time.deltaTime) {
                Color newValue = Color.Lerp(Color.clear, propertyValue, j / activateTime);
                foreach (var material in _materials)
                    material.SetProperty(propertyKey, newValue);
                
                yield return null;
            }
        }
        
        private IEnumerator ActivateProperty(string propertyKey, int propertyValue, float activateTime = 1f) {
            for (float j = 0; j <= activateTime; j += Time.deltaTime) {
                int newValue = (int) Mathf.Lerp(0, propertyValue, j / activateTime);
                foreach (var material in _materials) 
                    material.SetProperty(propertyKey, newValue);
                
                yield return null;
            }
        }
        
        private IEnumerator ActivateProperty(string propertyKey, Vector4 propertyValue, float activateTime = 1f) {
            for (float j = 0; j <= activateTime; j += Time.deltaTime) {
                Vector4 newValue = Vector4.Lerp(Vector4.zero, propertyValue, j / activateTime);
                foreach (var material in _materials) 
                    material.SetProperty(propertyKey, newValue);
                
                yield return null;
            }
        }
    }

    public static class MaterialExtensions {
        public static void SetProperty(this Material mat, string key, float value) => mat.SetFloat(key, value);
        public static void SetProperty(this Material mat, string key, Color value) => mat.SetColor(key, value);
        public static void SetProperty(this Material mat, string key, int value) => mat.SetInt(key, value);
        public static void SetProperty(this Material mat, string key, ComputeBuffer value) => mat.SetBuffer(key, value);
        public static void SetProperty(this Material mat, string key, Texture value) => mat.SetTexture(key, value);
        public static void SetProperty(this Material mat, string key, Vector4 value) => mat.SetVector(key, value);
    }
}
