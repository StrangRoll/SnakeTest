using NTC.Pool;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.InGameObjects.Apple
{
    [RequireComponent(typeof(Collider))]
    public class Apple : MonoBehaviour
    {
        [SerializeField] private float _groundElevation;

        public event UnityAction<Apple> AppleDespawned;

        private void OnTriggerEnter(Collider other)
        {
            NightPool.Despawn(gameObject);
            AppleDespawned?.Invoke(this);
        }

        public void Init(Vector3 surfaceNormal)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
            transform.position += _groundElevation * surfaceNormal;
        }
    }
}