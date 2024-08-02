using System;
using Rune.Scripts.Base;
using Rune.Scripts.Gameplay.Character_Related;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace Rune.Scripts.Gameplay.Guns_Related
{
    public class ProjectileData
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
        public float Speed;
    }

    public enum ProjectileType
    {
        Bullet,
        Orb
    }

    [Serializable]
    public class ProjectileCreationData
    {
        public ProjectileType ProjectileData;
        public GameObject ProjectileObject;
    }
    
    public class Projectile : MonoBehaviour, IPoolableObject
    {
        [Inject] private BulletService _bulletService;

        private Rigidbody _rigidbody;
        private PoolingService _poolingService;
        private bool _isObjectActive = true;
        private ProjectileData _projectileData;
        private UnityEvent<PlayerBase> _onBulletHit = new UnityEvent<PlayerBase>();
        private PlayerBase _enemyHit;
        public void Init(ProjectileData projectileData, UnityAction<PlayerBase> onBulletHit,
            PlayerBase closestEnemy)
        {
            _enemyHit = closestEnemy;
            _projectileData = projectileData;
            transform.position = projectileData.StartPoint;
            _onBulletHit.AddListener(onBulletHit);
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_projectileData == null)
            {
                return;
            }

            Vector3 direction = (_projectileData.EndPoint - _projectileData.StartPoint).normalized;
            _rigidbody.velocity = direction * _projectileData.Speed;
            
            if (Vector3.Distance(transform.position, _projectileData.EndPoint) < 1.0f)
            {
                RemoveObject();
                
                //OnBulletReached();   
            }
            // to-do bullet hit.
        }
        
        
        public void Init(ISpawner spawner)
        {
            
        }

        public bool IsObjectActive()
        {
            return _isObjectActive;
        }

        public void SetObjectActive(bool isActive)
        {
            gameObject.SetActive(!isActive);
            _isObjectActive = isActive;
        }

        public void OnObjectSpawned()
        {
            
        }

        public void OnBulletReached()
        {
            _onBulletHit.Invoke(_enemyHit);
            RemoveObject();
        }
        
        public void RemoveObject()
        {
            _poolingService.RemoveObject(this);
            _onBulletHit.RemoveAllListeners();
            _projectileData = null;
        }

        public void SetSpawnPoint(Vector3 spawnPoint)
        {
            transform.position = spawnPoint;
        }
        
        public PoolingService GetPoolingService()
        {
            return _poolingService;
        }

        public void SetPoolingService(PoolingService poolingService)
        {
            _poolingService = poolingService;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}