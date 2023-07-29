using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Projectiles;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours
{
    public class Combat : EntityBehaviour
    {
        [SerializeField] private ProjectilesManager projectilesManager;

        private AttackData primaryAttackData;

        public void SetPrimaryAttackData(AttackData primaryAttackData)
        {
            this.primaryAttackData = primaryAttackData;
        }

        public void DemandPrimaryAttack()
        {
            switch (primaryAttackData)
            {
                case ProjectileAttackData projectileAttackData: PerformProjectileAttack(projectileAttackData); break;
                default: Debug.LogError($"{gameObject.name} tried to perform unhandled type of attack.", this); break;
            }
        }

        private void PerformProjectileAttack(ProjectileAttackData attackData)
        {
            if (attackData.ProjectilePrefab != null)
            {
                projectilesManager.ShootProjectileFromAttackData(attackData);
            }
        }
    }
}