using System;
using Code.Enum;
using UnityEngine;

namespace Code.Fruits
{
    [Serializable]
    public struct FruitSpawnData
    {
        [field: SerializeField] public Sprite _icon { get; private set; }
        [field: SerializeField] public Fruit _fruit { get; private set; }
        [field: SerializeField] public FruitType _type { get; private set; }
    }
}