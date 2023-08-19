using TLH.Extensions;
using TLH.Gameplay.Interactions;
using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Entities.Attacks
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Interactable))]
    public class Projectile : EntityAttack, IInteractionReceiver<KnockbackInteraction>, IInteractionReceiver<DestructionInteraction>
    {
        public void HandleInteraction(KnockbackInteraction interaction, InteractionInitiator initiator)
        {
            Vector2 directionNormalized = (transform.position - initiator.transform.position).normalized;

            float speed = interaction.MultiplyExistingVelocity
                ? entityAttackRigidbody.velocity.magnitude * interaction.Power
                : interaction.Power;

            SetVelocity(directionNormalized, speed);
            
            if (initiator is EntityAttack attack)
            {
                SetSource(attack.CurrentSource);
            }
        }
        
        public void HandleInteraction(DestructionInteraction interaction, InteractionInitiator initiator)
        {
            if (interaction.LayersToDestroy.ContainsLayer(gameObject.layer))
            {
                Deactivate();
            }
        }

        protected override void HandleUnignoredTrigger(Collider2D collider)
        {
            if (!AttackData.LayersToPierce.ContainsLayer(collider.gameObject.layer))
            {
                Deactivate();
            }
        }
    }
}