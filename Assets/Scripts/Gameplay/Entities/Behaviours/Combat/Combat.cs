using TLH.Gameplay.Entities.ActionData;

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
            PerformAttack(primaryAttackData);
        }

        private void PerformAttack(AttackData attackData)
        {
            if (attackData.ProjectilePrefab != null)
            {
                projectilesManager.ShootProjectileFromAttackData(attackData);
            }
        }
    }
}