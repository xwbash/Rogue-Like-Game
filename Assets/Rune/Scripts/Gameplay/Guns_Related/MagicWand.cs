using Rune.Scripts.Base;
using Rune.Scripts.Data;
using Rune.Scripts.Gameplay.Character_Related;
using Rune.Scripts.Services;
using UnityEngine;
using VContainer;

namespace Rune.Scripts.Gameplay.Guns_Related
{
    public class MagicWand : WeaponBase
    {
        [SerializeField] private Transform m_bulletStartPoint;

        private float _shootingCooldown;
        private PlayerBase _closestEnemy = null;
        private CommonPlayerService _commonPlayerService;
        private BulletService _bulletService;
        private PlayerBase _currentPlayerBase;

        [Inject]
        private void Construct(CommonPlayerService commonPlayerService, BulletService bulletService)
        {
            _bulletService = bulletService;
            _commonPlayerService = commonPlayerService;
        }
        public override void Init(WeaponData weaponData, PlayerBase player)
        {
            _weaponData = weaponData;
            _currentPlayerBase = player;
        }

        private void Update()
        {
            _shootingCooldown -= Time.deltaTime;
            if(_shootingCooldown > 0) return;
            
           _closestEnemy = _commonPlayerService.GetPlayer();

            if (_closestEnemy)
            {
                var closestEnemyDistance = Vector3.Distance(_closestEnemy.transform.position, transform.position);
                Debug.Log("distance between them " + closestEnemyDistance);
            
                if (closestEnemyDistance < _weaponData.Range)
                {
                    Shoot(m_bulletStartPoint.position, _closestEnemy.transform.position);
                    _shootingCooldown = _weaponData.Cooldown;
                }    
            }
        }

        public override void Shoot(Vector3 startPosition, Vector3 targetPosition)
        {
            ProjectileData projectileData = new ProjectileData();
            projectileData.Speed = _weaponData.BulletSpeed;
            projectileData.StartPoint = startPosition;
            projectileData.EndPoint = new Vector3(targetPosition.x, 1, targetPosition.z);


            var projectile = (Projectile)_bulletService.GetBullet(ProjectileType.Orb);
            projectile.Init(projectileData, _weaponData.Damage, _currentPlayerBase);
        }

        private void OnBulletHit(PlayerBase enemy)
        {
            enemy.HitEnemy(_weaponData.Damage);
        }

        public override void RemoveBullet(Projectile projectile)
        {
            _bulletService.RemoveObject(projectile, ProjectileType.Orb);
        }

    }
}