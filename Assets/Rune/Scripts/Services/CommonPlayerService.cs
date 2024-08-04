using Rune.Scripts.Base;
using Rune.Scripts.Spawner;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class CommonPlayerService : IStartable
    {
        private EnemySpawner _enemySpawner;
        private PlayerSpawner _playerSpawner;
        private EnemyService _enemyService;
        
        [Inject]
        private void Construct(EnemySpawner enemySpawner, PlayerSpawner playerSpawner, EnemyService enemyService)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _enemyService = enemyService;
            
            _playerSpawner.OnPlayerSpawned.AddListener(OnPlayerSpawned);
        }

        private void OnPlayerSpawned()
        {
            _enemySpawner.SetPlayerTransform(_playerSpawner.GetTransform());
            _enemyService.SetPlayerTransform(_playerSpawner.GetTransform());
            _enemyService.SetPlayerBase(_playerSpawner.GetPlayerBase());
        }

        public PlayerBase GetClosestEnemy()
        {
            return _enemySpawner.GetClosestAlly(_playerSpawner.GetTransform().position);
        }

        public PlayerBase GetPlayer()
        {
            return _playerSpawner.GetPlayerBase();
        }

        public Transform GetPlayerTransform()
        {
            return _playerSpawner.GetTransform();
        }

        public void Start()
        {
            
        }
    }
}