using System;
using UnityEngine;

namespace Rune.Scripts.Base
{
    [Serializable]
    public class PlayerData
    {
        public int Health;
        public float Speed;
        public int Damage;
        public int Range;
        public PlayerType PlayerType;
    }
    
    [Serializable]
    public class PlayersSpawnData
    {
        public PlayerData PlayerData;
        public GameObject GameObject;
    }

    public enum PlayerType
    {
        Player,
        Enemy
    }
    
    public abstract class PlayerBase : MonoBehaviour
    {
        public PlayerData PlayerData;
        
        public abstract void OnDead();

        public abstract void HitEnemy(int weaponDamage);

        public PlayerType GetPlayerType()
        {
            return PlayerData.PlayerType;
        }
    }
}
