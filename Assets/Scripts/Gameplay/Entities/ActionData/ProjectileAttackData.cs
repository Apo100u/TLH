using TLH.Gameplay.Projectiles;
using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Projectile Attack Data", menuName = EntityActionDataMenuName + nameof(ProjectileAttackData))]
    public class ProjectileAttackData : AttackData
    {
        [SerializeField] private Projectile projectilePrefab;
        public Projectile ProjectilePrefab => projectilePrefab;
        
        [Tooltip("Layers on which this object will be destroyed. It will pierce other layers and still interact with them if possible.")]
        [SerializeField] private LayerMask layersToDestroyOn;
        public LayerMask LayersToDestroyOn => layersToDestroyOn;
        
        [Tooltip("Time in seconds after which this object will be destroyed, if it wasn't already destroyed.")]
        [SerializeField][Min(0f)] private float lifeTimeInSec = 10f;
        public float LifeTimeInSec => lifeTimeInSec;
    }
}