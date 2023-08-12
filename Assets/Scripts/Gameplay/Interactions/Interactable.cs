using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Interactions
{
    public class Interactable : MonoBehaviour
    {
        private IInteractionReceiver<DamageInteraction>[] damageInteractionReceivers;
        private IInteractionReceiver<KnockbackInteraction>[] knockbackInteractionReceivers;
        
        private void Awake()
        {
            damageInteractionReceivers = GetComponents<IInteractionReceiver<DamageInteraction>>();
            knockbackInteractionReceivers = GetComponents<IInteractionReceiver<KnockbackInteraction>>();
        }

        public void HandleInteraction<T>(T interaction, InteractionInitiator initiator) where T : Interaction
        {
            switch (interaction)
            {
                case DamageInteraction damageInteraction:
                    SendInteractionToReceivers(damageInteraction, initiator, damageInteractionReceivers);
                    break;
                case KnockbackInteraction knockbackInteraction:
                    SendInteractionToReceivers(knockbackInteraction, initiator, knockbackInteractionReceivers);
                    break;

                default:
                    Debug.LogError($"{nameof(Interactable)} doesn't implement handling interaction type {interaction.GetType()}." +
                                   " Most likely it should be added.");
                    break;
            }
        }

        private void SendInteractionToReceivers<T>(T interaction, InteractionInitiator initiator, IInteractionReceiver<T>[] receivers) where T : Interaction
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                receivers[i].HandleInteraction(interaction, initiator);
            }
        }
    }
}