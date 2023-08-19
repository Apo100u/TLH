using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.ObjectPools;
using TLH.Gameplay.Entities.Attacks;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours
{
    public class Combat : EntityBehaviour
    {
        [Header("Projectiles")]
        [SerializeField] private Transform projectilesSpawnPoint;
        [SerializeField] private AttacksManager attacksManager;

        private AttackData primaryAttackData;
        private Vector3 aimPointWorldPosition;
        private AttacksPool attacksPool;
        
        private float remainingPrimaryAttackCooldown;

        public void Init(AttacksPool attacksPool)
        {
            this.attacksPool = attacksPool;
            attacksManager.Init(entity, attacksPool);
        }

        public void SetPrimaryAttackData(AttackData primaryAttackData)
        {
            this.primaryAttackData = primaryAttackData;
            OnAttackDataSet(primaryAttackData);
        }

        public void UpdateAimPoint(Vector3 aimPointWorldPosition)
        {
            this.aimPointWorldPosition = aimPointWorldPosition;
        }
        
        private void Update()
        {
            UpdateCooldowns();
        }

        private void UpdateCooldowns()
        {
            if (remainingPrimaryAttackCooldown > 0)
            {
                remainingPrimaryAttackCooldown -= Time.deltaTime;

                if (remainingPrimaryAttackCooldown < 0)
                {
                    remainingPrimaryAttackCooldown = 0;
                }
            }
        }

        public void DemandPrimaryAttack()
        {
            if (CanPerformPrimaryAttack())
            {
                PerformAttack(primaryAttackData);
                remainingPrimaryAttackCooldown = primaryAttackData.Cooldown;
            }
        }

        private void PerformAttack(AttackData attackData)
        {
            if (attackData.Prefab != null)
            {
                Vector2 direction = (aimPointWorldPosition - projectilesSpawnPoint.position).normalized;
                attacksManager.UseAttackFromAttackData(attackData, projectilesSpawnPoint, direction);
            }
        }

        private void OnAttackDataSet(AttackData attackData)
        {
            if (attackData is ProjectileAttackData projectileAttackData)
            {
                attacksPool.RegisterHandledAttack(projectileAttackData);
            }
        }

        private bool CanPerformPrimaryAttack()
        {
            return remainingPrimaryAttackCooldown <= 0;
        }
    }
}