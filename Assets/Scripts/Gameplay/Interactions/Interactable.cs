using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Interactions
{
    public class Interactable : MonoBehaviour
    {
        private IInteractionReceiver<DamageInteraction>[] damageReceivers;
        private IInteractionReceiver<KnockbackInteraction>[] knockbackReceivers;
        private IInteractionReceiver<DestructionInteraction>[] destructionReceivers;
        
        private void Awake()
        {
            damageReceivers = GetComponents<IInteractionReceiver<DamageInteraction>>();
            knockbackReceivers = GetComponents<IInteractionReceiver<KnockbackInteraction>>();
            destructionReceivers = GetComponents<IInteractionReceiver<DestructionInteraction>>();
        }

        public void HandleInteraction<T>(T interaction, InteractionInitiator initiator) where T : Interaction
        {
            switch (interaction)
            {
                case DamageInteraction      damageInteraction:      SendInteraction(damageInteraction,      initiator, damageReceivers);      break;
                case KnockbackInteraction   knockbackInteraction:   SendInteraction(knockbackInteraction,   initiator, knockbackReceivers);   break;
                case DestructionInteraction destructionInteraction: SendInteraction(destructionInteraction, initiator, destructionReceivers); break;

                default: Debug.LogError($"{nameof(Interactable)} doesn't implement handling interaction type {interaction.GetType()}." +
                                        " Most likely it should be added."); break;
            }
        }

        private void SendInteraction<T>(T interaction, InteractionInitiator initiator, IInteractionReceiver<T>[] receivers) where T : Interaction
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                receivers[i].HandleInteraction(interaction, initiator);
            }
        }
    }
}