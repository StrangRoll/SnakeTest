using UnityEngine;
using UnityEngine.Events;

namespace Sources.Snake_mechanics.Interfaces
{
    public interface IInputRoot
    {
        public event UnityAction<Vector2> DirectionChanged; 
    }
}