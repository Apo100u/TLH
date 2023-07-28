using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Attack Action Data", menuName = EntityActionDataMenuName + nameof(AttackData))]
    public class AttackData : EntityActionData
    {
        [SerializeField] private Projectile projectile;
        public Projectile Projectile => projectile;
    }
}