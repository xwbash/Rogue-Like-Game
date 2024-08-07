using Rune.Scripts.Base;
using UnityEngine;

namespace Rune.Scripts.Services
{
    public class EnemyService
    {
        private Transform _playerTransform;
        private EntityBase _entityBase;

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
        
        public void SetPlayerBase(EntityBase entityBase)
        {
            _entityBase = entityBase;
        }
        
        public Transform GetPlayerTransform()
        {
            return _playerTransform;
        }


        public EntityBase GetPlayerBase()
        {
            return _entityBase;
        }
    }
}