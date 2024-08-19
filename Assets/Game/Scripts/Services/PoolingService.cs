using System.Collections.Generic;
using JetBrains.Annotations;
using Rune.Scripts.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Services
{
    public class PoolingFactory
    {
        private IObjectResolver _objectResolver;
        
        [Inject]
        public PoolingFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public PoolingService Create(GameObject PoolObjectToSpawn, int objectAmount)
        {
            return new PoolingService(PoolObjectToSpawn, objectAmount, _objectResolver);
        }
    }
    
    public class PoolingService
    {
        private int _objectAmount = 20;
        private List<IPoolableObject> _poolObject = new List<IPoolableObject>();


        public PoolingService (GameObject PoolObjectToSpawn, int objectAmount, IObjectResolver objectResolver)
        {
            var newPoolParent = GameObject.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
            newPoolParent.name = "-- POOL OBJECT --";
            
            _objectAmount = objectAmount;
            
            for (int i = 0; i < _objectAmount; i++)
            {
                var poolObject = objectResolver.Instantiate(PoolObjectToSpawn);

                poolObject.transform.SetParent(newPoolParent.transform);
                
                var poolableObject = poolObject.GetComponent<IPoolableObject>();
                poolableObject.SetPoolingService(this);
                poolableObject.SetObjectActive(true);
                _poolObject.Add(poolableObject);
            }
        }
        
        public IPoolableObject GetObject()
        {
            IPoolableObject activeObject;
            
            for (int i = 0; i < _objectAmount; i++)
            {
                var poolSelectedObject = _poolObject[i];
                
                if (poolSelectedObject.IsObjectActive())
                {
                    poolSelectedObject.OnObjectSpawned();
                    poolSelectedObject.SetObjectActive(false);
                    activeObject = poolSelectedObject;
                    return activeObject;
                }
            }

            return null;
        }

        public void RemoveObject(IPoolableObject poolableObject)
        {
            foreach (var poolObject in _poolObject)
            {
                if (poolObject == poolableObject)
                {
                    poolObject.SetObjectActive(true);
                    break;
                }
            }
        }

        public IPoolableObject GetObjectWithoutWake()
        {
            IPoolableObject activeObject;
            
            for (int i = 0; i < _objectAmount; i++)
            {
                var poolSelectedObject = _poolObject[i];
                
                if (poolSelectedObject.IsObjectActive())
                {
                    activeObject = poolSelectedObject;
                    return activeObject;
                }
            }

            return null;
        }
    }
}