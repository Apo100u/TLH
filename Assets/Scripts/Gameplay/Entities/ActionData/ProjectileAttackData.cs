using TLH.Gameplay.Entities.Attacks;
using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Projectile Attack Data", menuName = EntityActionDataMenuName + nameof(ProjectileAttackData))]
    public class ProjectileAttackData : AttackData
    {
        [SerializeField] private Projectile projectilePrefab;
        public Projectile ProjectilePrefab => projectilePrefab;

        [SerializeField] private float speed = 10f;
        public float Speed => speed;
        
        [Tooltip("Time in seconds after which this object will be destroyed, if it wasn't already destroyed.")]
        [SerializeField][Min(0f)] private float lifeTimeInSec = 10f;
        public float LifeTimeInSec => lifeTimeInSec;
        
        [Tooltip("The projectile will go through these layers and still interact with them if there is an interaction.")]
        [SerializeField] private LayerMask layersToPierce;
        public LayerMask LayersToPierce => layersToPierce;
    }
}