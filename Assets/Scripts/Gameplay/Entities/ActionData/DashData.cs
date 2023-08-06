using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Dash Data", menuName = EntityActionDataMenuName + nameof(DashData))]
    public class DashData : EntityActionData
    {
        [SerializeField] private float distance = 5f;
        public float Distance => distance;
        
        [SerializeField] private float speed = 15f;
        public float Speed => speed;

        [SerializeField] private float cooldown = 3f;
        public float Cooldown => cooldown;
        
        [Tooltip("It will be impossible for dash to end on those layers, but it will still be able to move through them it empty space is available further.")]
        [SerializeField] private LayerMask layersBlockingDashEnd;
        public LayerMask LayersBlockingDashEnd => layersBlockingDashEnd;
    }
}