using TLH.Gameplay.Entities.ActionData;
using TLH.Gameplay.Entities.Behaviours;
using UnityEngine;

namespace TLH.Playground
{
    [RequireComponent(typeof(Combat))]
    public class PlaygroundTurret : MonoBehaviour
    {
        [SerializeField] private Combat combat;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private ProjectileAttackData attackData;
        
        private void Awake()
        {
            combat.SetPrimaryAttackData(attackData);
        }

        private void Update()
        {
            combat.UpdateAimPoint(aimTarget.position);
            combat.DemandPrimaryAttack();
        }
    }
}