using Rune.Scripts.Base;
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
            var spawnedGunObject = _objectResolver.Instantiate(m_gunObject);
            spawnedGunObject.transform.SetParent(m_gunParentTransform, false);
            _spawnedWeaponBase = spawnedGunObject.GetComponent<WeaponBase>();
            _spawnedWeaponBase.Init(m_weaponData, this);
        }
    }
}