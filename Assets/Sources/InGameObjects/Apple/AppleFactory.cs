using System;
using System.Collections.Generic;
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
        
        private List<Apple> _allApples = new List<Apple>();

        private void Start()
        {
            for (var i = 0; i < _startAppleCount; i++)
            {
                var newApple = SpawnNewApple();
                _allApples.Add(newApple);
                newApple.AppleDespawned += OnAppleDespawned;
            }
        }

        private void OnDestroy()
        {
            foreach (var apple in _allApples)
                apple.AppleDespawned -= OnAppleDespawned;
        }

        private void OnAppleDespawned(Apple apple)
        {
            SpawnNewApple();
        }

        private Apple SpawnNewApple()
        {
            Vector3 surfaceNormal;   
            var randomPosition = RandomPositionOnSurface(out surfaceNormal);
            var newApple = NightPool.Spawn(_applePrefab, randomPosition, Quaternion.identity);
            newApple.Init(surfaceNormal);
            return newApple;
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