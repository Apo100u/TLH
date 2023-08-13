using UnityEngine;

namespace TLH.Gameplay.Interactions.Types
{
    [CreateAssetMenu(fileName = "New Destroy Interaction", menuName = InteractionMenuName + nameof(DestructionInteraction))]
    public class DestructionInteraction : Interaction
    {
        [SerializeField] private LayerMask layersToDestroy;
        public LayerMask LayersToDestroy => layersToDestroy;
    }
}