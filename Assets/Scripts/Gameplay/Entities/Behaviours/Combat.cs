using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Projectiles;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours
{
    public class Combat : EntityBehaviour
    {
        [Header("Projectiles")]
        [SerializeField] private Transform projectilesSpawnPoint;
        [SerializeField] private ProjectileAttacksManager projectileAttacksManager;

        private AttackData primaryAttackData;
        private Vector3 aimPointWorldPosition;

        public void SetPrimaryAttackData(AttackData primaryAttackData)
        {
            this.primaryAttackData = primaryAttackData;
            OnAttackDataSet(primaryAttackData);
        }

        public void UpdateAimPoint(Vector3 aimPointWorldPosition)
        {
            this.aimPointWorldPosition = aimPointWorldPosition;
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
                Vector2 direction = (aimPointWorldPosition - transform.position).normalized;
                projectileAttacksManager.ShootProjectileFromAttackData(attackData, projectilesSpawnPoint.position, direction);
            }
        }

        private void OnAttackDataSet(AttackData attackData)
        {
            if (attackData is ProjectileAttackData projectileAttackData)
            {
                projectileAttacksManager.RegisterHandledProjectileAttack(projectileAttackData);
            }
        }
    }
}