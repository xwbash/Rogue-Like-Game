using UnityEngine;

namespace Rune.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Create Data/Enemy Data", order = 0)]
    public class EnemyData : ScriptableObject
    {
        public BaseStats baseStats;
        [field:SerializeField] public int ExperiencePoint { get; private set; }
    }
}