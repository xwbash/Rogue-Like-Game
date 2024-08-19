using Rune.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rune.Scripts.Base
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [FormerlySerializedAs("_weaponData")] [SerializeField] protected WeaponData weaponData;

        public abstract void Init(WeaponData weaponData, EntityBase entity);

    }
}