using System.Collections.Generic;
using Rune.Scripts.Gameplay.Guns_Related;
using Rune.Scripts.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class BulletService : IStartable
    {
        private PoolingService _poolingService;
        private PoolingFactory _poolingFactory;
        private List<ProjectileCreationData> _objectToSpawn;

        private Dictionary<ProjectileType, PoolingService> _poolingServices =
            new Dictionary<ProjectileType, PoolingService>();
        
        [Inject] 
        public BulletService(PoolingFactory poolingFactory, List<ProjectileCreationData> objectToSpawn)
        {
            _objectToSpawn = objectToSpawn;
            _poolingFactory = poolingFactory;
        }
        
        public void Start()
        {
            foreach (var spawnObject in _objectToSpawn)
            {
                _poolingService = _poolingFactory.Create(spawnObject.ProjectileObject, 200);
                _poolingServices.Add(spawnObject.ProjectileData, _poolingService);
            }
        }
        
        public void RemoveObject(IPoolableObject projectile, ProjectileType projectileType)
        {
            if (_poolingServices.ContainsKey(projectileType))
            {
                _poolingServices[projectileType].RemoveObject(projectile);
            }
        }


        public IPoolableObject GetBullet(ProjectileType projectileType)
        {
            if (_poolingServices.ContainsKey(projectileType))
            {
                return _poolingServices[projectileType].GetObject();
            }

            return null;
        }
    }
}