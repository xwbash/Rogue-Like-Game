using Rune.Scripts.Base;
using Rune.Scripts.Data;
using Rune.Scripts.Services;
using Rune.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Gameplay.Character_Related
{
    public class RangedEnemy : Enemy
    {
        [SerializeField] private Transform m_gunParentTransform;
        [SerializeField] private GameObject m_gunObject;

        private IObjectResolver _objectResolver;
        private WeaponBase _spawnedWeaponBase;
        
        [Inject]
        private void Construct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public override void OnStart()
        {
            IsOverrided = true;
            var spawnedGunObject = _objectResolver.Instantiate(m_gunObject);
            spawnedGunObject.transform.SetParent(m_gunParentTransform, false);
            _spawnedWeaponBase = spawnedGunObject.GetComponent<WeaponBase>();

            WeaponData weaponData = new WeaponData();
            weaponData.BulletSpeed = 10f;
            weaponData.Damage = PlayerData.Damage;
            weaponData.Range = PlayerData.Range;
            weaponData.Cooldown = 1f;
            
            _spawnedWeaponBase.Init(weaponData, this);
        }
    }
}