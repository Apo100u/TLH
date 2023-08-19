using System;
using System.Collections.Generic;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Entities.Attacks;
using UnityEngine.Pool;

namespace TLH.Gameplay.ObjectPools
{
    public class AttacksPool
    {
        private Dictionary<AttackData, ObjectPool<EntityAttack>> poolsByAttackData = new();

        public ObjectPool<EntityAttack> this[AttackData attackData] => poolsByAttackData[attackData];

        public void RegisterHandledAttack(AttackData attackData)
        {
            if (!poolsByAttackData.ContainsKey(attackData))
            {
                ObjectPool<EntityAttack> pool = new(
                    GetCreateProjectileFunc(attackData),
                    null,
                    null,
                    OnAttackSurpassedPoolCapacity);

                poolsByAttackData.Add(attackData, pool);
            }
        }
        
        private Func<EntityAttack> GetCreateProjectileFunc(AttackData attackData)
        {
            return () =>
            {
                EntityAttack createdAttack = UnityEngine.Object.Instantiate(attackData.Prefab);
                createdAttack.Init(attackData);
                createdAttack.Deactivated += OnAttackDeactivated;

                for (int i = 0; i < attackData.Interactions.Length; i++)
                {
                    createdAttack.AddInteraction(attackData.Interactions[i]);
                }
                
                return createdAttack;
            };
        }

        private void OnAttackSurpassedPoolCapacity(EntityAttack attack)
        {
            attack.Destroy();
        }
        
        private void OnAttackDeactivated(EntityAttack attack)
        {
            poolsByAttackData[attack.AttackData].Release(attack);
        }
    }
}