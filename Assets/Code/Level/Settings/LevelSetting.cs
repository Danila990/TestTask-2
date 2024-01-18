using System;
using Code.Enum;
using Code.Fruits;
using UnityEngine;

namespace Code.Level.Settings
{
    [Serializable]
    public struct LevelSetting
    {
        [field: SerializeField] public int _levelNumber { get; private set; }
        [field: SerializeField] public int _countFruits { get; private set; }
        [field: SerializeField] public float _timer { get; private set; }
        [field: SerializeField] public int _completeCoins { get; private set; }
        [field: SerializeField] public Vector2Int _gridSize { get; private set; }
        [field: SerializeField] public Vector2Int _startPlayerPosition { get; private set; }
        [field: SerializeField] public DirectionMove _startPlayerDirection { get; private set; }
        
        public FruitCell[] FruitCell;
    }
}