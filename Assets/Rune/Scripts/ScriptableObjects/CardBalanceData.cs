using System;
using System.Collections.Generic;
using Rune.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rune.Scripts.ScriptableObjects
{
    public enum AdjustmentType
    {
        Health,
        Speed,
        GunSpeed,
        BulletSpeed,
        Damage,
        Range,
        EnemySpeed,
        EnemyDamage,
        EnemyBulletSpeed,
        Experiment
    }
    
    [Serializable]
    public class CardAdjustmentData
    {
        public AdjustmentType AdjustmentType;
        
        public int Percentage;
        
        
        [Space(10)]
        [InfoBox("Selects between random X to Y")]
        [MinMaxSlider(0, 1000, true)] public Vector2 Value;
    }
    
    [CreateAssetMenu(fileName = "Card Balance Data", menuName = "Create Data/Card Balance Data", order = 0)]
    public class CardBalanceData : ScriptableObject
    {
        public List<CardAdjustmentData> CardAdjustmentData = new List<CardAdjustmentData>();
    }
}