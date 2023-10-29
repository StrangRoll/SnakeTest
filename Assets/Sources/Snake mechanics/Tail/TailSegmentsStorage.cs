using System.Collections.Generic;
using UnityEngine;

namespace Sources.Snake_mechanics.Tail
{
    public class TailSegmentsStorage : MonoBehaviour
    {
        [SerializeField] private MainSegment _mainSegment;
        
        private LinkedList<TailSegment> _tailSegments = new LinkedList<TailSegment>();
        private TailSegment _lastSegment;
        
        private void Start()
        {
            _lastSegment = _mainSegment;
        }
        
        public void AddNewSegment(TailSegment newSegment)
        {
            var newTransform = newSegment.transform;
            
            newTransform.localPosition = _lastSegment.Position;
            newTransform.localRotation = _lastSegment.Rotation;
            
            _lastSegment.SetNextSegment(newSegment);
            _lastSegment = newSegment;
            _tailSegments.AddLast(newSegment);
        }
    }
}