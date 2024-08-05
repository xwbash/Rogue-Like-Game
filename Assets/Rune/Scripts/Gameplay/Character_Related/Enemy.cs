using System;
using Rune.Scripts.Base;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using Rune.Scripts.Spawner;
using Rune.Scripts.UI;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Rune.Scripts.Gameplay.Character_Related
{
    public class Enemy : PlayerBase, IPoolableObject
    {
        protected bool IsOverrided = false;

        [SerializeField] private Transform m_progressBarParent;
       
        private float _shootingCooldown = 0.0f;
        private float _attackCooldown = 2.0f;
        private HitLabelService _hitLabelService;
        private bool _isObjectActive = true;
        private ISpawner _enemySpawner;
        private PoolingService _poolingService;
        private NavMeshAgent _navMeshAgent;
        private Transform _playerTransform;
        private EnemyService _enemyService;
        private ProgressBarController _progressBarController;
        private PlayerBase _playerBase;
        private int _currentHealth = 0;
        private ExperimentService _experimentService;

        [Inject]
        private void Construct(EnemyService enemyService, ProgressbarService progressbarService, HitLabelService hitLabelService, ExperimentService experimentService)
        {
            _experimentService = experimentService;
            _enemyService = enemyService;
            _progressBarController = progressbarService.GetProgressBar(m_progressBarParent);
            _hitLabelService = hitLabelService;
        }
        public override void OnDead()
        {
            RemoveObject();
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

        public void Init(ISpawner spawner)
        {
            _enemySpawner = spawner;
        }

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = PlayerData.Speed;
            _playerTransform = _enemyService.GetPlayerTransform();
            _playerBase = _enemyService.GetPlayerBase();
            _currentHealth = PlayerData.Health;
            OnStart();
        }

        public virtual void OnStart()
        {
            
        }

        private void Update()
        {
            if (!IsOverrided)
            {
                _shootingCooldown -= Time.deltaTime;
                if(_shootingCooldown > 0) return;
            
                if (_playerTransform)
                {
                    var closestEnemyDistance = Vector3.Distance(_playerTransform.position, transform.position);
            
                    if (closestEnemyDistance < PlayerData.Range)
                    {
                        _playerBase.HitEnemy(PlayerData.Damage);
                        _shootingCooldown = _attackCooldown;
                    
                    }    
                }
            }
            
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
            _currentHealth = PlayerData.Health;
            _progressBarController.SetProgressBar(0, PlayerData.Health, _currentHealth);
        }

        public void RemoveObject()
        {
            _experimentService.AddExperiment(PlayerData.ExperimentAmount);
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