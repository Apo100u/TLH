using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Run Action Data", menuName = EntityActionDataMenuName + nameof(RunData))]
    public class RunData : EntityActionData
    {
        [SerializeField] private float maxSpeed = 5f;
        public float MaxSpeed => maxSpeed;
        
        [Tooltip("Time in seconds it takes to reach or lose max speed.")]
        [SerializeField] private float sliding;
        public float Sliding => sliding;
    }
}