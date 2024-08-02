using UnityEngine;

namespace Rune.Scripts.Services
{
    public class EnemyService
    {
        private Transform _playerTransform;

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
        
        public Transform GetPlayerTransform()
        {
            return _playerTransform;
        }
    }
}