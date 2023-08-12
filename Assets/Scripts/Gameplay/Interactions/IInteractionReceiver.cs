using TLH.Gameplay.Interactions.Types;

namespace TLH.Gameplay.Interactions
{
    public interface IInteractionReceiver<TInteraction> where TInteraction : Interaction
    {
        public void HandleInteraction(TInteraction interaction, InteractionInitiator initiator);
    }
}