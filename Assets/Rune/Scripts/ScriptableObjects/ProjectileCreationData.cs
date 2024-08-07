using Rune.Scripts.Gameplay.Guns_Related;
using UnityEngine;

namespace Rune.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Projectile Creation Data", menuName = "Create Data/Projectile Creation Data", order = 0)]
    public class ProjectileCreationData : ScriptableObject
    {
        public ProjectileType ProjectileData;
        public GameObject ProjectileObject;
    }
}