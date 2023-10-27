using UnityEngine;
using UnityEngine.Events;

namespace Sources.Snake_mechanics.Input
{
    public class PlayerInputRoot : MonoBehaviour
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
            _playerInput.Player.Move.canceled += ctx => OnMove();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void OnMove()
        {
            var newDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            DirectionChanged?.Invoke(newDirection);
        }
    }
}