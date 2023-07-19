using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Run Action Data", menuName = EntityActionDataMenuName + nameof(RunData))]
    public class RunData : EntityActionData
    {
        [SerializeField] private float speed = 5f;

        public float Speed => speed;
    }
}