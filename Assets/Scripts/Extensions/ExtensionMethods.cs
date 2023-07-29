using UnityEngine;

namespace TLH.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Copies this <see cref="LayerMask"/> and add new layer to the copy.
        /// </summary>
        public static LayerMask WithLayer(this LayerMask layerMask, int layerToAdd)
        {
            LayerMask layerMaskWithAddedLayer = layerMask;
            layerMaskWithAddedLayer |= 1 << layerToAdd;
            return layerMaskWithAddedLayer;
        }
        
        public static bool ContainsLayer(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}