using UnityEngine;

namespace Sources.Snake_mechanics.Interfaces
{
    public interface IMover
    {
        public void OnDirectionChanged(Vector2 newDirection);
        public void Move();
    }
}