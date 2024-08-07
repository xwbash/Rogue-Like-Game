using Rune.Scripts.Services;
using UnityEngine;

namespace Rune.Scripts.Interfaces
{
    public interface IPoolableObject
    {
        public void Init(ISpawner spawner);
        public bool IsObjectActive();
        public void SetObjectActive(bool isActive);
        
        public void OnObjectSpawned();
        public void SetSpawnPoint(Vector3 spawnPoint);

        public PoolingService GetPoolingService();
        public void SetPoolingService(PoolingService poolingService);
        public Vector3 GetPosition();
    }
}