using System;
using Cysharp.Threading.Tasks;
using Rune.Scripts.Base;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Spawner;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class PlayerService : IStartable
    {
        private PlayerSpawner _playerSpawner;
        private Transform _playerTransform;
        private bool _isPlayerCanSpawnable = true;
        private CameraService _cameraService;
        private EnemyService _enemyService;
        
        public Transform GetPlayerTransform() => _playerTransform;

        [Inject]
        public PlayerService(PlayerSpawner playerSpawner, CameraService cameraService)
        {
            _playerSpawner = playerSpawner;
            _cameraService = cameraService;
        }
        
        public void Start()
        {
            SpawnPlayer();
        }

        public async UniTask SpawnPlayer()
        {
            await UniTask.WaitUntil(() => _isPlayerCanSpawnable);
            _playerSpawner.Spawn();
            _playerTransform = _playerSpawner.GetTransform();
            _cameraService.UpdateCamera(_playerTransform);
        }


    }
}
