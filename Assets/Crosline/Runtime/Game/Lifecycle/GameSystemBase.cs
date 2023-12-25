using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Crosline.Game.Lifecycle {
    [Serializable]
    public abstract class GameSystemBase {

        protected GameSystemBase() { }

        [field: SerializeField]
        public bool IsDevelopmentOnly { get; private set; }
        
        public abstract Task Initialize();
        public abstract void Dispose();
    }
}