
using UnityEngine;

namespace TLH.Gameplay.Entities.Actions
{
    [CreateAssetMenu(fileName = "New Run Action", menuName = EntityActionMenuName + nameof(Run))]
    public class Run : EntityAction
    {
        [SerializeField] private float speed = 5;

        public float Speed => speed;
    }
}