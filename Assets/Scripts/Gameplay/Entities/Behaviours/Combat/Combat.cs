using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.ObjectPools;
using TLH.Gameplay.Entities.Attacks;
using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours.Combat
{
    public class Combat : EntityBehaviour
    {
        [SerializeField] private AttacksManager attacksManager;
        [SerializeField] private Transform[] objectsToRotateTowardsAimPoint;

        private AttackInfo primaryAttackInfo;
        private AttacksPool attacksPool;
        private Vector3 aimPointWorldPosition;

        public void Init(AttacksPool attacksPool)
        {
            this.attacksPool = attacksPool;
            attacksManager.Init(entity, attacksPool);
        }

        public void SetPrimaryAttackData(AttackData attackData, params Transform[] spawnPoints)
        {
            primaryAttackInfo = new AttackInfo(attackData, spawnPoints);
            OnAttackDataSet(attackData);
        }

        public void UpdateAimPoint(Vector3 aimPointWorldPosition)
        {
            this.aimPointWorldPosition = aimPointWorldPosition;

            for (int i = 0; i < objectsToRotateTowardsAimPoint.Length; i++)
            {
                objectsToRotateTowardsAimPoint[i].up = aimPointWorldPosition - objectsToRotateTowardsAimPoint[i].position;
            }
        }
        
        private void Update()
        {
            UpdateCooldowns();
        }

        private void UpdateCooldowns()
        {
            if (primaryAttackInfo.RemainingCooldown > 0)
            {
                primaryAttackInfo.RemainingCooldown -= Time.deltaTime;

                if (primaryAttackInfo.RemainingCooldown < 0)
                {
                    primaryAttackInfo.RemainingCooldown = 0;
                }
            }
        }

        public void DemandPrimaryAttack()
        {
            if (CanPerformPrimaryAttack())
            {
                Transform spawnPoint = primaryAttackInfo.MoveNextSpawnPoint();
                
                PerformAttack(primaryAttackInfo.Data, spawnPoint);
                primaryAttackInfo.RemainingCooldown = primaryAttackInfo.Data.Cooldown;
            }
        }

        private void PerformAttack(AttackData attackData, Transform spawnPoint)
        {
            if (attackData.Prefab != null)
            {
                Vector2 direction = (aimPointWorldPosition - spawnPoint.position).normalized;
                attacksManager.UseAttackFromAttackData(attackData, spawnPoint.position, direction);
            }
        }

        private void OnAttackDataSet(AttackData attackData)
        {
            attacksPool.RegisterHandledAttack(attackData);
        }

        private bool CanPerformPrimaryAttack()
        {
            return primaryAttackInfo.RemainingCooldown <= 0;
        }
    }
}