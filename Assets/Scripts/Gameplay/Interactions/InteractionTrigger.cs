using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Interactions
{
    public class InteractionTrigger : MonoBehaviour
    {
        [SerializeField] private Interaction[] interactions;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            Interactable interactable = collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                InteractWith(interactable);
            }
        }

        private void InteractWith(Interactable interactable)
        {
            for (int i = 0; i < interactions.Length; i++)
            {
                interactable.HandleInteraction(interactions[i]);
            }
        }
    }
}