using UnityEngine;

namespace TLH.Gameplay.Interactions.Types
{
    [CreateAssetMenu(fileName = "New Knockback Interaction", menuName = InteractionMenuName + nameof(KnockbackInteraction))]
    public class KnockbackInteraction : Interaction
    {
        [SerializeField] private float power = 1f;
        public float Power => power;

        [Tooltip("If checked, " + nameof(power) + " will be used to multiply velocity of the interactable.")]
        [SerializeField] private bool multiplyExistingVelocity;
        public bool MultiplyExistingVelocity => multiplyExistingVelocity;
    }
}