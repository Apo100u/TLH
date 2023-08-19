using TLH.Gameplay.Entities.Attacks;
using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Attack Data", menuName = EntityActionDataMenuName + nameof(AttackData))]
    public class AttackData : EntityActionData
    {
        [SerializeField] private EntityAttack prefab;
        public EntityAttack Prefab => prefab;

        [SerializeField] private float speed = 10f;
        public float Speed => speed;
        
        [SerializeField] private float cooldown = 0.5f;
        public float Cooldown => cooldown;
        
        [Tooltip("Time in seconds after which this object will be destroyed, if it wasn't already destroyed.")]
        [SerializeField][Min(0f)] private float lifeTimeInSec = 10f;
        public float LifeTimeInSec => lifeTimeInSec;
        
        [Tooltip("Attack will go through these layers and still interact with them if there is an interaction.")]
        [SerializeField] private LayerMask layersToPierce;
        public LayerMask LayersToPierce => layersToPierce;
        
        [SerializeField] private Interaction[] interactions;
        public Interaction[] Interactions => interactions;
    }
}