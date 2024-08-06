using System;
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
        private float _gunCooldown = 1f;
        private float _bulletSpeed = 20f;
        private ProgressBarController _progressBarController;
        private int _currentHealth = 0;
        private PlayerService _playerService;
        private AbilityService _abilityService;

        [Inject]
        private void Construct(IObjectResolver objectResolver, ProgressbarService progressbarService, HitLabelService hitLabelService, PlayerService playerService, AbilityService abilityService)
        {
            _abilityService = abilityService;
            _playerService = playerService;
            _objectResolver = objectResolver;
            _progressBarController = progressbarService.GetProgressBar(m_progressBarParent);
            _hitLabelService = hitLabelService;
        }

        private void OnEnable()
        {
            _abilityService.OnAbilitySelected.AddListener(OnAbilityUpdate);   
        }

        private void OnDisable()
        {
            _abilityService.OnAbilitySelected.RemoveListener(OnAbilityUpdate);
        }

        private void Start()
        {
            var spawnedGunObject = _objectResolver.Instantiate(m_gunObject);
            spawnedGunObject.transform.SetParent(m_gunParentTransform, false);
            _spawnedWeaponBase = spawnedGunObject.GetComponent<WeaponBase>();

            WeaponData weaponData = new WeaponData();
            weaponData.BulletSpeed = _bulletSpeed;
            weaponData.Damage = PlayerData.Damage;
            weaponData.Range = PlayerData.Range;
            weaponData.Cooldown = _gunCooldown;
            
            _currentHealth = PlayerData.Health;

            _spawnedWeaponBase.Init(weaponData, this);
        }
        
        private void OnAbilityUpdate(CardData cardData)
        {
            if (cardData.Speed > 0)
            {
                PlayerData.Speed += (PlayerData.Speed * cardData.Speed);
            }

            if (cardData.Health > 0)
            {
                PlayerData.Health += cardData.Health;
            }
        }

        public override void OnDead()
        {
            _playerService.OnPlayerDead();
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