using UnityEngine;

namespace Rune.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EntitySpawnData", menuName = "Create Data/Entity Spawn Data", order = 0)]
    public class EntitySpawnData : ScriptableObject
    {
        public PlayerData PlayerData;
        public GameObject GameObject;
    }
}