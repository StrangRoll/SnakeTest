using AYellowpaper;
using Sources.Snake_mechanics.Interfaces;
using UnityEngine;

namespace Sources.Snake_mechanics.Move
{
    public class MoverBySurface : MonoBehaviour, IMover
    {
        private const string GroundLayer = "Ground";
        
        [SerializeField] private SnakeHeadRotator _snakeHeadRotator;
        [SerializeField] private InterfaceReference<IInputRoot> _inputRoot;
        [SerializeField] private float _speed;
        [SerializeField] private float _desiredHeight;
        [SerializeField] private float _rayLength;

        private Vector2 _direction = Vector2.zero;

        private void OnEnable()
        {
            _inputRoot.Value.DirectionChanged += OnDirectionChanged;
        }

        private void FixedUpdate()
        {
            if (CheckRaycastForTerrain(out var hit) == false)
            {
                Debug.LogError("No ground detected");
                return;
            }

            RotateByNormal(hit);
            MoveToDesiredHeight(hit);
            
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
            var movement = new Vector3(_direction.x, 0.0f, _direction.y) * (_speed * Time.deltaTime);
            transform.Translate(movement);
            _snakeHeadRotator.RotateInDirection(movement);
        }

        private bool CheckRaycastForTerrain(out RaycastHit hit)
        {
            var groundLayerMask = 1 << LayerMask.NameToLayer(GroundLayer);

            var raycastDirection = transform.up * (-1) ; // The snake's forward direction

            if (Physics.Raycast(transform.position, raycastDirection, out var raycastHit, _rayLength, groundLayerMask))
            {
                hit = raycastHit;
                return true;
            }

            hit = default;
            return false;
        }

        private void MoveToDesiredHeight(RaycastHit hit)
        {
            var terrainNormal = hit.normal;
            var newPosition = hit.point + _desiredHeight * terrainNormal;
            transform.position = newPosition;
        }

        private void RotateByNormal(RaycastHit hit)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }
}