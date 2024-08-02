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
    }
    
    [Serializable]
    public class PlayersSpawnData
    {
        public PlayerData PlayerData;
        public GameObject GameObject;
    }
    
    public abstract class PlayerBase : MonoBehaviour
    {
        public PlayerData PlayerData;
        public abstract void OnDead();

        public abstract void HitEnemy(int weaponDamage);
    }
}
