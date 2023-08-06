using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Entities.ActionData
{
    public abstract class AttackData : EntityActionData
    {
        [SerializeField] private Interaction[] interactions;
        public Interaction[] Interactions => interactions;

        [SerializeField] private float cooldown = 0.5f;
        public float Cooldown => cooldown;
    }
}