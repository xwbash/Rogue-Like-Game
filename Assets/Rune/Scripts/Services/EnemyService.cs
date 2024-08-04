using Rune.Scripts.Base;
using UnityEngine;

namespace Rune.Scripts.Services
{
    public class EnemyService
    {
        private Transform _playerTransform;
        private PlayerBase _playerBase;

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
        
        public void SetPlayerBase(PlayerBase playerBase)
        {
            _playerBase = playerBase;
        }
        
        public Transform GetPlayerTransform()
        {
            return _playerTransform;
        }


        public PlayerBase GetPlayerBase()
        {
            return _playerBase;
        }
    }
}