using UnityEngine;

namespace TLH.Gameplay.ObjectPools
{
    public class Pools : MonoBehaviour
    {
        public ProjectilesPool Projectiles { get; } = new();
    }
}