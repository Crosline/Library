using System;
using UnityEngine;

namespace Crosline.Shaders.Liquid.Wobble {
    public class WobbleEffect : MonoBehaviour {
        [SerializeField]
        private Renderer _renderer;

        private Vector3 _velocity;
        private Vector3 _lastPos;
        private Vector3 _angularVelocity;
        private Vector3 _lastRot;


        [SerializeField]
        private float _smoothAmount = 1f;

        private Vector2 _wobbleAmountToAdd = Vector2.zero;
        private Vector2 _wobbleAxis = Vector2.zero;
        [SerializeField]
        private float _wobbleSpeed = 1f;
        [SerializeField]
        private float _maxWobble = 0.03f;
        private float _pulse;
        
        
        private void Awake() {
            if (_renderer == null) {
                _renderer = GetComponent<Renderer>();
            }
            
            _pulse = 2 * Mathf.PI * _wobbleSpeed;
        }

        private void Update() {
            _wobbleAmountToAdd = Vector2.Lerp(_wobbleAmountToAdd, Vector2.zero, Time.deltaTime * _smoothAmount);

            _wobbleAxis = _wobbleAmountToAdd * Mathf.Sin(_pulse * Time.time);

            if (_wobbleAxis.magnitude > 1f) {
                _wobbleAxis.Normalize();
            }
            
            _renderer.material.SetVector("_WobbleAxis", _wobbleAxis);

            var pos = transform.position;
            var rot = transform.rotation;
            
            _velocity = (_lastPos - pos) / Time.deltaTime;
            _angularVelocity = rot.eulerAngles - _lastRot;
            
            _wobbleAmountToAdd.x += Mathf.Clamp((_velocity.z + (_angularVelocity.z * 0.2f)) * _maxWobble, _maxWobble * -1f, _maxWobble);
            _wobbleAmountToAdd.y += Mathf.Clamp((_velocity.x + (_angularVelocity.x * 0.2f)) * _maxWobble, _maxWobble * -1f, _maxWobble);

            _lastPos = pos;
            _lastRot = rot.eulerAngles;
        }
    }
}