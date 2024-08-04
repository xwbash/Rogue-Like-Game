using Rune.Scripts.Base;
using Rune.Scripts.Data;
using Rune.Scripts.Services;
using Rune.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Gameplay.Character_Related
{
    public class Player : PlayerBase
    {
        [SerializeField] private Transform m_progressBarParent;
        [SerializeField] private Transform m_gunParentTransform;
        [SerializeField] private GameObject m_gunObject;

        private IObjectResolver _objectResolver;
        private WeaponBase _spawnedWeaponBase;
        private HitLabelService _hitLabelService;
        private ProgressBarController _progressBarController;
        private int _currentHealth = 0;
        
        [Inject]
        private void Construct(IObjectResolver objectResolver, ProgressbarService progressbarService, HitLabelService hitLabelService)
        {
            _objectResolver = objectResolver;
            _progressBarController = progressbarService.GetProgressBar(m_progressBarParent);
            _hitLabelService = hitLabelService;
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
            
            _currentHealth = PlayerData.Health;

            _spawnedWeaponBase.Init(weaponData, this);
        }

        public override void OnDead()
        {
            
        }

        public override void HitEnemy(int weaponDamage)
        {
            _currentHealth -= weaponDamage;
            _progressBarController.SetProgressBar(0, PlayerData.Health, _currentHealth);
            var labelObject = _hitLabelService.GetLabel();
            labelObject.SetSpawnPoint(transform.position);
            ((HitLabelController)labelObject).StartAnimation(weaponDamage);
            
            if (_currentHealth <= 0)
            {
                OnDead();
            }
        }
    }
}