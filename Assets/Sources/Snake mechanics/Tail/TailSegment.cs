using UnityEngine;

namespace Sources.Snake_mechanics.Tail
{
    public class TailSegment : MonoBehaviour
    {
        private TailSegment _nextSegment = null;
        
        public Vector3 Position => transform.localPosition;
        public Quaternion Rotation => transform.localRotation;

        
        public void SetNextSegment(TailSegment nextSegment)
        {
            if (_nextSegment != null)
            {
                Debug.LogError("Tail segment already has next segment!");
                return;
            }
            
            _nextSegment = nextSegment;
        }
        
        public void Move(Vector3 newPosition, Quaternion newRotation)
        {
            MoveNextSegment();
            
            transform.position = newPosition;
            transform.rotation = newRotation;

        }

        protected void MoveNextSegment()
        {
            if (_nextSegment != null)
            {
                _nextSegment.Move(transform.position, transform.rotation);
            }
        }
    }
}