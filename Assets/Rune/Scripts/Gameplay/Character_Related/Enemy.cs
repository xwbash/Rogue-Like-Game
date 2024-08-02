using System;
using Rune.Scripts.Base;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using Rune.Scripts.Spawner;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Rune.Scripts.Gameplay.Character_Related
{
    public class Enemy : PlayerBase, IPoolableObject
    {
        
        [Inject] private EnemyService _enemyService;
        private bool _isObjectActive = true;
        private ISpawner _enemySpawner;
        private PoolingService _poolingService;
        private NavMeshAgent _navMeshAgent;
        private Transform _playerTransform;
        
        public override void OnDead()
        {
            RemoveObject();
        }

        public override void HitEnemy(int weaponDamage)
        {
            
        }

        public void Init(ISpawner spawner)
        {
            _enemySpawner = spawner;
        }

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = PlayerData.Speed;
            _playerTransform = _enemyService.GetPlayerTransform();
            OnStart();
        }

        public virtual void OnStart()
        {
            
        }

        private void Update()
        {
            if (Vector3.Distance(_playerTransform.position, transform.position) < PlayerData.Range) return;
            _navMeshAgent.SetDestination(_playerTransform.position);
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

        public void RemoveObject()
        {
            _enemySpawner.DeSpawn(this);
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