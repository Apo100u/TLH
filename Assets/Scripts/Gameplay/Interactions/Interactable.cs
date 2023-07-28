using TLH.Gameplay.Interactions.Receivers;
using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Interactions
{
    public class Interactable : MonoBehaviour
    {
        private IInteractionReceiver[] interactionReceivers;

        private void Awake()
        {
            interactionReceivers = GetComponents<IInteractionReceiver>();
        }

        public void HandleInteraction<T>(T interaction) where T : Interaction
        {
            for (int i = 0; i < interactionReceivers.Length; i++)
            {
                if (interactionReceivers[i] is IInteractionReceiver<T> receiverOfHandledInteraction)
                {
                    receiverOfHandledInteraction.HandleInteraction(interaction);
                }
            }
        }
    }
}