using Rune.Scripts.Base;
using Rune.Scripts.ScriptableObjects;
using Rune.Scripts.Services;
using Rune.Scripts.UI;
using UnityEngine;
using VContainer;

namespace Rune.Scripts.Gameplay.Guns_Related
{
    public class Pistol : WeaponBase
    {
        [SerializeField] private Transform m_bulletStartPoint;
        private CommonPlayerService _commonPlayerService;
        private float _shootingCooldown;
        private BulletService _bulletService;
        private EntityBase _closestEnemy = null;
        private EntityBase _currentEntityBase;
        private GameCycleService _gameCycleService;
        private bool _isGamePaused = false;
        private AbilityService _abilityService;

        [Inject]
        private void Construct(CommonPlayerService commonPlayerService, BulletService bulletService, GameCycleService gameCycleService, AbilityService abilityService)
        {
            _abilityService = abilityService;
            _gameCycleService = gameCycleService;
            _bulletService = bulletService;
            _commonPlayerService = commonPlayerService;
        }
        
        public override void Init(WeaponData weaponData, EntityBase entity)
        {
            base.weaponData = weaponData;
            _currentEntityBase = entity;
        }
        
        private void OnEnable()
        {
            _abilityService.OnAbilitySelected.AddListener(OnAbilitySelected);
            _gameCycleService.OnGamePaused.AddListener(OnGamePaused);
            _gameCycleService.OnGameContinued.AddListener(OnGameContinued);
        }
        
        private void OnDisable()
        {
            _abilityService.OnAbilitySelected.RemoveListener(OnAbilitySelected);
            _gameCycleService.OnGamePaused.RemoveListener(OnGamePaused);
            _gameCycleService.OnGameContinued.RemoveListener(OnGameContinued);
        }

        private void OnAbilitySelected(CardData cardData)
        {
            if (cardData.BulletSpeed > 0)
            {
                weaponData.BulletSpeed += cardData.BulletSpeed;
            }

            if (cardData.GunSpeed > 0)
            {
                weaponData.Cooldown += cardData.GunSpeed;
            }

            if (cardData.Damage > 0)
            {
                weaponData.Damage += cardData.Damage;
            }
            
            if (cardData.Range > 0)
            {
                weaponData.Range += cardData.Range;
            }
        }

        private void OnGameContinued()
        {
            _isGamePaused = false;
        }

        private void OnGamePaused()
        {
            _isGamePaused = true;
        }
        
        private void Update()
        {
            if(_isGamePaused) return;

            
            _shootingCooldown -= Time.deltaTime;
            if(_shootingCooldown > 0) return;
            
            _closestEnemy= _commonPlayerService.GetClosestEnemy();

            if (_closestEnemy)
            {
                var closestEnemyDistance = Vector3.Distance(_closestEnemy.transform.position, transform.position);
                Debug.Log("distance between them " + closestEnemyDistance);
            
                if (closestEnemyDistance < weaponData.Range)
                {
                    Shoot(m_bulletStartPoint.position, _closestEnemy.transform.position);
                    _shootingCooldown = weaponData.Cooldown;
                }    
            }
        }

        public void Shoot(Vector3 startPosition, Vector3 targetPosition)
        {
            ProjectileData projectileData = new ProjectileData();
            projectileData.Speed = weaponData.BulletSpeed;
            projectileData.StartPoint = startPosition;
            projectileData.EndPoint = new Vector3(targetPosition.x, 1, targetPosition.z);


            var projectile = (Projectile)_bulletService.GetBullet(ProjectileType.Bullet);
            projectile.Init(projectileData, weaponData.Damage, _currentEntityBase);
        }
    }
}