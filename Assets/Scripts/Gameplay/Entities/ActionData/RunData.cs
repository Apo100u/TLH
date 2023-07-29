using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    [CreateAssetMenu(fileName = "New Run Data", menuName = EntityActionDataMenuName + nameof(RunData))]
    public class RunData : EntityActionData
    {
        [SerializeField][Min(0f)] private float maxSpeed = 5f;
        public float MaxSpeed => maxSpeed;
        
        [Tooltip("Time in seconds it takes to reach or lose max speed.")]
        [SerializeField][Min(0f)] private float sliding;
        public float Sliding => sliding;
    }
}