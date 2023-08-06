using TLH.Gameplay.Interactions.Types;
using UnityEngine;

namespace TLH.Gameplay.Interactions.Receivers
{
    public class Health : MonoBehaviour, IInteractionReceiver<DamageInteraction>
    {
        [SerializeField] private int maxHealth;

        private int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void HandleInteraction(DamageInteraction interaction)
        {
            GetDamage(interaction.DamagePower);
        }

        private void GetDamage(int damagePower)
        {
            int adjustedDamage = damagePower > currentHealth
                ? currentHealth
                : damagePower;

            currentHealth -= adjustedDamage;
        }
    }
}