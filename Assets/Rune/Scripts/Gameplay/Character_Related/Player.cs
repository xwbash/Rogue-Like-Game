using System;
using Rune.Scripts.Base;
using Rune.Scripts.ScriptableObjects;
using Rune.Scripts.Services;
using Rune.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Gameplay.Character_Related
{
    public class Player : EntityBase
    {
        [SerializeField] protected PlayerData m_playerData;
        [SerializeField] private WeaponData m_weaponData;
        [SerializeField] private Transform m_progressBarParent;
        [SerializeField] private Transform m_gunParentTransform;
        [SerializeField] private GameObject m_gunObject;

        private IObjectResolver _objectResolver;
        private WeaponBase _spawnedWeaponBase;
        private HitLabelService _hitLabelService;
        private ProgressBarController _progressBarController;
        private PlayerService _playerService;
        private AbilityService _abilityService;
        private float _currentSpeed;
        private int _currentHealth = 0;
        private int _maxHealth = 0;

        [Inject]
        private void Construct(IObjectResolver objectResolver, ProgressbarService progressbarService, HitLabelService hitLabelService, PlayerService playerService, AbilityService abilityService)
        {
            _abilityService = abilityService;
            _playerService = playerService;
            _objectResolver = objectResolver;
            _progressBarController = progressbarService.GetProgressBar(m_progressBarParent);
            _hitLabelService = hitLabelService;
            
            _currentHealth = m_playerData.baseStats.Health;
            _maxHealth = m_playerData.baseStats.Health;
            _currentSpeed = m_playerData.baseStats.Speed;
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
            _spawnedWeaponBase.Init(m_weaponData, this);
        }
        
        private void OnAbilityUpdate(CardData cardData)
        {
            if (cardData.Health > 0)
            {
                _maxHealth += cardData.Health;
            }

        }

        private void OnDead()
        {
            _playerService.OnPlayerDead();
        }

        public override void GetHit(int weaponDamage)
        {
            _currentHealth -= weaponDamage;
            _progressBarController.SetProgressBar(0, _maxHealth, _currentHealth);
            var labelObject = _hitLabelService.GetLabel();
            labelObject.SetSpawnPoint(transform.position);
            
            ((HitLabelController)labelObject).StartAnimation(weaponDamage);
            
            if (_currentHealth <= 0)
            {
                OnDead();
            }
        }

        public float GetRange()
        {
            return m_weaponData.Range;
        }

        public float GetSpeed()
        {
            return _currentSpeed;
        }
    }
}