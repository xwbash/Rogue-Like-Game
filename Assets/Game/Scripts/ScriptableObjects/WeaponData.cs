using UnityEngine;

namespace Rune.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Create Data/Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public int Damage;
        public float Cooldown;
        public float BulletSpeed;
        public float Range;
    }
}