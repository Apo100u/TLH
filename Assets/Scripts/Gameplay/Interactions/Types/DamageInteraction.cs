using UnityEngine;

namespace TLH.Gameplay.Interactions.Types
{
    [CreateAssetMenu(fileName = "New Damage Interaction", menuName = InteractionMenuName + nameof(DamageInteraction))]
    public class DamageInteraction : Interaction
    {
        [SerializeField][Min(0f)] private int damagePower = 1;
        public int DamagePower => damagePower;
    }
}