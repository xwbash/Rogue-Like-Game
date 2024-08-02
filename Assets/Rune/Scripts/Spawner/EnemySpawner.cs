using System;
using System.Collections.Generic;
using Rune.Scripts.Base;
using Rune.Scripts.Interfaces;
using Rune.Scripts.Services;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Rune.Scripts.Spawner
{
    public class EnemySpawner : MonoBehaviour, ISpawner
    {
        [SerializeField] private List<GameObject> m_enemyGameObjects = new List<GameObject>();

        
        private Transform _playerTransform;
        private List<IPoolableObject> _aliveObjects = new List<IPoolableObject>();
        private List<PoolingService> _poolingServices = new List<PoolingService>();

        private int _playerLimit = 10;
        private float _spawnRadius = 50f;

        [Inject]
        private void Construct(PoolingFactory poolingFactory)
        {
            foreach (var playerSpawn in m_enemyGameObjects)
            {
                _poolingServices.Add(poolingFactory.Create(playerSpawn, 10));
            }
        }
        
        public IPoolableObject SpawnObject()
        {
            var poolingService = _poolingServices[Random.Range(0,_poolingServices.Count - 1)].GetObjectWithoutWake();

            if (poolingService != null)
            {
                if (poolingService.IsObjectActive())
                {
                    _aliveObjects.Add(poolingService);
                    poolingService.SetObjectActive(false);
                    poolingService.OnObjectSpawned();
                    return poolingService;
                }
            }
            
            foreach (var targetService in _poolingServices)
            {
                var targetObject = targetService.GetObject();
                if (targetObject != null)
                {
                    if (targetObject.IsObjectActive())
                    {
                        _aliveObjects.Add(targetObject);
                        targetObject.SetObjectActive(false);
                        targetObject.OnObjectSpawned();
                        return targetObject;
                    }
                }
            }

            return null;
        }
        
        public void RemoveObject(PoolingService poolingService, IPoolableObject enemyObject)
        {
            _aliveObjects.Remove(enemyObject);
            poolingService.RemoveObject(enemyObject);
        }

        public PlayerBase GetClosestAlly(Vector3 position)
        {
            PlayerBase closestEnemy = null;
            var minimumDistance = 10000f;
            
            foreach (var aliveObject in _aliveObjects)
            {
                Vector3 enemyPosition = aliveObject.GetPosition();
                var distance = Vector3.Distance(enemyPosition, position);

                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    closestEnemy = (PlayerBase)aliveObject;
                }
            }

            return closestEnemy;
        }

        public void Update()
        {
            if (_aliveObjects.Count >= _playerLimit || !_playerTransform) return;
            Spawn();
        }

        public void Spawn()
        {
            var spawnedObject = SpawnObject();
            spawnedObject.Init(this);
            spawnedObject.OnObjectSpawned();
            spawnedObject.SetSpawnPoint(GetRandomPositionAroundPlayer(_playerTransform.position, _spawnRadius));
        }

        public void DeSpawn(IPoolableObject poolableObject)
        {
            RemoveObject(poolableObject.GetPoolingService(), poolableObject);
        }

        private Vector3 GetRandomPositionAroundPlayer(Vector3 playerPosition, float radius)
        {
            float u = Random.value;
            float v = Random.value;
            float theta = u * Mathf.PI * 2;
            float phi = Mathf.Acos(2 * v - 1);
            float r = radius * Mathf.Pow(Random.value, 1f / 3f);

            float x = r * Mathf.Sin(phi) * Mathf.Cos(theta);
            float z = r * Mathf.Cos(phi);

            return playerPosition + new Vector3(x, 0, z);
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
    }
}