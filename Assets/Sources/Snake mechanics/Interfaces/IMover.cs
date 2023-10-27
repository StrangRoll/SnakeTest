using UnityEngine;

namespace Sources.Snake_mechanics.Interfaces
{
    public interface IMover
    {
        public void DirectionChanged(Vector2 newDirection);
        public void Move();
    }
}