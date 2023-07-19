using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Run Action", menuName = EntityActionDataMenuName + nameof(RunData))]
    public class RunData : EntityActionData
    {
        [SerializeField] private float speed = 5;

        public float Speed => speed;
    }
}