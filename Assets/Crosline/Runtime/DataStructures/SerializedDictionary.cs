using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crosline.DataStructures {
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [Serializable]
        private struct KeyValuePair
        {
            public TKey Key;
            public TValue Value;
        }

        [SerializeField]
        private KeyValuePair[] Items;

        public void CopyTo(SerializedDictionary<TKey, TValue> other)
        {
            other.Clear();

            foreach (var (key, value) in this)
            {
                other.Add(key, value);
            }
        }

        public void OnBeforeSerialize()
        {
            Items = new KeyValuePair[Count];
            var index = 0;
            foreach (var (key, value) in this)
            {
                Items[index] = new KeyValuePair
                {
                    Key = key,
                    Value = value
                };
                index++;
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();

            if (Items != null && Items.Length > 0)
            {
                foreach (var keyValuePair in Items)
                {
                    Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
        }
    }

}