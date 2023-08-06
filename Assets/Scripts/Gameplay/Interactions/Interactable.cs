using TLH.Gameplay.Interactions.Receivers;
using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Interactions
{
    public class Interactable : MonoBehaviour
    {
        private IInteractionReceiver<DamageInteraction>[] damageInteractionReceivers;
        
        private void Awake()
        {
            damageInteractionReceivers = GetComponents<IInteractionReceiver<DamageInteraction>>();
        }

        public void HandleInteraction<T>(T interaction) where T : Interaction
        {
            switch (interaction)
            {
                case DamageInteraction damageInteraction: SendInteractionToReceivers(damageInteraction, damageInteractionReceivers); break;
                
                default: Debug.LogError($"{nameof(Interactable)} doesn't implement handling interaction type {interaction.GetType()}." +
                                        "Most likely it should be added."); break;
            }
        }

        private void SendInteractionToReceivers<T>(T interaction, IInteractionReceiver<T>[] receivers) where T : Interaction
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                receivers[i].HandleInteraction(interaction);
            }
        }
    }
}