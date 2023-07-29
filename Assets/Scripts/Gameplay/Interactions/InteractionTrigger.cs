using System.Collections.Generic;
using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Interactions
{
    public class InteractionTrigger : MonoBehaviour
    {
        [SerializeField] private List<Interaction> interactions;

        public void AddInteraction(Interaction interaction)
        {
            interactions.Add(interaction);
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            Interactable interactable = collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                InteractWith(interactable);
            }
        }

        private void InteractWith(Interactable interactable)
        {
            for (int i = 0; i < interactions.Count; i++)
            {
                interactable.HandleInteraction(interactions[i]);
            }
        }
    }
}