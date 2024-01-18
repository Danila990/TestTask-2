using System;
using Code.Enum;
using UnityEngine;

namespace Code.Fruits
{
    [Serializable]
    public struct FruitCell
    {
        [field: SerializeField] public Vector2Int _gridPosition { get; private set; }
        [field: SerializeField] public FruitType _type { get; private set; }
    }
}