using System;
using System.Collections.Generic;
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
        private Queue<Action> lateUpdateActions = new();

        public void HandleInteraction(KnockbackInteraction interaction, InteractionInitiator initiator)
        {
            Vector2 directionNormalized = (transform.position - initiator.transform.position).normalized;

            float speed = interaction.MultiplyExistingVelocity
                ? entityAttackRigidbody.velocity.magnitude * interaction.Power
                : interaction.Power;

            SetVelocity(directionNormalized, speed);

            if (initiator is EntityAttack attack)
            {
                Entity source = attack.CurrentSource;

                lateUpdateActions.Enqueue(() =>
                {
                    SetSource(source);
                });
            }
        }

        public void HandleInteraction(DestructionInteraction interaction, InteractionInitiator initiator)
        {
            if (interaction.LayersToDestroy.ContainsLayer(gameObject.layer))
            {
                Deactivate();
            }
        }

        private void LateUpdate()
        {
            while (lateUpdateActions.Count > 0)
            {
                lateUpdateActions.Dequeue()?.Invoke();
            }
        }
    }
}