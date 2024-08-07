using Rune.Scripts.ScriptableObjects;
using UnityEngine;

namespace Rune.Scripts.Base
{

    public enum PlayerType
    {
        Player,
        Enemy
    }
    
    public abstract class EntityBase : MonoBehaviour
    {
        [SerializeField] private PlayerType _playerType;
        public abstract void GetHit(int weaponDamage);

        public PlayerType GetPlayerType()
        {
            return _playerType;
        }
    }
}
