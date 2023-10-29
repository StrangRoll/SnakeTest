using System.Collections;
using NTC.Pool;
using Sources.InGameObjects;
using UnityEngine;

namespace Sources.Snake_mechanics.Tail
{
    public class TailSegmentFactory : MonoBehaviour
    {
        [SerializeField] private SnakeColliderDetector[] _snakeColliderDetectors;
        [SerializeField] private float _creationDelay;
        [SerializeField] private TailSegment _tailSegmentPrefab;

        private WaitForSeconds _waitBeforeCreateNewTailSegment;

        private void Awake()
        {
            _waitBeforeCreateNewTailSegment = new WaitForSeconds(_creationDelay);
        }

        private void OnEnable()
        {
            foreach (var colliderDetector in _snakeColliderDetectors)
            {
                colliderDetector.ColliderDetected += OnColliderEnter;
            }
        }

        private void OnDisable()
        {
            foreach (var colliderDetector in _snakeColliderDetectors)
            {
                colliderDetector.ColliderDetected -= OnColliderEnter;
            }
        }

        private void OnColliderEnter(Collider other, TailSegmentsStorage storage)
        {
            if (other.TryGetComponent(out Apple apple))
                StartCoroutine(CreateNewSegment(storage));
        }

        private IEnumerator CreateNewSegment(TailSegmentsStorage storage)
        {
            yield return _waitBeforeCreateNewTailSegment;

            var newSegment = NightPool.Spawn(_tailSegmentPrefab, transform);
            storage.AddNewSegment(newSegment);
        }
    }
}