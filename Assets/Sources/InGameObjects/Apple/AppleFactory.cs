using NTC.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.InGameObjects.Apple
{
    public class AppleFactory : MonoBehaviour
    {
        private const string GroundLayer = "Ground"; 
        
        [SerializeField] private Apple _applePrefab;
        [SerializeField] private Transform _planetTransform;
        [SerializeField] private float _maxRadius;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnNewApple), 0.0f, 2);
        }

        private void SpawnNewApple()
        {
            var randomPosition = RandomPositionOnSurface();
            var newApple = NightPool.Spawn(_applePrefab, randomPosition, Quaternion.identity);
            
        }
        
        private Vector3 RandomPositionOnSurface()
        {
            var groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

            var randomDirection = Random.onUnitSphere;
            var rayCastStartPosition = _planetTransform.position + randomDirection * _maxRadius; 
            
            if (Physics.Raycast(rayCastStartPosition, randomDirection * (-1), out var raycastHit, _maxRadius, groundLayerMask))
            {
                return raycastHit.point;
            }
            
            Debug.LogError("AppleFactory not find ground!");
            return _planetTransform.position;
        }

    }
}