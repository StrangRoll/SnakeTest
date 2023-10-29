using Sources.Snake_mechanics.Tail;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Snake_mechanics
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class SnakeColliderDetector : MonoBehaviour
    {
        [SerializeField] private TailSegmentsStorage tailSegmentsStorage;
        
        public event UnityAction<Collider, TailSegmentsStorage> ColliderDetected;

        private void OnTriggerEnter(Collider other)
        {
            ColliderDetected?.Invoke(other, tailSegmentsStorage);
        }
    }
}