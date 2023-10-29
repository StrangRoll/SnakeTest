using UnityEngine;

namespace Sources.Snake_mechanics.Move
{
    public class SnakeHeadRotator : MonoBehaviour
    {
        public void RotateInDirection(Vector3 movementDirection)
        {
            if (movementDirection == Vector3.zero)
                return;

            var targetRotation = Quaternion.LookRotation(movementDirection);
            transform.localRotation = targetRotation;
        }
    }
}