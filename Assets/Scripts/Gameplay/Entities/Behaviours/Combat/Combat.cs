using TLH.Gameplay.Entities.ActionData;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours.Combat
{
    public class Combat : EntityBehaviour
    {
        private AttackData primaryAttackData;
        private ProjectilesManager projectilesManager = new();

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