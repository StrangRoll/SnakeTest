using System.Collections.Generic;
using Sources.Snake_mechanics.Tail;
using UnityEngine;

namespace Sources.Snake_mechanics
{
    public class TailSegmentsStorage : MonoBehaviour
    {
        [SerializeField] private Transform _snakeHead;
        
        private LinkedList<TailSegment> _tailSegments = new LinkedList<TailSegment>();
        private Transform _lastSegment;
        private Vector3? _lastSegmentPosition = null;
        private Quaternion? _lastSegmentRotation = null;
        
        private void Start()
        {
            _lastSegment = _snakeHead;
        }

        public void RememberLastSegmentPosition()
        {
            _lastSegmentPosition = _lastSegment.position;
            _lastSegmentRotation = _lastSegment.rotation;
        }
        
        public void AddNewSegment(TailSegment newSegment)
        {
            if (_lastSegmentPosition == null || _lastSegmentRotation == null)
            {
                Debug.LogError("No last segment position or rotation!");
                return;
            }

            var newTransform = newSegment.transform;
            
            newTransform.position = _lastSegmentPosition.Value;
            newTransform.rotation = _lastSegmentRotation.Value;
            _lastSegment = newTransform;
            _tailSegments.AddLast(newSegment);
            _lastSegmentPosition = null;
            _lastSegmentRotation = null;
        }
    }
}