using TLH.Gameplay.Entities;
using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Entities.Behaviours.Combat;
using TLH.Gameplay.ObjectPools;
using UnityEngine;

namespace TLH.Playground
{
    [RequireComponent(typeof(Combat))]
    public class PlaygroundTurret : Entity
    {
        [SerializeField] private Combat combat;
        [SerializeField] private Transform attackSpawnPoint;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private Pools pools;
        [SerializeField] private AttackData attackData;
        
        private void Awake()
        {
            combat.Init(pools.Attacks);
            combat.SetPrimaryAttackData(attackData, attackSpawnPoint);
        }

        private void Update()
        {
            combat.UpdateAimPoint(aimTarget.position);
            combat.DemandPrimaryAttack();
        }
    }
}