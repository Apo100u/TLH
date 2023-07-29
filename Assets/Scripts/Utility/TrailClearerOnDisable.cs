using UnityEngine;

namespace TLH.Utility
{
    public class TrailClearerOnDisable : MonoBehaviour
    {
        private TrailRenderer trailRenderer;
        
        private void Awake()
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        private void OnDisable()
        {
            trailRenderer.Clear();
        }
    }
}