using System;
using AYellowpaper;
using Sources.Snake_mechanics.Interfaces;
using UnityEngine;

namespace Sources.Snake_mechanics.Move
{
    public class MoverBySurface : MonoBehaviour, IMover
    {
        [SerializeField] private InterfaceReference<IInputRoot> _inputRoot;
        [SerializeField] private float _speed;

        private Vector2 _direction = Vector2.zero;

        private void OnEnable()
        {
            _inputRoot.Value.DirectionChanged += OnDirectionChanged;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDisable()
        {
            _inputRoot.Value.DirectionChanged -= OnDirectionChanged;
        }

        public void OnDirectionChanged(Vector2 newDirection)
        {
            _direction = newDirection;
        }

        public void Move()
        {
            transform.Translate(_direction * (Time.deltaTime * _speed));
        }
    }
}