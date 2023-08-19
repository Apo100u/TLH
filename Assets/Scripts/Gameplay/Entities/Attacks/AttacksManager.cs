using System.Collections.Generic;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.ObjectPools;
using UnityEngine;

namespace TLH.Gameplay.Entities.Attacks
{
    public class AttacksManager : MonoBehaviour
    {
        private Entity attacksSource;
        private AttacksPool attacksPool;
        private List<EntityAttack> activeAttacks = new();

        public void Init(Entity attacksSource, AttacksPool attacksPool)
        {
            this.attacksSource = attacksSource;
            this.attacksPool = attacksPool;
        }

        private void Update()
        {
            for (int i = activeAttacks.Count - 1; i >= 0; i--)
            {
                activeAttacks[i].OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (int i = activeAttacks.Count - 1; i >= 0; i--)
            {
                activeAttacks[i].OnFixedUpdate();
            }
        }
        
        public void UseAttackFromAttackData(AttackData attackData, Transform spawnPoint, Vector2 directionNormalized)
        {
            EntityAttack attack = attacksPool[attackData].Get();
            
            attack.Deactivated += OnAttackDeactivated;
            attack.Destroying += OnAttackDestroying;
            
            activeAttacks.Add(attack);
            attack.transform.position = spawnPoint.position;
            attack.Activate(directionNormalized, attacksSource);
        }

        private void OnAttackDeactivated(EntityAttack attack)
        {
            attack.Deactivated -= OnAttackDeactivated;
            activeAttacks.Remove(attack);
        }

        private void OnAttackDestroying(EntityAttack attack)
        {
            attack.Destroying -= OnAttackDestroying;
            activeAttacks.Remove(attack);
        }
    }
}