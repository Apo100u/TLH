using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Dash Action Data", menuName = EntityActionDataMenuName + nameof(DashData))]
    public class DashData : EntityActionData
    {
        [SerializeField] private float distance = 5f;
        public float Distance => distance;
        
        [SerializeField] private float speed = 15f;
        public float Speed => speed;

    }
}