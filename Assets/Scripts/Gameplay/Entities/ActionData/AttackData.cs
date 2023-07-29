using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Attack Action Data", menuName = EntityActionDataMenuName + nameof(AttackData))]
    public class AttackData : EntityActionData
    {
        [SerializeField] private Interaction[] interactions;
        public Interaction[] Interactions => interactions;
        
        [SerializeField] private Projectile projectile;
        public Projectile Projectile => projectile;

        // [SerializeField] private bool useMeleeColliders;
        // public bool UseMeleeColliders => useMeleeColliders; // TODO: Add melee colliders to Combat behaviour and activate them with interactions on attack
    }
}