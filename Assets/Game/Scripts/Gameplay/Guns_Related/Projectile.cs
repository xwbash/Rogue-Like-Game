using System;
using Rune.Scripts.Base;
using Rune.Scripts.Gameplay.Character_Related;
using Rune.Scripts.Interfaces;
using Rune.Scripts.ScriptableObjects;
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
   
    
    public class Projectile : MonoBehaviour, IPoolableObject
    {
        private Rigidbody _rigidbody;
        private PoolingService _poolingService;
        private bool _isObjectActive = true;
        private ProjectileData _projectileData;
        private float _timer = 3.0f;
        private int _weaponDamage;
        private EntityBase _currentEntityBase;
        private BulletService _bulletService;
        private GameCycleService _gameCycleService;
        private bool _isGamePaused = false;
        
        [Inject]
        private void Construct(GameCycleService gameCycleService, BulletService bulletService)
        {
            _bulletService = bulletService;
            _gameCycleService = gameCycleService;
        }
        
        public void Init(ProjectileData projectileData, int weaponDamage, EntityBase currentEntityBase)
        {
            _currentEntityBase = currentEntityBase;
            _projectileData = projectileData;
            transform.position = projectileData.StartPoint;
            _weaponDamage = weaponDamage;
        }
        
        private void OnEnable()
        {
            _isGamePaused = _gameCycleService.IsGamePaused(); // protection for pooling porpuses
            _gameCycleService.OnGamePaused.AddListener(OnGamePaused);
            _gameCycleService.OnGameContinued.AddListener(OnGameContinued);
        }

        private void OnDisable()
        {
            _gameCycleService.OnGamePaused.RemoveListener(OnGamePaused);
            _gameCycleService.OnGameContinued.RemoveListener(OnGameContinued);
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void OnGamePaused()
        {
            _isGamePaused = true;
        }

        private void OnGameContinued()
        {
            _isGamePaused = false;
        }

        private void FixedUpdate()
        {
            if (_isGamePaused)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }
            
            if (_projectileData == null)
            {
                return;
            }

            Vector3 direction = (_projectileData.EndPoint - _projectileData.StartPoint).normalized;
            _rigidbody.velocity = direction * _projectileData.Speed;
            _timer -= Time.deltaTime;
            
            if (_timer <= 0)
            {
                RemoveObject(); 
            }
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
            RemoveObject();
        }
        
        public void RemoveObject()
        {
            _poolingService.RemoveObject(this);
            _projectileData = null;
            _currentEntityBase = null;
            _timer = 3.0f;
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

        private void OnTriggerEnter(Collider other)
        {
            var playerBase = other.GetComponent<EntityBase>();
            if (!playerBase)
            {
                return;
            }
            
            
            if (playerBase != _currentEntityBase && playerBase.GetPlayerType() != _currentEntityBase.GetPlayerType())
            {
                playerBase.GetHit(_weaponDamage);
                RemoveObject();
            }
        }
    }
}