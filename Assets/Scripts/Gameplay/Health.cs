using TLH.Gameplay.Interactions;
using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay
{
    [RequireComponent(typeof(Interactable))]
    public class Health : MonoBehaviour, IInteractionReceiver<DamageInteraction>
    {
        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;
        
        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void HandleInteraction(DamageInteraction interaction, InteractionInitiator initiator)
        {
            GetDamage(interaction.DamagePower);
        }

        private void GetDamage(int damagePower)
        {
            int adjustedDamage = damagePower > CurrentHealth
                ? CurrentHealth
                : damagePower;

            CurrentHealth -= adjustedDamage;
        }
    }
}