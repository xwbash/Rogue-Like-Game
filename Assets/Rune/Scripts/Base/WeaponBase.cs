using Rune.Scripts.Data;
using Rune.Scripts.Gameplay.Character_Related;
using Rune.Scripts.Gameplay.Guns_Related;
using UnityEngine;

namespace Rune.Scripts.Base
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected WeaponData _weaponData;

        public abstract void Init(WeaponData weaponData, PlayerBase player);

        public abstract void Shoot(Vector3 startPosition, Vector3 targetPosition);

        public abstract void RemoveBullet(Projectile projectile);
    }
}