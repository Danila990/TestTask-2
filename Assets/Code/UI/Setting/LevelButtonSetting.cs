using System;
using UnityEngine;

namespace Code.UI.Setting
{
    [Serializable]
    public struct LevelButtonSetting
    {
        [HideInInspector] public bool IsOpened;
        [field: SerializeField] public int _openingPrice { get; private set; }
    }
}