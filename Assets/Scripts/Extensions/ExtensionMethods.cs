using UnityEngine;

namespace TLH.Extensions
{
    public static class ExtensionMethods
    {
        public static bool ContainLayer(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}