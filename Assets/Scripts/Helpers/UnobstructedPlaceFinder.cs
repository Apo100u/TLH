using UnityEngine;

namespace TLH.Helpers
{
    /// <summary>
    /// Helper that helps in finding place not obstructed by certain layers.
    /// </summary>
    public class UnobstructedPlaceFinder
    {
        private LayerMask obstructedLayerMask;
        
        /// <param name="obstructedLayerMask">All layers that are considered as obstruction.</param>
        public UnobstructedPlaceFinder(LayerMask obstructedLayerMask)
        {
            this.obstructedLayerMask = obstructedLayerMask;
        }
        
        /// <summary>
        /// Finds furthest unobstructed place between points <paramref name="pathStart"/> and <paramref name="pathEnd"/>.
        /// </summary>
        /// <param name="pathStart">Start of the path.</param>
        /// <param name="pathEnd">End of the path.</param>
        /// <param name="searchRadius">Radius around the points on the path that will be checked.</param>
        /// <param name="accuracy">Offset between each check along the path. Lower value gives better results. Higher value gives better performance.</param>
        public Vector2 FindFurthestOnPath(Vector2 pathStart, Vector2 pathEnd, float searchRadius, float accuracy = 0.1f)
        {
            Vector2 unobstructedPlace = pathStart;
            Vector2 positionToCheck = pathEnd;
            Vector2 checkingStep = (pathStart - pathEnd).normalized * accuracy;
            bool unobstructedPlaceFound = false;
            float pathSqrLength = (pathEnd - pathStart).sqrMagnitude;

            while (!unobstructedPlaceFound)
            {
                RaycastHit2D castResult = Physics2D.CircleCast(positionToCheck, searchRadius, Vector2.zero, 0f, obstructedLayerMask);

                if (castResult.collider == null)
                {
                    unobstructedPlaceFound = true;
                    unobstructedPlace = positionToCheck;
                }
                else
                {
                    positionToCheck += checkingStep;

                    if (pathSqrLength < (positionToCheck - pathEnd).sqrMagnitude)
                    {
                        unobstructedPlaceFound = true;
                        unobstructedPlace = pathStart;
                    }
                }
            }
            
            return unobstructedPlace;
        }
    }
}