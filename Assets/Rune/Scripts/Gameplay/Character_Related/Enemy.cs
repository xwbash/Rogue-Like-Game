using DG.Tweening;
using Rune.Scripts.Base;
using Rune.Scripts.Interfaces;
using Rune.Scripts.ScriptableObjects;
using Rune.Scripts.Services;
using Rune.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Rune.Scripts.Gameplay.Character_Related
{
    public class Enemy : EntityBase, IPoolableObject
    {
        [SerializeField] protected WeaponData m_weaponData;
        [SerializeField] private Transform m_progressBarParent;
        [SerializeField] protected EnemyData m_enemyData;

        protected Transform PlayerTransform;
        protected EntityBase EntityBase;
        protected float ShootingCooldown;
        protected float AttackCooldown;

        private bool _isGamePaused = false;
        private bool _isObjectActive = true;
        private int _currentHealth = 0;
        private int _maxHealth;
        private int _currentExperimentAmount = 0;
        private float _currentSpeed = 0;
        private HitLabelService _hitLabelService;
        private ISpawner _enemySpawner;
        private PoolingService _poolingService;
        private NavMeshAgent _navMeshAgent;
        private EnemyService _enemyService;
        private ProgressBarController _progressBarController;
        private ExperimentService _experimentService;
        private GameCycleService _gameCycleService;
        private AbilityService _abilityService;


        [Inject]
        private void Construct(EnemyService enemyService, ProgressbarService progressbarService, HitLabelService hitLabelService, ExperimentService experimentService, GameCycleService gameCycleService, AbilityService abilityService)
        {
            _abilityService = abilityService;
            _gameCycleService = gameCycleService;
            _experimentService = experimentService;
            _enemyService = enemyService;
            _progressBarController = progressbarService.GetProgressBar(m_progressBarParent);
            _hitLabelService = hitLabelService;

            if (m_weaponData)
            {
                _currentHealth = m_enemyData.baseStats.Health;
                _maxHealth = m_enemyData.baseStats.Health;
                _currentSpeed = m_enemyData.baseStats.Speed;
                _currentExperimentAmount = m_enemyData.ExperiencePoint;
                ShootingCooldown = m_weaponData.Cooldown;
                AttackCooldown = m_weaponData.Cooldown;    
            }
        }

        private void OnEnable()
        {
            _isGamePaused = _gameCycleService.IsGamePaused(); // protection for pooling porpuses
            _gameCycleService.OnGamePaused.AddListener(OnGamePaused);
            _gameCycleService.OnGameContinued.AddListener(OnGameContinued);
            _abilityService.OnAbilitySelected.AddListener(OnAbilityUpdate);   
        }

        private void OnDisable()
        {
            _abilityService.OnAbilitySelected.RemoveListener(OnAbilityUpdate);
            _gameCycleService.OnGamePaused.RemoveListener(OnGamePaused);
            _gameCycleService.OnGameContinued.RemoveListener(OnGameContinued);
        }

        private void OnAbilityUpdate(CardData cardData)
        {
            if (cardData.EnemySpeedDecreasePercentage > 0)
            {
                _currentSpeed -= (int)(_currentSpeed * cardData.Speed);
                _navMeshAgent.speed = _currentSpeed;
            }

            if (cardData.ExperimentAmount > 0)
            {
                _currentExperimentAmount += (int)(_currentExperimentAmount * cardData.ExperimentAmount);
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

        private void OnDead()
        {
            RemoveObject();
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

        public void Init(ISpawner spawner)
        {
            _enemySpawner = spawner;
        }

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _currentSpeed;
            PlayerTransform = _enemyService.GetPlayerTransform();
            EntityBase = _enemyService.GetPlayerBase();
            OnStart();
        }

        public virtual void OnStart()
        {
            
        }

        private void Update()
        {
            if(_isGamePaused)
            {
                _navMeshAgent.isStopped = true;
                return;
            }
            else
            {
                _navMeshAgent.isStopped = false;
            }
            
            if (Vector3.Distance(PlayerTransform.position, transform.position) < (m_weaponData.Range - 1)) return;
            _navMeshAgent.SetDestination(PlayerTransform.position);
            
            AfterUpdate();
        }

        protected virtual void AfterUpdate()
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
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.35f).OnComplete(() =>
            {
                _currentHealth = m_enemyData.baseStats.Health;
                _progressBarController.SetProgressBar(0, m_enemyData.baseStats.Health, _currentHealth);
            });
            
        }

        public void RemoveObject()
        {
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
            {
                _experimentService.AddExperiment(_currentExperimentAmount);
                _enemySpawner.DeSpawn(this);
            });
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