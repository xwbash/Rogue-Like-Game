using Rune.Scripts.Base;
using Rune.Scripts.Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Gameplay.Character_Related
{
    public class Player : PlayerBase
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

        private void Start()
        {
            var spawnedGunObject = _objectResolver.Instantiate(m_gunObject);
            spawnedGunObject.transform.SetParent(m_gunParentTransform, false);
            _spawnedWeaponBase = spawnedGunObject.GetComponent<WeaponBase>();

            WeaponData weaponData = new WeaponData();
            weaponData.BulletSpeed = 20f;
            weaponData.Damage = PlayerData.Damage;
            weaponData.Range = PlayerData.Range;
            weaponData.Cooldown = 1f;
            
            _spawnedWeaponBase.Init(weaponData);
        }

        public override void OnDead()
        {
            
        }

        public override void HitEnemy(int weaponDamage)
        {
            
        }
    }
}