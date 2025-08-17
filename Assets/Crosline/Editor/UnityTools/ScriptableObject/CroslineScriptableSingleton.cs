using System.IO;
using UnityEditor;
using UnityEngine;

namespace Crosline.UnityTools.Editor {
    public abstract class CroslineScriptableSingleton<T> : ScriptableSingleton<T> where T : CroslineScriptableSingleton<T> {

        protected static T GetOrCreate() {
            var newObj = instance != null ? instance : CreateInstance<T>();
            var path = $"{newObj.AssetDirectory}/{typeof(T).Name}.asset";
            var obj = AssetDatabase.LoadAssetAtPath<T>(path);

            if (obj != null)
                return obj;

            obj = newObj;
            
            var dir = obj.AssetDirectory;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            obj.SetDefaults();

            if (!File.Exists(path)) {
                AssetDatabase.CreateAsset(obj, path);
            }

            return obj;
        }

        
        protected abstract void SetDefaults();
        protected abstract string AssetDirectory { get; }
    }
    // public abstract class ScriptableObjectPersist<T> : ScriptableObject where T : ScriptableObjectPersist<T> {
    //
    //     protected static T GetOrCreate() {
    //         var path = $"{typeof(T).Name}.asset";
    //         var obj = Resources.Load<T>(path);
    //
    //         if (obj != null)
    //             return obj;
    //         
    //         obj = CreateInstance<T>();
    //         
    //         var dir = obj.AssetDirectory;
    //
    //         if (!Directory.Exists(dir))
    //             Directory.CreateDirectory(dir);
    //         obj.SetDefaults();
    //
    //         if (!File.Exists(path)) {
    //             AssetDatabase.CreateAsset(obj, path);
    //         }
    //
    //         return obj;
    //     }
    //
    //     
    //     protected abstract void SetDefaults();
    //     protected abstract string AssetDirectory { get; }
    // }
}
