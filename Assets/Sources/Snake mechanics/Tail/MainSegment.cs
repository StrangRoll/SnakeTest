using UnityEngine;

namespace Sources.Snake_mechanics.Tail
{
    public class MainSegment : TailSegment
    {
        private Vector3 _oldPosition;
        private Quaternion _oldRotation;
        
        public void FixedUpdate()
        {
            MoveNextSegment();
        }
    }
}