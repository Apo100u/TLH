using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Entities.Behaviours;
using TLH.Gameplay.ObjectPools;
using UnityEngine;

namespace TLH.Playground
{
    [RequireComponent(typeof(Combat))]
    public class PlaygroundTurret : MonoBehaviour
    {
        [SerializeField] private Combat combat;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private Pools pools;
        [SerializeField] private ProjectileAttackData attackData;
        
        private void Awake()
        {
            combat.Init(pools.Projectiles);
            combat.SetPrimaryAttackData(attackData);
        }

        private void Update()
        {
            combat.UpdateAimPoint(aimTarget.position);
            combat.DemandPrimaryAttack();
        }
    }
}