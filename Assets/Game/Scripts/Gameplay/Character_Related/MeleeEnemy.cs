using UnityEngine;

namespace Rune.Scripts.Gameplay.Character_Related
{
    public class MeleeEnemy : Enemy
    {
        protected override void AfterUpdate()
        {
            base.AfterUpdate();
            
            ShootingCooldown -= Time.deltaTime;
            if(ShootingCooldown > 0) return;
            
            if (PlayerTransform)
            {
                var closestEnemyDistance = Vector3.Distance(PlayerTransform.position, transform.position);
            
                if (closestEnemyDistance < m_weaponData.Range)
                {
                    EntityBase.GetHit(m_weaponData.Damage);
                    ShootingCooldown = AttackCooldown;
                }    
            }
        }
    }
}