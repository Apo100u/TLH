using UnityEngine;

namespace TLH.Gameplay.ObjectPools
{
    public class Pools : MonoBehaviour
    {
        public AttacksPool Attacks { get; } = new();
    }
}