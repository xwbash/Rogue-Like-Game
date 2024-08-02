using System.Collections.Generic;
using Rune.Scripts.Base;
using Rune.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using VContainer.Unity;

namespace Rune.Scripts.Spawner
{
    public class PlayerSpawner : MonoBehaviour, ISpawner
    {
        public UnityEvent OnPlayerSpawned = new UnityEvent();        
        [SerializeField] private PlayersSpawnData m_playerGameObject;

        private IObjectResolver _objectResolver;
        private PlayerBase _mainPlayerBase;
        private Transform _playerTransform;

        [Inject]
        private void Construct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public void Spawn()
        {
            var spawnedObject = _objectResolver.Instantiate(m_playerGameObject.GameObject);
            _playerTransform = spawnedObject.transform;
            OnPlayerSpawned.Invoke();
            spawnedObject.transform.position = Vector3.zero;
            _mainPlayerBase = spawnedObject.GetComponent<PlayerBase>();
        }

        public void DeSpawn(IPoolableObject poolableObject)
        {
            
        }

        public Transform GetTransform() => _playerTransform;

        public PlayerBase GetPlayerBase()
        {
            return _mainPlayerBase;
        }
    }
}