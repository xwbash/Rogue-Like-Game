using System;
using Rune.Scripts.Base;
using UnityEngine;

namespace Rune.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Create Data/Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public BaseStats baseStats;
    }

    

    [Serializable]
    public struct BaseStats
    {
        [field:SerializeField] public int Health { get; private set; }
        [field:SerializeField] public float Speed { get; private set; }
    }
    
    
}