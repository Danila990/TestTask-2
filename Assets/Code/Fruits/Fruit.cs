using Code.Enum;
using UnityEngine;

namespace Code.Fruits
{
    public class Fruit : MonoBehaviour
    {
        public FruitType _type { get; private set; }
        public Vector2Int _gridPosition { get; private set; }

        public void Init(FruitType fruitType, Vector2Int gridIndex)
        {
            _type = fruitType;
            _gridPosition = gridIndex;
        }
    }
}