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
        [SerializeField] private int _startAppleCount;

        private void Start()
        {
            for (var i = 0; i < _startAppleCount; i++)
            {
                SpawnNewApple();
            }
        }

        private void SpawnNewApple()
        {
            Vector3 surfaceNormal;   
            var randomPosition = RandomPositionOnSurface(out surfaceNormal);
            var newApple = NightPool.Spawn(_applePrefab, randomPosition, Quaternion.identity);
            newApple.Init(surfaceNormal);
        }
        
        private Vector3 RandomPositionOnSurface(out Vector3 surfaceNormal)
        {
            var groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

            var randomDirection = Random.onUnitSphere;
            var rayCastStartPosition = _planetTransform.position + randomDirection * _maxRadius; 
            
            if (Physics.Raycast(rayCastStartPosition, randomDirection * (-1), out var raycastHit, _maxRadius, groundLayerMask))
            {
                surfaceNormal = raycastHit.normal;
                return raycastHit.point;
            }
            
            surfaceNormal = Vector3.zero;
            Debug.LogError("AppleFactory not find ground!");
            return _planetTransform.position;
        }

    }
}