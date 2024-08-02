using Rune.Scripts.Base;
using Rune.Scripts.Data;
using Rune.Scripts.Services;
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
        private PlayerBase _closestEnemy = null;
        
        [Inject]
        private void Construct(CommonPlayerService commonPlayerService, BulletService bulletService)
        {
            _bulletService = bulletService;
            _commonPlayerService = commonPlayerService;
        }
        
        public override void Init(WeaponData weaponData)
        {
            _weaponData = weaponData;
        }

        private void Update()
        {
            _shootingCooldown -= Time.deltaTime;
            if(_shootingCooldown > 0) return;
            
            _closestEnemy= _commonPlayerService.GetClosestEnemy();

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
            projectileData.EndPoint = targetPosition;


            var projectile = (Projectile)_bulletService.GetBullet(ProjectileType.Bullet);
            projectile.Init(projectileData, OnBulletHit, _closestEnemy);
        }

        private void OnBulletHit(PlayerBase enemy)
        {
            enemy.HitEnemy(_weaponData.Damage);
        }

        public override void RemoveBullet(Projectile projectile)
        {
            _bulletService.RemoveObject(projectile, ProjectileType.Bullet);
        }
    }
}