using UnityEngine;

namespace Sources.InGameObjects.Apple
{
    public class Apple : MonoBehaviour
    {
        [SerializeField] private float _groundElevation;
        
        public void Init(Vector3 surfaceNormal)
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
            transform.position += _groundElevation * surfaceNormal;
        }
    }
}