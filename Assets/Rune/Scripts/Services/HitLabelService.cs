using Rune.Scripts.Gameplay.Guns_Related;
using Rune.Scripts.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class HitLabelService : IStartable
    {
        private PoolingService _poolingService;
        private PoolingFactory _poolingFactory;
        private GameObject _hitLabelGameObject;
        
        [Inject] 
        public HitLabelService(PoolingFactory poolingFactory, GameObject hitLabelGameObject)
        {
            _poolingFactory = poolingFactory;
            _hitLabelGameObject = hitLabelGameObject;
        }
        
        public void Start()
        {
            _poolingService = _poolingFactory.Create(_hitLabelGameObject, 50);
        }
        
        public void RemoveObject(IPoolableObject projectile)
        {
            _poolingService.RemoveObject(projectile);
        }


        public IPoolableObject GetLabel()
        {
            return _poolingService.GetObject();
        }
    }
}