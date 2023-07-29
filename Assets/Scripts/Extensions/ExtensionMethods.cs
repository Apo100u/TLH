using UnityEngine;

namespace TLH.Extensions
{
    public static class ExtensionMethods
    {
        public static bool ContainsLayer(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}