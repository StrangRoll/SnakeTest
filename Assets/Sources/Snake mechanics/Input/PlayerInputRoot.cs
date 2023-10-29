using Sources.Snake_mechanics.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Snake_mechanics.Input
{
    public class PlayerInputRoot : MonoBehaviour, IInputRoot
    {
        private PlayerInput _playerInput;

        public event UnityAction<Vector2> DirectionChanged;
        
        private void OnEnable()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        private void Start()
        {
            _playerInput.Player.Move.performed += ctx => OnMove();
            DirectionChanged?.Invoke(Vector2.right);
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void OnMove()
        {
            var newDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            DirectionChanged?.Invoke(newDirection.normalized);
        }
    }
}
